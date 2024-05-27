using ClusterVR.CreatorKit.Translation;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class UnityVersionChecker
    {
        static UnityVersionChecker()
        {
            if (!Package.PackageInfo.IsRecommendedUnityEditorVersion())
            {
                EditorUtility.DisplayDialog(TranslationTable.cck_attention,
                    TranslationUtility.GetMessage(TranslationTable.cck_unity_version_warning, Package.PackageInfo.RecommendedUnityEditorVersion),
                    TranslationTable.cck_close);
            }
        }
    }
}
