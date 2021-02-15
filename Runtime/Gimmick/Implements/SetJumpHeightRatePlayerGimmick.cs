using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public sealed class SetJumpHeightRatePlayerGimmick : MonoBehaviour, IPlayerEffectGimmick
    {
        [SerializeField] PlayerGimmickKey key = new PlayerGimmickKey("jumpHeight");

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Float;

        public event PlayerEffectEventHandler OnRun;

        sealed class SetJumpHeightRatePlayerEffect : ISetJumpHeightRatePlayerEffect
        {
            public float JumpHeightRate { get; }

            public SetJumpHeightRatePlayerEffect(float jumpHeightRate)
            {
                JumpHeightRate = jumpHeightRate;
            }
        }

        public void Run(GimmickValue value, DateTime current)
        {
            OnRun?.Invoke(new SetJumpHeightRatePlayerEffect(Mathf.Max(value.FloatValue, 0f)));
        }
    }
}
