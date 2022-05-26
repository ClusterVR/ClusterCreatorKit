using System;
using System.Reflection;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Package
{
    public static class PackageInfo
    {
        public const string RecommendedUnityEditorVersion = "2021.3";

        public static string GetCreatorKitVersion()
        {
            var type = MethodBase.GetCurrentMethod().DeclaringType;
            var assembly = Assembly.GetAssembly(type);
            var package = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
            return package.version;
        }

        public static bool IsRecommendedUnityEditorVersion()
        {
            return Application.unityVersion.StartsWith(RecommendedUnityEditorVersion, StringComparison.Ordinal);
        }
    }
}
