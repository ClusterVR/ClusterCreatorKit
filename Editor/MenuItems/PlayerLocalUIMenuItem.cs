using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.MenuItems
{
    public static class PlayerLocalUIMenuItem
    {
        [MenuItem("GameObject/UI/PlayerLocalUI - cluster")]
        static void CreatePlayerLocalUI()
        {
            var playerLocalUI = ObjectFactory.CreateGameObject(nameof(PlayerLocalUI), typeof(PlayerLocalUI));
            var activeGameObject = Selection.activeGameObject;
            if (activeGameObject != null) playerLocalUI.transform.SetParent(activeGameObject.transform);
            Selection.activeGameObject = playerLocalUI;
        }
    }
}