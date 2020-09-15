using System;
using System.CodeDom;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor
{
    [InitializeOnLoad]
    public static class EditorInitializer
    {
        static EditorInitializer()
        {
            if (!IsRecommendedUnityEditorVersion()) EditorUtility.DisplayDialog("注意", $"この Unity バージョンで作成したワールドは正常に動作しない可能性があります。Cluster Creator Kit の推奨 Unity バージョンは {Core.Constants.RecommendedUnityEditorVersion} です。", "閉じる");
        }
        static bool IsRecommendedUnityEditorVersion()
        {
            return Application.unityVersion.StartsWith(Core.Constants.RecommendedUnityEditorVersion, StringComparison.Ordinal);
        }
    }
}