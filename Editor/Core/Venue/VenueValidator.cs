using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using ClusterVR.CreatorKit.World.Implements.SpawnPoints;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public static class VenueValidator
    {
        public static bool ValidateVenue(out string errorMessage, out GameObject[] invalidObjects)
        {
            var scene = SceneManager.GetActiveScene();
            var sceneRootObjects = scene.GetRootGameObjects();

            var despawnHeights = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<DespawnHeight>(true));
            if (despawnHeights.Count() != 1)
            {
                errorMessage = $"{nameof(DespawnHeight)}はワールドに1つ配置されている必要があります。現在配置されている{nameof(DespawnHeight)}の数は {despawnHeights.Count()} です";
                invalidObjects = despawnHeights.Select(x => x.gameObject).ToArray();
                return false;
            }

            var spawnPoints = sceneRootObjects.SelectMany(x => x.GetComponentsInChildren<SpawnPoint>(true));
            var entrances = spawnPoints.Where(x => x.SpawnType == SpawnType.Entrance);
            if (!entrances.Any())
            {
                errorMessage = $"ワールドには{nameof(SpawnPoint)}が「{nameof(SpawnType.Entrance)}」の{nameof(SpawnPoint)}が1つ以上配置されている必要があります";
                invalidObjects = spawnPoints.Select(x => x.gameObject).ToArray();
                return false;
            }

            var itemTemplates = ItemTemplateGatherer.GatherItemTemplates(scene);
            var allRootObjects = sceneRootObjects.Concat(itemTemplates.Select(t => t.gameObject)).ToArray();
            
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

            var items = allRootObjects.SelectMany(x => x.GetComponentsInChildren<Item.Implements.Item>(true));
            var nestedItems = items.Where(i => i.transform.parent != null && i.transform.parent.GetComponentsInParent<Item.Implements.Item>(true).FirstOrDefault() != null).ToArray();
            if (nestedItems.Any())
            {
                errorMessage = $"{nameof(Item.Implements.Item)}の子に{nameof(Item.Implements.Item)}は配置できません";
                invalidObjects = nestedItems.Select(x => x.gameObject)
                    .Concat(nestedItems.Select(i => i.transform.parent.GetComponentsInParent<Item.Implements.Item>(true).First().gameObject))
                    .Distinct()
                    .ToArray();
                return false;
            }

            var canvases = allRootObjects.SelectMany(x => x.GetComponentsInChildren<Canvas>(true));
            var screenSpaceCanvases = canvases.Where(c => c.isRootCanvas && (c.renderMode == RenderMode.ScreenSpaceCamera || c.renderMode == RenderMode.ScreenSpaceOverlay));
            var unmanagedPlayerLocalUIs = screenSpaceCanvases.Where(c => c.GetComponent<IPlayerLocalUI>() == null).ToArray();
            if (unmanagedPlayerLocalUIs.Any())
            {
                errorMessage = $"{nameof(RenderMode)}が ScreenSpace である {nameof(Canvas)} には {nameof(PlayerLocalUI)} を追加する必要があります";
                invalidObjects = unmanagedPlayerLocalUIs.Select(x => x.gameObject).ToArray();
                return false;
            }

            var globalGimmicks = allRootObjects.SelectMany(x => x.GetComponentsInChildren<IGlobalGimmick>(true));
            var invalidPlayerLocalGlobalGimmick = globalGimmicks.Where(g => !LocalPlayerGimmickValidation.IsValid(g)).ToArray();
            if (invalidPlayerLocalGlobalGimmick.Any())
            {
                errorMessage = LocalPlayerGimmickValidation.ErrorMessage;
                invalidObjects = invalidPlayerLocalGlobalGimmick.Select(x => ((Component) x).gameObject).ToArray();
                return false;
            }

            foreach (var itemTemplate in itemTemplates)
            {
                var result = ItemTemplateValidator.Validate(itemTemplate, onlyErrors: true);
                if (result.Errors.Any())
                {
                    var firstError = result.Errors.First();
                    errorMessage = firstError.Message;
                    invalidObjects = new GameObject[] {itemTemplate.gameObject}; // Validation結果ではprefab内部がSelectされないためrootを返している
                    return false;
                }
            }

            Debug.Log("Venue Validation is Passed.");
            errorMessage = "";
            invalidObjects = new GameObject[]{};
            return true;
        }
    }
}
