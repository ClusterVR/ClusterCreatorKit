using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class PlayerEffectManager
    {
        readonly PlayerPresenter playerPresenter;

        public PlayerEffectManager(PlayerPresenter playerPresenter, ItemCreator itemCreator, IEnumerable<IPlayerEffect> playerEffects)
        {
            this.playerPresenter = playerPresenter;
            itemCreator.OnCreate += OnCreateItem;
            RegisterPlayerEffects(playerEffects);
        }

        void OnCreateItem(IItem item)
        {
            RegisterPlayerEffects(item.gameObject.GetComponentsInChildren<IPlayerEffect>(true));
        }

        void RegisterPlayerEffects(IEnumerable<IPlayerEffect> playerEffects)
        {
            foreach (var playerEffect in playerEffects)
            {
                RegisterPlayerEffect(playerEffect);
            }
        }

        void RegisterPlayerEffect(IPlayerEffect playerEffect)
        {
            playerEffect.OnRun += Run;
        }

        void Run(IPlayerEffect playerEffect)
        {
            switch (playerEffect)
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