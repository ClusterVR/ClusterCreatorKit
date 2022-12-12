using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(MovableItemBase))]
    public sealed class OnVelocityChangedItemTrigger : MonoBehaviour, IOnReceiveOwnershipItemTrigger, IInvoluntaryItemTrigger
    {
        [SerializeField, HideInInspector] MovableItemBase movableItem;
        [SerializeField, ItemVariableTriggerParam(ParameterType.Vector3)]
        VariableTriggerParam[] triggers;
        [SerializeField] Transform space;

        IItem IItemTrigger.Item => (movableItem != null ? movableItem : movableItem = GetComponent<MovableItemBase>()).Item;

        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => TriggerParams(default);

        Vector3 previousVelocity;

        void Start()
        {
            movableItem = GetComponent<MovableItemBase>();
        }

        void Update()
        {
            if (movableItem == null)
            {
                return;
            }

            var velocity = InverseTransformDirection(movableItem.Velocity);
            if (velocity == previousVelocity)
            {
                return;
            }
            previousVelocity = velocity;
            OnValueChanged(velocity);
        }

        void IOnReceiveOwnershipItemTrigger.Invoke(bool _)
        {
            if (movableItem == null)
            {
                return;
            }

            var velocity = InverseTransformDirection(movableItem.Velocity);
            previousVelocity = velocity;
            OnValueChanged(velocity);
        }

        Vector3 InverseTransformDirection(Vector3 direction)
        {
            return space != null ? space.InverseTransformDirection(direction) : direction;
        }

        void OnValueChanged(Vector3 input)
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(TriggerParams(input).ToArray()));
        }

        IEnumerable<TriggerParam> TriggerParams(Vector3 input)
        {
            var triggerValue = new TriggerValue(input);
            return triggers.Select(t => t.ConvertWithOverrideValue(ParameterType.Vector3, triggerValue));
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItemBase>();
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject)
            {
                movableItem = GetComponent<MovableItemBase>();
            }
        }
    }
}
