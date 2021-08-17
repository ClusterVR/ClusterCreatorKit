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
            playerLocalUI.transform.SetParent(MenuItemUtilities.GetActiveContentsRoot());
            Selection.activeGameObject = playerLocalUI;
        }
    }
}
