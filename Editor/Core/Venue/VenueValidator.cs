using System.Linq;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
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
            var rootObjects = scene.GetRootGameObjects();

            var despawnHeights = rootObjects.SelectMany(x => x.GetComponentsInChildren<DespawnHeight>(true));
            if (despawnHeights.Count() != 1)
            {
                errorMessage = $"{nameof(DespawnHeight)}はワールドに1つ配置されている必要があります。現在配置されている{nameof(DespawnHeight)}の数は {despawnHeights.Count()} です";
                invalidObjects = despawnHeights.Select(x => x.gameObject).ToArray();
                return false;
            }

            var spawnPoints = rootObjects.SelectMany(x => x.GetComponentsInChildren<SpawnPoint>(true));
            var entrances = spawnPoints.Where(x => x.SpawnType == SpawnType.Entrance);
            if (!entrances.Any())
            {
                errorMessage = $"ワールドには{nameof(SpawnPoint)}が「{nameof(SpawnType.Entrance)}」の{nameof(SpawnPoint)}が1つ以上配置されている必要があります";
                invalidObjects = spawnPoints.Select(x => x.gameObject).ToArray();
                return false;
            }

            var mainCameras = rootObjects.SelectMany(x => x.GetComponentsInChildren<Camera>(true))
                .Where(camera => camera.gameObject.CompareTag("MainCamera"));
            if (mainCameras.Any())
            {
                errorMessage = $"ワールドにはTagが「MainCamera」の{nameof(Camera)}を配置できません";
                invalidObjects = mainCameras.Select(x => x.gameObject).ToArray();
                return false;
            }

            var eventSystems = rootObjects.SelectMany(x => x.GetComponentsInChildren<EventSystem>(true));
            if (eventSystems.Any())
            {
                errorMessage = $"ワールドには{nameof(EventSystem)}を配置できません";
                invalidObjects = eventSystems.Select(x => x.gameObject).ToArray();
                return false;
            }

            var items = rootObjects.SelectMany(x => x.GetComponentsInChildren<Item.Implements.Item>(true));
            var nestedItems = items.Where(i => i.transform.parent != null && i.transform.parent.GetComponentInParent<Item.Implements.Item>() != null);
            if (nestedItems.Any())
            {
                errorMessage = $"{nameof(Item.Implements.Item)}の子に{nameof(Item.Implements.Item)}は配置できません";
                invalidObjects = nestedItems.Select(x => x.gameObject).ToArray();
                return false;
            }

            Debug.Log("Venue Validation is Passed.");
            errorMessage = "";
            invalidObjects = new GameObject[]{};
            return true;
        }
    }
}
