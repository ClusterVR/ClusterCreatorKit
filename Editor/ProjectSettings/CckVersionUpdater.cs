using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEditor;
using PackageInfo = ClusterVR.CreatorKit.Editor.Infrastructure.PackageInfo;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class CckVersionUpdater
    {
        static CckVersionUpdater()
        {
            var version = PackageInfo.GetCreatorKitVersion();
            var prevVersion = ClusterCreatorKitSettings.instance.CckVersion;
            if (version != prevVersion)
            {
                ClusterCreatorKitSettings.instance.CckVersion = version;
                PanamaLogger.LogCckInstall(version, prevVersion ?? "");
            }
        }
    }
}
