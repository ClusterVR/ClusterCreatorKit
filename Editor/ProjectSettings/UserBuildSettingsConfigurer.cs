using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class UserBuildSettingsConfigurer
    {
        static UserBuildSettingsConfigurer()
        {
#if !UNITY_6000_0_OR_NEWER
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
#endif
        }
    }
}
