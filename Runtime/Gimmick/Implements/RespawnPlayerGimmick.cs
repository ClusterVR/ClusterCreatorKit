using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public sealed class RespawnPlayerGimmick : MonoBehaviour, IPlayerEffectGimmick, IRespawnPlayerEffect
    {
        [SerializeField] PlayerGimmickKey key = new PlayerGimmickKey("respawn");

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event PlayerEffectEventHandler OnRun;

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            OnRun?.Invoke(this);
        }
    }
}
