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
    public sealed class GlobalTimer : MonoBehaviour, IGlobalTrigger, IGlobalGimmick, IRerunOnReceiveOwnershipInvoluntaryGimmick
    {
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField, Min(0.01f)] float delayTimeSeconds = 1;
        [SerializeField, GlobalOperationTriggerParam] ConstantTriggerParam[] triggers;

        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event GlobalTriggerEventHandler TriggerEvent;
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
            var expireAt = Time.realtimeSinceStartup + dueTime.TotalSeconds +
                TriggerGimmick.OwnershipExpireExpectedSeconds;

            void Action()
            {
                if (expireAt < Time.realtimeSinceStartup)
                {
                    return;
                }
                Invoke();
            }

            schedulerCancellation?.Dispose();
            schedulerCancellation = new Scheduler.Cancellation();
            Scheduler.Schedule(dueTime, Action, schedulerCancellation);
        }

        void IRerunnableGimmick.Rerun(GimmickValue value, DateTime current)
        {
            var executeAt = value.TimeStamp.AddSeconds(delayTimeSeconds);
            if (current - TimeSpan.FromSeconds(TriggerGimmick.OwnershipExpireExpectedSeconds) < executeAt &&
                executeAt < current)
            {
                schedulerCancellation?.Dispose();
                Invoke();
            }
        }

        void Invoke()
        {
            TriggerEvent?.Invoke(new TriggerEventArgs(triggersCache ??
                (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void OnDestroy()
        {
            schedulerCancellation?.Dispose();
        }

        void OnValidate()
        {
            triggers = triggers?.Select(trigger =>
            {
                return trigger.Target != TriggerTarget.Global
                    ? new ConstantTriggerParam(TriggerTarget.Global, null, trigger.Key, trigger.Type,
                        trigger.RawValue)
                    : trigger;
            }).ToArray();
        }
    }
}
