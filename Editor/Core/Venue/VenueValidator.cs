using System.Linq;
using ClusterVR.CreatorKit.World;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public static class VenueValidator
    {
        public static bool ValidateVenue(out string errorMessage)
        {
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();

            var despawnHeight = rootObjects.SelectMany(x => x.GetComponentsInChildren<IDespawnHeight>(true));
            if (despawnHeight.Count() != 1)
            {
                errorMessage = $"DespawnHeightはワールドに1つ配置されている必要があります。現在配置されているDespawnHeightの数は {despawnHeight.Count()} です";
                return false;
            }

            var spawnPoints = rootObjects.SelectMany(x => x.GetComponentsInChildren<ISpawnPoint>(true))
                .Where(x => x.SpawnType == SpawnType.Entrance);
            if (!spawnPoints.Any())
            {
                errorMessage = "ワールドにはSpawnTypeが「Entrance」のSpawnPointが1つ以上配置されている必要があります";
                return false;
            }

            var mainCameras = rootObjects.SelectMany(x => x.GetComponentsInChildren<Camera>(true))
                .Where(camera => camera.gameObject.CompareTag("MainCamera"));
            if (mainCameras.Any())
            {
                errorMessage = $"ワールドにはTagが「MainCamera」の{nameof(Camera)}を配置できません";
                return false;
            }

            var eventSystems = rootObjects.SelectMany(x => x.GetComponentsInChildren<EventSystem>(true));
            if (eventSystems.Any())
            {
                errorMessage = $"ワールドには{nameof(EventSystem)}を配置できません";
                return false;
            }

            var items = rootObjects.SelectMany(x => x.GetComponentsInChildren<Item.Implements.Item>(true));
            if (items.Any(i => i.transform.GetComponentsInParent<Item.Implements.Item>(true).Length > 1))
            {
                errorMessage = "アイテムの子にアイテムは配置できません";
                return false;
            }

            Debug.Log("Venue Validation is Passed.");
            errorMessage = "";
            return true;
        }
    }
}
