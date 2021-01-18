using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorSettings
{
    public static class ProjectSettingsConfigurer
    {
        public static void Setup()
        {
            Physics.autoSyncTransforms = true;
        }
    }
}
