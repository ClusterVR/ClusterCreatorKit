using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class OnCollideItemTrigger : MonoBehaviour, IItemTrigger, IInvoluntaryItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField] CollisionEventType collisionEventType;
        [SerializeField] CollisionType collisionType = CollisionType.Everything;
        [SerializeField, CollideItemTriggerParam] ConstantTriggerParam[] triggers;

        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        TriggerParam[] triggersCache;

        void OnCollisionEnter(Collision other)
        {
            if (collisionEventType == CollisionEventType.Enter && (collisionType & CollisionType.Collision) > 0)
            {
                Invoke(other.gameObject);
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (collisionEventType == CollisionEventType.Exit && (collisionType & CollisionType.Collision) > 0)
            {
                Invoke(other.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (collisionEventType == CollisionEventType.Enter && (collisionType & CollisionType.Trigger) > 0)
            {
                Invoke(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (collisionEventType == CollisionEventType.Exit && (collisionType & CollisionType.Trigger) > 0)
            {
                Invoke(other.gameObject);
            }
        }

        void Invoke(GameObject collidedObject)
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray()),
                    collidedObject));
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
