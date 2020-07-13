using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public class OnCreateItemTrigger : MonoBehaviour, IOnCreateItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        [SerializeField] [ItemTriggerParam] TriggerParam[] triggers;

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
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }
    }
}