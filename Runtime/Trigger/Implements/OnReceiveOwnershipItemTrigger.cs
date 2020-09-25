using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public class OnReceiveOwnershipItemTrigger : MonoBehaviour, IOnReceiveOwnershipItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField] EventType eventType = EventType.Always;
        [SerializeField, ItemTriggerParam] TriggerParam[] triggers;

        [Flags]
        enum EventType
        {
            Voluntary = 1 << 0,
            Involuntary = 1 << 1,
            Always = ~0
        }
        
        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        Trigger.TriggerParam[] triggersCache;

        void IOnReceiveOwnershipItemTrigger.Invoke(bool voluntary)
        {
            var type = voluntary ? EventType.Voluntary : EventType.Involuntary;
            if ((type & eventType) > 0) TriggerEvent?.Invoke(this, new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }
    }
}