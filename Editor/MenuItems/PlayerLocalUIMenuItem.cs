using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.MenuItems
{
    public static class PlayerLocalUIMenuItem
    {
        [MenuItem("GameObject/UI/PlayerLocalUI - cluster")]
        static void CreatePlayerLocalUI()
        {
            var playerLocalUI = ObjectFactory.CreateGameObject(nameof(PlayerLocalUI), typeof(PlayerLocalUI));
            playerLocalUI.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            playerLocalUI.layer = LayerName.UI;
            playerLocalUI.transform.SetParent(MenuItemUtilities.GetActiveContentsRoot());
            Selection.activeGameObject = playerLocalUI;
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.GameObject_PlayerLocalUI);
        }
    }
}
