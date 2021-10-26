using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class InteractItemTrigger : ContactableItem, IInteractableItem, IItemTrigger

    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] triggers;

        public override IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());
        TriggerParam[] triggersCache;

        void Start()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        public void Invoke()
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
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
