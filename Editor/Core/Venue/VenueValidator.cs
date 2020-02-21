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

            var despawnHeight = rootObjects.SelectMany(x => x.GetComponentsInChildren<IDespawnHeight>());
            if (despawnHeight.Count() != 1)
            {
                errorMessage = $"{nameof(IDespawnHeight)}の数はScene上で1つだけにしてください。現在{nameof(IDespawnHeight)}の数は {despawnHeight.Count()} です。";
                return false;
            }

            var spawnPoints = rootObjects.SelectMany(x => x.GetComponentsInChildren<ISpawnPoint>());
            if (!spawnPoints.Any())
            {
                errorMessage = $"{nameof(ISpawnPoint)}はScene上に最低でも1つは配置してください";
                return false;
            }

            var mainCameras = rootObjects.SelectMany(x => x.GetComponentsInChildren<Camera>()).Where(camera => camera.gameObject.tag == "MainCamera");
            if (mainCameras.Any())
            {
                errorMessage = $"Scene上にはMainCameraを配置しないでください";
                return false;
            }

            var eventSystems = rootObjects.SelectMany(x => x.GetComponentsInChildren<EventSystem>());
            if (eventSystems.Any())
            {
                errorMessage = $"Scene上には、{nameof(EventSystem)}を配置しないでください";
                return false;
            }

            Debug.Log("Venue Validation is Passed.");
            errorMessage = "";
            return true;
        }
    }
}
