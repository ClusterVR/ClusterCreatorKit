using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class OnReceiveOwnershipItemTrigger : MonoBehaviour, IOnReceiveOwnershipItemTrigger, IInvoluntaryItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField] EventType eventType = EventType.Always;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] triggers;

        [Flags]
        enum EventType
        {
            Voluntary = 1 << 0,
            Involuntary = 1 << 1,
            Always = ~0
        }

        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        TriggerParam[] triggersCache;

        void IOnReceiveOwnershipItemTrigger.Invoke(bool voluntary)
        {
            var type = voluntary ? EventType.Voluntary : EventType.Involuntary;
            if ((type & eventType) > 0)
            {
                TriggerEvent?.Invoke(this,
                    new TriggerEventArgs(triggersCache ??
                        (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
            }
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject)
            {
                item = GetComponent<Item.Implements.Item>();
            }
        }
    }
}
