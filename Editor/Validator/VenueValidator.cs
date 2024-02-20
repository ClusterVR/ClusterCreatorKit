using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using ClusterVR.CreatorKit.World.Implements.SpawnPoints;
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

            Debug.Log("Venue Validation is Passed.");
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
                    $"{nameof(DespawnHeight)}はワールドに1つ配置されている必要があります。現在配置されている{nameof(DespawnHeight)}の数は {despawnHeights.Count()} です";
                invalidObjects = despawnHeights.Select(x => x.gameObject).ToArray();
                return false;
            }

            var spawnPoints = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<SpawnPoint>(true));
            var entrances = spawnPoints.Where(x => x.SpawnType == SpawnType.Entrance);
            if (!entrances.Any())
            {
                errorMessage =
                    $"ワールドには{nameof(SpawnPoint)}が「{nameof(SpawnType.Entrance)}」の{nameof(SpawnPoint)}が1つ以上配置されている必要があります";
                invalidObjects = spawnPoints.Select(x => x.gameObject).ToArray();
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
                    $"{nameof(Item.Implements.Item)}の子に{nameof(Item.Implements.Item)}は配置できません";
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
                errorMessage = $"{nameof(ScriptableItem)}のsource codeが長すぎます｡最大値: {Constants.Constants.ScriptableItemMaxSourceCodeByteCount}bytes";
                invalidObjects = invalidScriptableItems.Select(x => x.gameObject).ToArray();
                return false;
            }

            foreach (var itemTemplate in itemTemplates)
            {
                var result = ItemTemplateValidator.Validate(itemTemplate, true);
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
                    $"{nameof(InitializePlayerTrigger)} の Key は {Constants.TriggerGimmick.PersistedPlayerStateKeysCount}種類以下である必要があります。";
                invalidObjects = new GameObject[] { };
                return false;
            }

            if (subScenes.Length > Constants.Constants.MaxSubSceneCount)
            {
                errorMessage =
                    $"ワールドに配置できる{nameof(SubScene)}は最大{Constants.Constants.MaxSubSceneCount}個です。現在配置されている{nameof(SubScene)}数は{subScenes.Count()}です。";
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
                    $"{nameof(SubSceneSubstitutes)}自身もしくは子に{nameof(ClusterVR.CreatorKit.Item.Implements.Item)}は配置できません";
                invalidObjects = substitutesItems.Select(i => i.gameObject).ToArray();
                return false;
            }

            var subSceneSubstitutesInItems = items.SelectMany(i => i.GetComponentsInChildren<SubSceneSubstitutes>(true))
                .ToArray();
            if (subSceneSubstitutesInItems.Any())
            {
                errorMessage =
                    $"{nameof(ClusterVR.CreatorKit.Item.Implements.Item)}の子に{nameof(SubSceneSubstitutes)}は配置できません";
                invalidObjects = subSceneSubstitutesInItems.Select(x => x.gameObject).ToArray();
                return false;
            }

            if (subSceneSubstitutes.Any(s => ((ISubSceneSubstitutes) s).SubScene == null))
            {
                Debug.LogWarning($"Unity Sceneを指定していない{nameof(SubSceneSubstitutes)}があります");
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
                Debug.LogWarning($"Unity Sceneを指定していない{nameof(SubScene)}があります");

                errorMessage = default;
                invalidObjects = default;
                return true;
            }

            var duplication = subScenes.Where(s => ((ISubScene) s).UnityScene == unityScene);
            if (duplication.Count() > 1)
            {
                errorMessage =
                    $"Unity Sceneは複数の{nameof(SubScene)}に指定できません。Scene: {unityScene.name}";
                invalidObjects = duplication.Select(x => x.gameObject).ToArray();
                return false;
            }

            var colliders = subScene.GetComponentsInChildren<Collider>(true).ToArray();
            if (colliders.Length <= 0)
            {
                Debug.LogWarning($"{nameof(SubScene)}にCollierが設定されていません。Scene: {unityScene.name}");
            }
            else if (colliders.Any(c => !c.isTrigger))
            {
                Debug.LogWarning($"isTriggerがonになっていない{nameof(Collider)}が{nameof(SubScene)}内にあります。Scene: {unityScene.name}");
            }

            var assetPath = AssetDatabase.GetAssetPath(unityScene);
            if (string.IsNullOrEmpty(assetPath))
            {
                errorMessage = $"{nameof(SubScene)}に指定されたUnity Sceneが見つかりません: {unityScene.name}";
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
                    errorMessage = $"{nameof(SubScene)}に指定したUnity Sceneでは{name}を使用できません。Scene: {unitySceneName}";
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
                errorMessage = $"ワールドにはTagが「MainCamera」の{nameof(Camera)}を配置できません";
                invalidObjects = mainCameras.Select(x => x.gameObject).ToArray();
                return false;
            }

            var eventSystems = allRootObjects.SelectMany(x => x.GetComponentsInChildren<EventSystem>(true));
            if (eventSystems.Any())
            {
                errorMessage = $"ワールドには{nameof(EventSystem)}を配置できません";
                invalidObjects = eventSystems.Select(x => x.gameObject).ToArray();
                return false;
            }

            var missingPrefabs = GatherMissingPrefabs(allRootObjects);
            if (missingPrefabs.Any())
            {
                errorMessage = $"Prefabを見つけられないGameObjectがあります。アセットを復元するかGameObjectを取り除くかをしてください";
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
                    $"{nameof(RenderMode)}が ScreenSpace である {nameof(Canvas)} には {nameof(PlayerLocalUI)} を追加する必要があります";
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
                errorMessage = $"Key は {Constants.TriggerGimmick.MaxKeyLength}文字以下({nameof(ParameterType)} が {nameof(ParameterType.Vector2)} もしくは {nameof(ParameterType.Vector3)} の場合は{Constants.TriggerGimmick.MaxKeyLength - vectorSuffixLength}文字)である必要があります。";
                invalidObjects = invalidKeyLengthComponents.Select(x => x.gameObject).ToArray();
                return false;
            }

            var idContainers = allRootObjects.SelectMany(x => x.GetComponentsInChildren<IIdContainer>(true)).ToArray();

            var invalidCharacterIdContainers = idContainers
                .Where(c => c.Ids.Any(id => !Constants.Component.ValidIdCharactersRegex.IsMatch(id)))
                .ToArray();
            if (invalidCharacterIdContainers.Any())
            {
                errorMessage = $"Id には英数字とアポストロフィ・カンマ・ハイフン・ピリオド・アンダースコアのみが使用可能です。";
                invalidObjects = invalidCharacterIdContainers.OfType<Component>().Select(x => x.gameObject).ToArray();
                return false;
            }

            var invalidLengthIdContainers = idContainers
                .Where(c => c.Ids.Any(id => id.Length > Constants.Component.MaxIdLength))
                .ToArray();
            if (invalidLengthIdContainers.Any())
            {
                errorMessage = $"Id は {Constants.Component.MaxIdLength}文字以下である必要があります。";
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
