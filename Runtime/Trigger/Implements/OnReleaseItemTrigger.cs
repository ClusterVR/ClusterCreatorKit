using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(GrabbableItem))]
    public sealed class OnReleaseItemTrigger : MonoBehaviour, IItemTrigger
    {
        [SerializeField, HideInInspector] GrabbableItem grabbableItem;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] triggers;

        IItem IItemTrigger.Item => grabbableItem != null
            ? grabbableItem.Item
            : (grabbableItem = GetComponent<GrabbableItem>()).Item;

        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        TriggerParam[] triggersCache;

        void Start()
        {
            if (grabbableItem == null)
            {
                grabbableItem = GetComponent<GrabbableItem>();
            }
            grabbableItem.OnReleased += Invoke;
        }

        void Invoke()
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void Reset()
        {
            grabbableItem = GetComponent<GrabbableItem>();
        }

        void OnValidate()
        {
            if (grabbableItem == null || grabbableItem.gameObject != gameObject)
            {
                grabbableItem = GetComponent<GrabbableItem>();
            }
        }
    }
}
