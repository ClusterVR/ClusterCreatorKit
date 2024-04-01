using System;
using System.Linq;
using ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class WorldRuntimeSettingGatherer
    {
        public static bool TryGetWorldRuntimeSetting(Scene scene, out WorldRuntimeSetting result)
        {
            var settings = GatherWorldRuntimeSettings(scene);
            switch (settings.Length)
            {
                case 0:
                    result = null;
                    return false;
                case 1:
                    result = settings[0];
                    return true;
                default:
                    throw new InvalidOperationException("WorldRuntimeSetting count in scene must be 0 or 1");
            }
        }

        public static WorldRuntimeSetting[] GatherWorldRuntimeSettings(Scene scene)
        {
            return scene.GetRootGameObjects()
                .SelectMany(o => o.GetComponentsInChildren<WorldRuntimeSetting>(true))
                .ToArray();
        }
    }
}
