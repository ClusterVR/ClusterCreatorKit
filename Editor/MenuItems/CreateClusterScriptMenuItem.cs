using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.MenuItems
{
    public static class CreateClusterScriptMenuItem
    {
        [MenuItem("Assets/Create/cluster/ClusterScript", priority = 0)]
        static void Create()
        {
            ProjectWindowUtil.CreateAssetWithContent("ClusterScript.js", "", (Texture2D) EditorGUIUtility.IconContent("d_ScriptableObject Icon").image);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Assets_ClusterScript);
        }
    }
}
