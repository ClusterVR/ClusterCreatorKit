using System;
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
        [SerializeField, ItemTrigger] ItemTrigger[] triggers;

        public override IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        void Start()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        public void Invoke()
        {
            foreach (var trigger in triggers)
            {
                TriggerEvent?.Invoke(this, new TriggerEventArgs(trigger.Target, trigger.SpecifiedTargetItem, null, trigger.Key, trigger.Type, trigger.Value));
            }
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