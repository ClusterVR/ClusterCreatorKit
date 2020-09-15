using UnityEngine;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Preview.PlayerController;
using ClusterVR.CreatorKit.World;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Preview.WarpPortal
{
    public sealed class WarpEventExecutor : MonoBehaviour
    {
        [SerializeField] DesktopPlayerController playerController;
        readonly List<IWarpPortal> warpPortals = new List<IWarpPortal>();

        void Start()
        {
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                var portals = rootGameObject.GetComponentsInChildren<IWarpPortal>(true);
                warpPortals.AddRange(portals);
            }

            foreach (var warpPortal in warpPortals)
            {
                warpPortal.OnEnterWarpPortalEvent += WarpTo;
            }
        }

        void WarpTo(OnEnterWarpPortalEventArgs e)
        {
            if (playerController == null || !e.Target.CompareTag("Player")) return;
            if (!e.KeepPosition)
            {
                playerController.PlayerTransform.position = e.ToPosition;
            }

            if (!e.KeepRotation)
            {
                playerController.CameraTransform.rotation = e.ToRotation;
            }
        }

        void OnDestroy()
        {
            foreach (var warpPortal in warpPortals)
            {
                warpPortal.OnEnterWarpPortalEvent -= WarpTo;
            }
        }
    }
}
