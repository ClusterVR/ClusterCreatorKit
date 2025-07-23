using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using PackageInfo = ClusterVR.CreatorKit.Editor.Infrastructure.PackageInfo;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class UnityVersionChecker
    {
        static UnityVersionChecker()
        {
            if (!PackageInfo.IsRecommendedUnityEditorVersion())
            {
                EditorUtility.DisplayDialog(TranslationTable.cck_attention,
                    TranslationUtility.GetMessage(TranslationTable.cck_unity_version_warning, PackageInfo.RecommendedUnityEditorVersion),
                    TranslationTable.cck_close);
            }
        }
    }
}
