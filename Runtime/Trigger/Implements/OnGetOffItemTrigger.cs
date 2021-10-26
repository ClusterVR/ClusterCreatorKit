using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(RidableItem))]
    public sealed class OnGetOffItemTrigger : MonoBehaviour, IItemTrigger
    {
        [SerializeField, HideInInspector] RidableItem ridableItem;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] triggers;

        IItem IItemTrigger.Item => ridableItem != null
            ? ridableItem.Item
            : (ridableItem = GetComponent<RidableItem>()).Item;

        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        TriggerParam[] triggersCache;

        void Start()
        {
            if (ridableItem == null)
            {
                ridableItem = GetComponent<RidableItem>();
            }
            ridableItem.OnGetOff += Invoke;
        }

        void Invoke()
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void Reset()
        {
            ridableItem = GetComponent<RidableItem>();
        }

        void OnValidate()
        {
            if (ridableItem == null || ridableItem.gameObject != gameObject)
            {
                ridableItem = GetComponent<RidableItem>();
            }
        }
    }
}
