using UnityEditor;

using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.MenuItems
{
    public static class MenuItemUtilities
    {
        public static Transform GetActiveContentsRoot()
        {
            var activeGameObject = Selection.activeGameObject;
            if (activeGameObject != null)
            {
                return activeGameObject.transform;
            }
            var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                return prefabStage.prefabContentsRoot.transform;
            }
            return null;
        }
    }
}
