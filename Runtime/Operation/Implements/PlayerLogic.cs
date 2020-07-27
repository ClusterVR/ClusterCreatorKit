using System;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public class PlayerLogic : MonoBehaviour, IPlayerLogic
    {
        [SerializeField, PlayerGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Player);
        [SerializeField, PlayerLogic] Logic logic;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event RunPlayerLogicEventHandler OnRunPlayerLogic;

        DateTime lastTriggeredAt;
        bool validated;
        bool isValid;

        public void Run(GimmickValue value, DateTime current)
        {
            if (!validated) Validate();
            if (!isValid) return;
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;

            OnRunPlayerLogic?.Invoke(new RunPlayerLogicEventArgs(logic));
        }

        void Validate()
        {
            isValid = logic != null && logic.IsValid();
            validated = true;
        }
    }
}