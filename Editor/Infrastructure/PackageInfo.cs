using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Infrastructure
{
    public static class PackageInfo
    {
        const string RecommendedUnity6000EditorVersion = "6000.2.6f2";
        const string RecommendedUnity2021EditorVersion = "2021.3.4f1";

        public const string RecommendedUnityEditorVersion = RecommendedUnity6000EditorVersion;

        static readonly string[] SupportedUnityEditorVersions =
        {
            RecommendedUnity6000EditorVersion,
            RecommendedUnity2021EditorVersion
        };

        public static string GetCreatorKitVersion()
        {
            var type = MethodBase.GetCurrentMethod().DeclaringType;
            var assembly = Assembly.GetAssembly(type);
            var package = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
            return package.version;
        }

        public static bool IsSupportedUnityEditorVersion()
        {
            return SupportedUnityEditorVersions.Contains(Application.unityVersion);
        }
    }
}
