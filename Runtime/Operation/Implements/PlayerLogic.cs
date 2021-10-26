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
    public sealed class PlayerLogic : MonoBehaviour, IPlayerLogic
    {
        [SerializeField] PlayerGimmickKey key;
        [SerializeField, PlayerLogic] Logic logic;

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event RunPlayerLogicEventHandler OnRunPlayerLogic;
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

            OnRunPlayerLogic?.Invoke(new RunPlayerLogicEventArgs(logic));
        }

        void Validate()
        {
            isValid = logic != null && logic.IsValid();
            validated = true;
        }
    }
}
