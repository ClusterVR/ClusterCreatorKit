using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public sealed class SetMoveSpeedRatePlayerGimmick : MonoBehaviour, IPlayerEffectGimmick
    {
        [SerializeField] PlayerGimmickKey key = new PlayerGimmickKey();

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Float;

        public event PlayerEffectEventHandler OnRun;

        sealed class SetMoveSpeedRatePlayerEffect : ISetMoveSpeedRatePlayerEffect
        {
            public float MoveSpeedRate { get; }

            public SetMoveSpeedRatePlayerEffect(float moveSpeedRate)
            {
                MoveSpeedRate = moveSpeedRate;
            }
        }

        public void Run(GimmickValue value, DateTime current)
        {
            OnRun?.Invoke(new SetMoveSpeedRatePlayerEffect(Mathf.Max(value.FloatValue, 0f)));
        }
    }
}