using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
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
                    "移動する床に追従する設定が有効ですが、プレビューではこの挙動は再現されません。この挙動を確認する場合はワールドをアップロードし、clusterアプリを使用して下さい"
                    );
            }
        }
    }
}
