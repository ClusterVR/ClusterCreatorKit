using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public sealed class GlobalLogic : MonoBehaviour, IGlobalLogic
    {
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField, GlobalLogic] Logic logic;

        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event RunGlobalLogicEventHandler OnRunGlobalLogic;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => logic.GetTriggerParams();
        Logic ILogic.Logic => logic;

        DateTime lastTriggeredAt;
        bool validated;
        bool isValid;

        public void Run(GimmickValue value, DateTime current)
        {
            if (!validated)
            {
                Validate();
            }
            if (!isValid)
            {
                return;
            }
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }

            OnRunGlobalLogic?.Invoke(new RunGlobalLogicEventArgs(logic));
        }

        void Validate()
        {
            isValid = logic != null && logic.IsValid();
            validated = true;
        }
    }
}
