using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class PlayerSettingsConfigurer
    {
        static PlayerSettingsConfigurer()
        {
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_Standard_2_0);
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_Standard_2_0);
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_Standard_2_0);
        }
    }
}
