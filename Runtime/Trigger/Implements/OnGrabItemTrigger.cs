using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(GrabbableItem))]
    public class OnGrabItemTrigger : MonoBehaviour, IItemTrigger
    {
        [SerializeField, HideInInspector] GrabbableItem grabbableItem;
        [SerializeField, ItemTrigger] ItemTrigger[] triggers;

        IItem IItemTrigger.Item => grabbableItem != null ? grabbableItem.Item : (grabbableItem = GetComponent<GrabbableItem>()).Item;
        public event TriggerEventHandler TriggerEvent;

        void Start()
        {
            if (grabbableItem == null) grabbableItem = GetComponent<GrabbableItem>();
            grabbableItem.OnGrabbed += Invoke;
        }

        void Invoke()
        {
            foreach (var trigger in triggers)
            {
                TriggerEvent?.Invoke(this, new TriggerEventArgs(trigger.Target, trigger.SpecifiedTargetItem, null, trigger.Key, trigger.Type, trigger.Value));
            }
        }

        void Reset()
        {
            grabbableItem = GetComponent<GrabbableItem>();
        }

        void OnValidate()
        {
            if (grabbableItem == null || grabbableItem.gameObject != gameObject) grabbableItem = GetComponent<GrabbableItem>();
        }
    }
}