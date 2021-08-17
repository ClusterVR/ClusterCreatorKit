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
                EditorUtility.DisplayDialog("注意",
                    $"この Unity バージョンで作成したワールドは正常に動作しない可能性があります。Cluster Creator Kit の推奨 Unity バージョンは {Package.PackageInfo.RecommendedUnityEditorVersion} です。",
                    "閉じる");
            }
        }
    }
}
