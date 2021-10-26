using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(Item.Implements.Item), typeof(RidableItem)), DisallowMultipleComponent]
    public sealed class SteerItemTrigger : MonoBehaviour, ISteerItemTrigger
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField] SteerSpace firstPersonMoveSpace;
        [SerializeField] SteerSpace thirdPersonMoveSpace;
        [SerializeField, ItemVariableTriggerParamAttribute(ParameterType.Vector2)]
        VariableTriggerParam[] moveInputTriggers;
        [SerializeField, ItemVariableTriggerParamAttribute(ParameterType.Float)]
        VariableTriggerParam[] additionalAxisInputTriggers = { };

        IItem IItemTrigger.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        public event TriggerEventHandler TriggerEvent;

        IEnumerable<TriggerParam> ITrigger.TriggerParams =>
            MoveInputTriggerParams(default).Concat(AdditionalAxisInputTriggerParams(default));

        SteerSpace ISteerItemTrigger.FirstPersonMoveSpace => firstPersonMoveSpace;
        SteerSpace ISteerItemTrigger.ThirdPersonMoveSpace => thirdPersonMoveSpace;
        bool ISteerItemTrigger.HasMoveInputTriggers() => moveInputTriggers.Any();
        bool ISteerItemTrigger.HasAdditionalAxisInputTriggers() => additionalAxisInputTriggers.Any();

        void ISteerItemTrigger.OnMoveInputValueChanged(Vector2 input)
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(MoveInputTriggerParams(input).ToArray()));
        }

        void ISteerItemTrigger.OnAdditionalAxisInputValueChanged(float input)
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(AdditionalAxisInputTriggerParams(input).ToArray()));
        }

        IEnumerable<TriggerParam> MoveInputTriggerParams(Vector2 input)
        {
            var triggerValue = new TriggerValue(input);
            return moveInputTriggers.Select(t => t.ConvertWithOverrideValue(ParameterType.Vector2, triggerValue));
        }

        IEnumerable<TriggerParam> AdditionalAxisInputTriggerParams(float input)
        {
            var triggerValue = new TriggerValue(input);
            return additionalAxisInputTriggers.Select(t => t.ConvertWithOverrideValue(ParameterType.Float, triggerValue));
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
