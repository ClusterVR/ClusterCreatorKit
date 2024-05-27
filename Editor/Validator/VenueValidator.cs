using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using ClusterVR.CreatorKit.World.Implements.SpawnPoints;
using ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class VenueValidator
    {
        public static bool ValidateVenue(bool isBeta, out string errorMessage, out GameObject[] invalidObjects)
        {
            var scene = SceneManager.GetActiveScene();
            var sceneRootObjects = scene.GetRootGameObjects();
            var itemTemplates = ItemTemplateGatherer.GatherItemTemplates(scene).ToArray();
            var allRootObjects = sceneRootObjects.Concat(itemTemplates.Select(t => t.gameObject)).ToArray();
            var subScenes = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<SubScene>(true)).ToArray();

            if (!ValidateMainSceneVenue(isBeta, scene, sceneRootObjects, itemTemplates, allRootObjects, subScenes, out errorMessage, out invalidObjects))
            {
                return false;
            }

            foreach (var subScene in subScenes)
            {
                if (!ValidateSubScene(isBeta, subScene, out errorMessage, out invalidObjects))
                {
                    return false;
                }
            }

            Debug.Log(TranslationTable.cck_venue_validation_passed);
            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static bool ValidateMainSceneVenue(bool isBeta, Scene scene, GameObject[] sceneRootObjects, IEnumerable<IItem> itemTemplates, GameObject[] allRootObjects, SubScene[] subScenes, out string errorMessage, out GameObject[] invalidObjects)
        {
            var despawnHeights = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<DespawnHeight>(true));
            if (despawnHeights.Count() != 1)
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_despawnheight_single_instance, nameof(DespawnHeight), nameof(DespawnHeight), despawnHeights.Count());
                invalidObjects = despawnHeights.Select(x => x.gameObject).ToArray();
                return false;
            }

            var spawnPoints = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<SpawnPoint>(true));
            var entrances = spawnPoints.Where(x => x.SpawnType == SpawnType.Entrance);
            if (!entrances.Any())
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_spawnpoint_entrance_required, nameof(SpawnPoint), nameof(SpawnType.Entrance), nameof(SpawnPoint));
                invalidObjects = spawnPoints.Select(x => x.gameObject).ToArray();
                return false;
            }

            var worldRuntimeSettings = WorldRuntimeSettingGatherer.GatherWorldRuntimeSettings(scene);
            if (worldRuntimeSettings.Length >= 2)
            {
                errorMessage =
                    $"ワールドに配置できる{nameof(WorldRuntimeSetting)}は最大1つです。現在配置されている{nameof(WorldRuntimeSetting)}の数は {worldRuntimeSettings.Length} です";
                invalidObjects = worldRuntimeSettings.Select(x => x.gameObject).ToArray();
                return false;
            }

            if (!ValidateVenueScene(isBeta, allRootObjects, out errorMessage, out invalidObjects))
            {
                return false;
            }

            var items = allRootObjects.SelectMany(x =>
                x.GetComponentsInChildren<ClusterVR.CreatorKit.Item.Implements.Item>(true));
            var nestedItems = items.Where(i =>
                    i.transform.parent != null && i.transform.parent
                        .GetComponentsInParent<ClusterVR.CreatorKit.Item.Implements.Item>(true).FirstOrDefault() !=
                    null)
                .ToArray();
            if (nestedItems.Any())
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_item_child_of_item, nameof(Item.Implements.Item), nameof(Item.Implements.Item));
                invalidObjects = nestedItems.Select(x => x.gameObject)
                    .Concat(nestedItems.Select(i =>
                        i.transform.parent.GetComponentsInParent<ClusterVR.CreatorKit.Item.Implements.Item>(true)
                            .First().gameObject))
                    .Distinct()
                    .ToArray();
                return false;
            }

            var scriptableItems = allRootObjects.SelectMany(x =>
                x.GetComponentsInChildren<ClusterVR.CreatorKit.Item.Implements.ScriptableItem>(true));
            var invalidScriptableItems = scriptableItems.Where(s => !s.IsValid(true)).ToArray();
            if (invalidScriptableItems.Any())
            {
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_scriptableitem_source_code_too_long, nameof(ScriptableItem), Constants.Constants.ScriptableItemMaxSourceCodeByteCount);
                invalidObjects = invalidScriptableItems.Select(x => x.gameObject).ToArray();
                return false;
            }

            foreach (var itemTemplate in itemTemplates)
            {
                var result = ItemTemplateValidator.Validate(isBeta, itemTemplate, true);
                if (result.Errors.Any())
                {
                    var firstError = result.Errors.First();
                    errorMessage = firstError.Message;
                    invalidObjects = new[] { itemTemplate.gameObject }; // Validation結果ではprefab内部がSelectされないためrootを返している
                    return false;
                }
            }

            var persistedKeys = PersistedPlayerStateKeysGatherer.Gather(scene);
            if (persistedKeys.Count > Constants.TriggerGimmick.PersistedPlayerStateKeysCount)
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_initializeplayertrigger_key_limit, nameof(InitializePlayerTrigger), Constants.TriggerGimmick.PersistedPlayerStateKeysCount);
                invalidObjects = new GameObject[] { };
                return false;
            }

            if (subScenes.Length > Constants.Constants.MaxSubSceneCount)
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_subscene_max_count, nameof(SubScene), Constants.Constants.MaxSubSceneCount, nameof(SubScene), subScenes.Count());
                invalidObjects = subScenes.Select(x => x.gameObject).ToArray();
                return false;
            }

            foreach (var subScene in subScenes)
            {
                if (!ValidateSubSceneComponent(subScenes, subScene, out errorMessage, out invalidObjects))
                {
                    return false;
                }
            }

            var subSceneSubstitutes = allRootObjects.SelectMany(x => x.GetComponentsInChildren<SubSceneSubstitutes>(true));
            var substitutesItems = subSceneSubstitutes.SelectMany(s =>
                s.GetComponentsInChildren<ClusterVR.CreatorKit.Item.Implements.Item>(true))
                .ToArray();
            if (substitutesItems.Any())
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_subscenesubstitutes_no_item, nameof(SubSceneSubstitutes), nameof(ClusterVR.CreatorKit.Item.Implements.Item));
                invalidObjects = substitutesItems.Select(i => i.gameObject).ToArray();
                return false;
            }

            var subSceneSubstitutesInItems = items.SelectMany(i => i.GetComponentsInChildren<SubSceneSubstitutes>(true))
                .ToArray();
            if (subSceneSubstitutesInItems.Any())
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_item_no_subscenesubstitutes_child, nameof(ClusterVR.CreatorKit.Item.Implements.Item), nameof(SubSceneSubstitutes));
                invalidObjects = subSceneSubstitutesInItems.Select(x => x.gameObject).ToArray();
                return false;
            }

            if (subSceneSubstitutes.Any(s => ((ISubSceneSubstitutes) s).SubScene == null))
            {
                Debug.LogWarning(TranslationUtility.GetMessage(TranslationTable.cck_subscenesubstitutes_no_unity_scene, nameof(SubSceneSubstitutes)));
            }

            foreach (var item in items)
            {
                if (!ValidateMaterialSetList(isBeta, item, out errorMessage, out invalidObjects))
                {
                    return false;
                }
            }

            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static bool ValidateSubSceneComponent(IEnumerable<SubScene> subScenes, SubScene subScene, out string errorMessage, out GameObject[] invalidObjects)
        {
            var unityScene = ((ISubScene) subScene).UnityScene;

            if (unityScene == null)
            {
                Debug.LogWarning(TranslationUtility.GetMessage(TranslationTable.cck_subscene_no_unity_scene, nameof(SubScene)));

                errorMessage = default;
                invalidObjects = default;
                return true;
            }

            var duplication = subScenes.Where(s => ((ISubScene) s).UnityScene == unityScene);
            if (duplication.Count() > 1)
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_subscene_unity_scene_multiple_assignment, nameof(SubScene), unityScene.name);
                invalidObjects = duplication.Select(x => x.gameObject).ToArray();
                return false;
            }

            var colliders = subScene.GetComponentsInChildren<Collider>(true).ToArray();
            if (colliders.Length <= 0)
            {
                Debug.LogWarning(TranslationUtility.GetMessage(TranslationTable.cck_subscene_no_collider, nameof(SubScene), unityScene.name));
            }
            else if (colliders.Any(c => !c.isTrigger))
            {
                Debug.LogWarning(TranslationUtility.GetMessage(TranslationTable.cck_subscene_trigger_not_on, nameof(Collider), nameof(SubScene), unityScene.name));
            }

            var assetPath = AssetDatabase.GetAssetPath(unityScene);
            if (string.IsNullOrEmpty(assetPath))
            {
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_subscene_unity_scene_not_found, nameof(SubScene), unityScene.name);
                invalidObjects = new GameObject[] { subScene.gameObject };
                return false;
            }

            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static bool ValidateSubScene(bool isBeta, SubScene subScene, out string errorMessage, out GameObject[] invalidObjects)
        {
            var unityScene = ((ISubScene) subScene).UnityScene;
            if (unityScene == null)
            {
                errorMessage = default;
                invalidObjects = default;
                return true;
            }

            var assetPath = AssetDatabase.GetAssetPath(unityScene);
            var subSceneAsset = SceneManager.GetSceneByPath(assetPath);
            var isLoaded = subSceneAsset.isLoaded;
            var isAdded = subSceneAsset.IsValid();
            if (!isLoaded)
            {
                subSceneAsset = EditorSceneManager.OpenScene(assetPath, OpenSceneMode.Additive);
            }

            try
            {
                return ValidateSubSceneVenue(isBeta, subSceneAsset, unityScene.name, out errorMessage, out invalidObjects);
            }
            finally
            {
                if (!isLoaded)
                {
                    EditorSceneManager.CloseScene(subSceneAsset, !isAdded);
                }
            }
        }

        static bool ValidateSubSceneVenue(bool isBeta, Scene scene, string unitySceneName, out string errorMessage, out GameObject[] invalidObjects)
        {
            var sceneRootObjects = scene.GetRootGameObjects();

            bool ValidateNoComponents<T>(string name, out string errorMessage, out GameObject[] invalidObjects)
            {
                var components = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<T>(true));
                if (components.Any())
                {
                    errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_subscene_invalid_unity_scene, nameof(SubScene), name, unitySceneName);
                    invalidObjects = components.OfType<Component>().Select(x => x.gameObject).ToArray();
                    return false;
                }
                else
                {
                    errorMessage = default;
                    invalidObjects = default;
                    return true;
                }
            }

            if (!ValidateNoComponents<Item.Implements.Item>("Item", out errorMessage, out invalidObjects))
            {
                return false;
            }

            if (!ValidateNoComponents<ITrigger>("Trigger", out errorMessage, out invalidObjects))
            {
                return false;
            }

            if (!ValidateNoComponents<IPlayerGimmick>("PlayerGimmick", out errorMessage, out invalidObjects))
            {
                return false;
            }

            if (!ValidateNoComponents<IPlayerLocalUI>("PlayerLocalUI", out errorMessage, out invalidObjects))
            {
                return false;
            }

            if (!ValidateVenueScene(isBeta, sceneRootObjects, out errorMessage, out invalidObjects))
            {
                return false;
            }

            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static bool ValidateVenueScene(bool isBeta, GameObject[] allRootObjects, out string errorMessage, out GameObject[] invalidObjects)
        {
            var mainCameras = allRootObjects.SelectMany(x => x.GetComponentsInChildren<Camera>(true))
                .Where(camera => camera.gameObject.CompareTag("MainCamera"));
            if (mainCameras.Any())
            {
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_no_maincamera_tag_in_world, nameof(Camera));
                invalidObjects = mainCameras.Select(x => x.gameObject).ToArray();
                return false;
            }

            var eventSystems = allRootObjects.SelectMany(x => x.GetComponentsInChildren<EventSystem>(true));
            if (eventSystems.Any())
            {
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_no_eventsystem_in_world, nameof(EventSystem));
                invalidObjects = eventSystems.Select(x => x.gameObject).ToArray();
                return false;
            }

            var missingPrefabs = GatherMissingPrefabs(allRootObjects);
            if (missingPrefabs.Any())
            {
                errorMessage = TranslationTable.cck_prefab_not_found;
                invalidObjects = missingPrefabs.ToArray();
                return false;
            }

            var canvases = allRootObjects.SelectMany(x => x.GetComponentsInChildren<Canvas>(true));
            var screenSpaceCanvases = canvases.Where(c =>
                c.isRootCanvas && (c.renderMode == RenderMode.ScreenSpaceCamera ||
                    c.renderMode == RenderMode.ScreenSpaceOverlay));
            var unmanagedPlayerLocalUIs =
                screenSpaceCanvases.Where(c => c.GetComponent<IPlayerLocalUI>() == null).ToArray();
            if (unmanagedPlayerLocalUIs.Any())
            {
                errorMessage =
                    TranslationUtility.GetMessage(TranslationTable.cck_canvas_requires_playerlocalui, nameof(RenderMode), nameof(Canvas), nameof(PlayerLocalUI));
                invalidObjects = unmanagedPlayerLocalUIs.Select(x => x.gameObject).ToArray();
                return false;
            }

            var globalGimmicks = allRootObjects.SelectMany(x => x.GetComponentsInChildren<IGlobalGimmick>(true));
            var invalidPlayerLocalGlobalGimmick =
                globalGimmicks.Where(g => !LocalPlayerGimmickValidation.IsValid(g)).ToArray();
            if (invalidPlayerLocalGlobalGimmick.Any())
            {
                errorMessage = LocalPlayerGimmickValidation.ErrorMessage;
                invalidObjects = invalidPlayerLocalGlobalGimmick.Select(x => ((Component) x).gameObject).ToArray();
                return false;
            }

            var triggers = allRootObjects.SelectMany(x => x.GetComponentsInChildren<ITrigger>(true));
            var invalidKeyLengthTriggers = triggers
                .Where(g => g.TriggerParams.SelectMany(p => p.GetKeyWithFieldNames())
                    .Any(key => key.Length > Constants.TriggerGimmick.MaxKeyLength)).ToArray();
            var gimmicks = allRootObjects.SelectMany(x => x.GetComponentsInChildren<IGimmick>(true));
            var invalidKeyLengthGimmicks =
                gimmicks.Where(g => g.Key.Length > Constants.TriggerGimmick.MaxKeyLength).ToArray();
            var invalidKeyLengthComponents = invalidKeyLengthTriggers.OfType<Component>()
                .Concat(invalidKeyLengthGimmicks.OfType<Component>())
                .ToArray();
            if (invalidKeyLengthComponents.Any())
            {
                const int vectorSuffixLength = 2;
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_key_length_limit, Constants.TriggerGimmick.MaxKeyLength, nameof(ParameterType), nameof(ParameterType.Vector2), nameof(ParameterType.Vector3), Constants.TriggerGimmick.MaxKeyLength - vectorSuffixLength);
                invalidObjects = invalidKeyLengthComponents.Select(x => x.gameObject).ToArray();
                return false;
            }

            var idContainers = allRootObjects.SelectMany(x => x.GetComponentsInChildren<IIdContainer>(true)).ToArray();

            var invalidCharacterIdContainers = idContainers
                .Where(c => c.Ids.Any(id => !Constants.Component.ValidIdCharactersRegex.IsMatch(id)))
                .ToArray();
            if (invalidCharacterIdContainers.Any())
            {
                errorMessage = TranslationTable.cck_id_invalid_characters;
                invalidObjects = invalidCharacterIdContainers.OfType<Component>().Select(x => x.gameObject).ToArray();
                return false;
            }

            var invalidLengthIdContainers = idContainers
                .Where(c => c.Ids.Any(id => id.Length > Constants.Component.MaxIdLength))
                .ToArray();
            if (invalidLengthIdContainers.Any())
            {
                errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_id_length_limit, Constants.Component.MaxIdLength);
                invalidObjects = invalidLengthIdContainers.OfType<Component>().Select(x => x.gameObject).ToArray();
                return false;
            }

            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static bool ValidateNoComponents<T>(GameObject[] allRootObjects, Func<T, bool> filter, out GameObject[] invalidObjects)
        {
            var components = allRootObjects.SelectMany(x => x.GetComponentsInChildren<T>(true)).Where(filter);
            if (components.Any())
            {
                invalidObjects = components.OfType<Component>().Select(x => x.gameObject).ToArray();
                return false;
            }

            invalidObjects = default;
            return true;
        }

        static bool ValidateMaterialSetList(bool isBeta, IItem item, out string errorMessage, out GameObject[] invalidObjects)
        {
            var gameObject = item.gameObject;
            var itemMaterialSetList = gameObject.GetComponent<IItemMaterialSetList>();
            if (itemMaterialSetList != null)
            {
                var errorMessages = ItemMaterialSetListValidator.Validate(isBeta, item.gameObject, itemMaterialSetList).ToArray();
                if (errorMessages.Any())
                {
                    errorMessage = string.Join('\n', errorMessages);
                    invalidObjects = new[] { gameObject };
                    return false;
                }
            }
            errorMessage = default;
            invalidObjects = default;
            return true;
        }

        static IEnumerable<GameObject> GatherMissingPrefabs(IEnumerable<GameObject> rootObjects)
        {
            var result = new List<GameObject>();
            foreach (var rootObject in rootObjects)
            {
                GatherMissingPrefabs(rootObject, result);
            }
            return result;
        }

        static void GatherMissingPrefabs(GameObject gameObject, IList<GameObject> result)
        {
            if (PrefabUtility.GetPrefabAssetType(gameObject) == PrefabAssetType.MissingAsset)
            {
                result.Add(gameObject);
            }
            foreach (Transform child in gameObject.transform)
            {
                GatherMissingPrefabs(child.gameObject, result);
            }
        }
    }
}
