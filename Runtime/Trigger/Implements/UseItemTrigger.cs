using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item), typeof(GrabbableItem)), DisallowMultipleComponent]
    public sealed class UseItemTrigger : MonoBehaviour, IUseItemTrigger, IItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] downTriggers;
        [SerializeField, ItemConstantTriggerParam] ConstantTriggerParam[] upTriggers;

        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        IEnumerable<TriggerParam> ITrigger.TriggerParams =>
            downTriggers.Concat(upTriggers).Select(t => t.Convert());

        TriggerParam[] downTriggersCache;
        TriggerParam[] upTriggersCache;

        public void Invoke(bool isDown)
        {
            var triggers = isDown
                ? downTriggersCache ?? (downTriggersCache = downTriggers.Select(t => t.Convert()).ToArray())
                : upTriggersCache ?? (upTriggersCache = upTriggers.Select(t => t.Convert()).ToArray());
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggers));
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
