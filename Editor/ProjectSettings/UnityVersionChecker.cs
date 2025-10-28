#if !CLUSTER_CREATOR_KIT_DISABLE_PREVIEW
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using PackageInfo = ClusterVR.CreatorKit.Editor.Infrastructure.PackageInfo;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class UnityVersionChecker
    {
        const string UnityVersionCheckedKey = "ClusterCreatorKitUnityVersionChecker";
        static UnityVersionChecker()
        {
            if (SessionState.GetBool(UnityVersionCheckedKey, false))
            {
                return;
            }
            if (!PackageInfo.IsSupportedUnityEditorVersion())
            {
                EditorUtility.DisplayDialog(TranslationTable.cck_attention,
                    TranslationUtility.GetMessage(TranslationTable.cck_unity_version_warning, PackageInfo.RecommendedUnityEditorVersion),
                    TranslationTable.cck_close);
            }
            SessionState.SetBool(UnityVersionCheckedKey, true);
        }
    }
}
#endif
