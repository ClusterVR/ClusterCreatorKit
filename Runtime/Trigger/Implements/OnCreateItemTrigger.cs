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

        [SerializeField][ItemTrigger] ItemTrigger[] triggers;

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
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }
    }
}