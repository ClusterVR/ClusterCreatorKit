using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class OnCreateItemTrigger : MonoBehaviour, IOnCreateItemTrigger, IItemTrigger, IInvoluntaryItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] triggers;
        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        public void Invoke()
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggers.Select(t => t.Convert()).ToArray()));
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
