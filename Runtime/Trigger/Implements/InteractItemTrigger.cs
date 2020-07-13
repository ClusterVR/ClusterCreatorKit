using System;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public class InteractItemTrigger : InteractableItem, IInteractItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemTriggerParam] TriggerParam[] triggers;

        public override IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        Trigger.TriggerParam[] triggersCache;

        void Start()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        public void Invoke()
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray())));
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }
    }
}