using System;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public class GlobalTimer : MonoBehaviour, IGlobalTrigger, IGlobalGimmick
    {
        [SerializeField, ConsistentlySyncGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField] float delayTimeSeconds = 1;
        [SerializeField, GlobalOperationTriggerParam] Trigger.Implements.TriggerParam[] triggers;

        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event GlobalTriggerEventHandler TriggerEvent;

        Trigger.TriggerParam[] triggersCache;

        DateTime lastTriggerReceivedAt;
        Scheduler.Cancellation schedulerCancellation;

        public void Run(GimmickValue value, DateTime current)
        {
            if (lastTriggerReceivedAt == value.TimeStamp) return;
            lastTriggerReceivedAt = value.TimeStamp;

            var dueTime = value.TimeStamp.AddSeconds(delayTimeSeconds) - current;
            if (dueTime.TotalSeconds < -Constants.Gimmick.TriggerExpireSeconds) return;
            var expireAt = Time.realtimeSinceStartup + dueTime.TotalSeconds + Constants.Gimmick.TriggerExpireSeconds;

            void Action()
            {
                if (expireAt < Time.realtimeSinceStartup) return;
                Invoke();
            }

            schedulerCancellation?.Dispose();
            schedulerCancellation = new Scheduler.Cancellation();
            Scheduler.Schedule(dueTime, Action, schedulerCancellation);
        }

        void Invoke()
        {
            TriggerEvent?.Invoke(new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void OnDestroy()
        {
            schedulerCancellation?.Dispose();
        }

        void OnValidate()
        {
            delayTimeSeconds = Mathf.Max(delayTimeSeconds, 0.01f);
            triggers = triggers?.Select(trigger =>
            {
                return trigger.Target != TriggerTarget.Global
                    ? new Trigger.Implements.TriggerParam(TriggerTarget.Global, null, trigger.Key, trigger.Type,
                        trigger.RawValue)
                    : trigger;
            }).ToArray();
        }
    }
}