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
    [RequireComponent(typeof(Item.Implements.Item))]
    public class ItemTimer : MonoBehaviour, IItemTrigger, IItemGimmick
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField] float delayTimeSeconds = 1;
        [SerializeField, ItemTimerTriggerParam] Trigger.Implements.TriggerParam[] triggers;

        IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        IItem IItemTrigger.Item => Item;
        ItemId IGimmick.ItemId => Item.Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event TriggerEventHandler TriggerEvent;

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
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void OnDestroy()
        {
            schedulerCancellation?.Dispose();
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
            delayTimeSeconds = Mathf.Max(delayTimeSeconds, 0.01f);
            triggers = triggers?.Select(trigger =>
            {
                return trigger.Target != TriggerTarget.Item ?
                    new Trigger.Implements.TriggerParam(TriggerTarget.Item, null, trigger.Key, trigger.Type, trigger.RawValue) :
                    trigger;
            }).ToArray();
        }
    }
}