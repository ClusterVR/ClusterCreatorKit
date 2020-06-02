using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class PlayerGimmickManager
    {
        readonly PlayerPresenter playerPresenter;

        public PlayerGimmickManager(PlayerPresenter playerPresenter, ItemCreator itemCreator, IEnumerable<IPlayerGimmick> playerGimmicks)
        {
            this.playerPresenter = playerPresenter;
            itemCreator.OnCreate += OnCreateItem;
            RegisterPlayerGimmicks(playerGimmicks);
        }

        void OnCreateItem(IItem item)
        {
            RegisterPlayerGimmicks(item.gameObject.GetComponentsInChildren<IPlayerGimmick>(true));
        }

        void RegisterPlayerGimmicks(IEnumerable<IPlayerGimmick> playerGimmicks)
        {
            foreach (var playerGimmick in playerGimmicks)
            {
                RegisterPlayerGimmick(playerGimmick);
            }
        }

        void RegisterPlayerGimmick(IPlayerGimmick playerGimmick)
        {
            playerGimmick.OnRun += Run;
        }

        void Run(IPlayerGimmick playerGimmick)
        {
            switch (playerGimmick)
            {
                case IWarpPlayerGimmick warpPlayerGimmick:
                    if (!warpPlayerGimmick.KeepPosition)
                    {
                        playerPresenter.MoveTo(warpPlayerGimmick.TargetPosition);
                    }
                    if (!warpPlayerGimmick.KeepRotation)
                    {
                        playerPresenter.RotateTo(warpPlayerGimmick.TargetRotation);
                    }
                    break;
                case IRespawnPlayerGimmick _:
                    playerPresenter.Respawn();
                    break;
            }
        }
    }
}