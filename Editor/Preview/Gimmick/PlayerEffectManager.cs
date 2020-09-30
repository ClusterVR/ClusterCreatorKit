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

        public PlayerEffectManager(PlayerPresenter playerPresenter, ItemCreator itemCreator, IEnumerable<IPlayerEffectGimmick> playerEffectGimmicks)
        {
            this.playerPresenter = playerPresenter;
            itemCreator.OnCreate += OnCreateItem;
            RegisterPlayerEffects(playerEffectGimmicks);
        }

        void OnCreateItem(IItem item)
        {
            RegisterPlayerEffects(item.gameObject.GetComponentsInChildren<IPlayerEffectGimmick>(true));
        }

        void RegisterPlayerEffects(IEnumerable<IPlayerEffectGimmick> playerEffectGimmicks)
        {
            foreach (var playerEffectGimmick in playerEffectGimmicks)
            {
                RegisterPlayerEffect(playerEffectGimmick);
            }
        }

        void RegisterPlayerEffect(IPlayerEffectGimmick playerEffectGimmick)
        {
            playerEffectGimmick.OnRun += Run;
        }

        void Run(IPlayerEffect playerEffect)
        {
            switch (playerEffect)
            {
                case IWarpPlayerEffect warpPlayerEffect:
                    if (!warpPlayerEffect.KeepPosition)
                    {
                        playerPresenter.MoveTo(warpPlayerEffect.TargetPosition);
                    }
                    if (!warpPlayerEffect.KeepRotation)
                    {
                        playerPresenter.RotateTo(warpPlayerEffect.TargetRotation);
                    }
                    break;
                case IRespawnPlayerEffect _:
                    playerPresenter.Respawn();
                    break;
                case ISetMoveSpeedRatePlayerEffect setMoveSpeedRatePlayerEffect:
                    playerPresenter.SetMoveSpeedRate(setMoveSpeedRatePlayerEffect.MoveSpeedRate);
                    break;
            }
        }
    }
}