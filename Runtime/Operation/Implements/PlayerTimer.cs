using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public sealed class PlayerTimer : MonoBehaviour, IPlayerTrigger, IPlayerGimmick
    {
        [SerializeField] PlayerGimmickKey key;
        [SerializeField, Min(0.01f)] float delayTimeSeconds = 1;
        [SerializeField, PlayerOperationTriggerParam] ConstantTriggerParam[] triggers;

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event PlayerTriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        TriggerParam[] triggersCache;

        DateTime lastTriggerReceivedAt;
        Scheduler.Cancellation schedulerCancellation;

        public void Run(GimmickValue value, DateTime current)
        {
            if (lastTriggerReceivedAt == value.TimeStamp)
            {
                return;
            }
            lastTriggerReceivedAt = value.TimeStamp;

            var dueTime = value.TimeStamp.AddSeconds(delayTimeSeconds) - current;
            if (dueTime.TotalSeconds < -TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }

            schedulerCancellation?.Dispose();
            schedulerCancellation = new Scheduler.Cancellation();
            Scheduler.Schedule(dueTime, Invoke, schedulerCancellation);
        }

        void Invoke()
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void OnDestroy()
        {
            schedulerCancellation?.Dispose();
        }

        void OnValidate()
        {
            triggers = triggers?.Select(trigger =>
            {
                return trigger.Target != TriggerTarget.Player
                    ? new ConstantTriggerParam(TriggerTarget.Player, null, trigger.Key, trigger.Type,
                        trigger.RawValue)
                    : trigger;
            }).ToArray();
        }
    }
}
