using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [Serializable]
    public sealed class VariableTriggerParam
    {
        [SerializeField] TriggerTarget target;
        [SerializeField] Item.Implements.Item specifiedTargetItem;
        [SerializeField, StateKeyString] string key;
        [SerializeField] ValueType valueType;

        public enum ValueType
        {
            Signal,
            Input
        }

        public TriggerParam ConvertWithOverrideValue(ParameterType overrideType, TriggerValue overrideValue)
        {
            switch (valueType)
            {
                case ValueType.Signal:
                    return new TriggerParam(target, specifiedTargetItem, key, ParameterType.Signal, null);
                case ValueType.Input:
                    return new TriggerParam(target, specifiedTargetItem, key, overrideType, overrideValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
