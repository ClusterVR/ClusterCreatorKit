using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [Serializable]
    public sealed class ConstantTriggerParam
    {
        [SerializeField] TriggerTarget target;
        [SerializeField] Item.Implements.Item specifiedTargetItem;
        [SerializeField, StateKeyString] string key;
        [SerializeField] ParameterType type;
        [SerializeField] Value value;

        public TriggerTarget Target => target;
        public string Key => key;
        public ParameterType Type => type;
        public Value RawValue => value;

        public TriggerParam Convert()
        {
            return new TriggerParam(target, specifiedTargetItem, key, type, value.ToTriggerValue());
        }

        public ConstantTriggerParam(TriggerTarget target, Item.Implements.Item specifiedTargetItem, string key,
            ParameterType type, Value value)
        {
            this.target = target;
            this.specifiedTargetItem = specifiedTargetItem;
            this.key = key;
            this.type = type;
            this.value = value;
        }
    }

    [Serializable]
    public sealed class Value
    {
        [SerializeField] bool boolValue;
        [SerializeField] float floatValue;
        [SerializeField] int integerValue;
        [SerializeField] Vector2 vector2Value;
        [SerializeField] Vector3 vector3Value;

        public bool BoolValue => boolValue;
        public float FloatValue => floatValue;
        public int IntegerValue => integerValue;
        public Vector2 Vector2Value => vector2Value;
        public Vector3 Vector3Value => vector3Value;

        public TriggerValue ToTriggerValue()
        {
            return new TriggerValue(BoolValue, FloatValue, IntegerValue, Vector2Value, Vector3Value);
        }
    }
}
