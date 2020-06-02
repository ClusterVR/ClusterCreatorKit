using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item), typeof(GrabbableItem)), DisallowMultipleComponent]
    public class UseItemTrigger : MonoBehaviour, IUseItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemTrigger] ItemTrigger[] downTriggers;
        [SerializeField, ItemTrigger] ItemTrigger[] upTriggers;

        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        public void Invoke(bool isDown)
        {
            foreach (var trigger in isDown ? downTriggers : upTriggers)
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