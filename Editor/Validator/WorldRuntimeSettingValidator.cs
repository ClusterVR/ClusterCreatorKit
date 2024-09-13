using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor
{
    public static class WorldRuntimeSettingValidator
    {
        public static void ShowWarningIfPreviewUnsupportedSettingDetected(Scene scene)
        {
            var settings = WorldRuntimeSettingGatherer.GatherWorldRuntimeSettings(scene);
            if (settings.Length == 0 || settings.Any(s => s.UseMovingPlatform))
            {
                Debug.Log(
                    TranslationTable.cck_follow_moving_floor_preview
                    );
            }
        }
    }
}
