using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public class RespawnPlayerGimmick : MonoBehaviour, IRespawnPlayerGimmick
    {
        [SerializeField, PlayerGimmickKey] GimmickKey key = new GimmickKey(Target.Player, "respawn");

        Target IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event PlayerGimmickEventHandler OnRun;

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
            OnRun?.Invoke(this);
        }
    }
}