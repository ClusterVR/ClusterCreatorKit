using System.Collections.Generic;
using ClusterVR.CreatorKit.World;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class WarpPortalExecutor
    {
        readonly PlayerPresenter playerPresenter;

        public WarpPortalExecutor(PlayerPresenter playerPresenter, IEnumerable<IWarpPortal> warpPortals)
        {
            this.playerPresenter = playerPresenter;
            foreach (var warpPortal in warpPortals)
            {
                warpPortal.OnEnterWarpPortalEvent += OnEnterWarpPortal;
            }
        }

        void OnEnterWarpPortal(OnEnterWarpPortalEventArgs e)
        {
            if (!e.Target.CompareTag("Player"))
            {
                return;
            }
            if (!e.KeepPosition)
            {
                playerPresenter.WarpTo(e.ToPosition);
            }

            if (!e.KeepRotation)
            {
                playerPresenter.RotateTo(e.ToRotation);
            }
        }
    }
}
