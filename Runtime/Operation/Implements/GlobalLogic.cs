using System;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public class GlobalLogic : MonoBehaviour, IGlobalLogic
    {
        [SerializeField, ConsistentlySyncGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField, GlobalLogic] Logic logic;

        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event RunGlobalLogicEventHandler OnRunGlobalLogic;

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

            OnRunGlobalLogic?.Invoke(new RunGlobalLogicEventArgs(logic));
        }

        void Validate()
        {
            isValid = logic != null && logic.IsValid();
            validated = true;
        }
    }
}