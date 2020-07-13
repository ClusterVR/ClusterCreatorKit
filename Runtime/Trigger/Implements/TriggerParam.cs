using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [Serializable]
    public class TriggerParam
    {
        [SerializeField] TriggerTarget target;
        [SerializeField] Item.Implements.Item specifiedTargetItem;
        [SerializeField] string key;
        [SerializeField] ParameterType type;
        [SerializeField] Value value;

        public TriggerTarget Target => target;
        public IItem SpecifiedTargetItem => specifiedTargetItem;
        public string Key => key;
        public ParameterType Type => type;
        public Value RawValue => value;

        public Trigger.TriggerParam Convert()
        {
            return new Trigger.TriggerParam(target, specifiedTargetItem, key, type, value.ToTriggerValue());
        }

        public TriggerParam(TriggerTarget target, Item.Implements.Item specifiedTargetItem, string key, ParameterType type, Value value)
        {
            this.target = target;
            this.specifiedTargetItem = specifiedTargetItem;
            this.key = key;
            this.type = type;
            this.value = value;
        }
    }

    [Serializable]
    public class Value
    {
        [SerializeField] bool boolValue;
        [SerializeField] float floatValue;
        [SerializeField] int integerValue;

        public bool BoolValue => boolValue;
        public float FloatValue => floatValue;
        public int IntegerValue => integerValue;

        public TriggerValue ToTriggerValue()
        {
            return new TriggerValue(BoolValue, FloatValue, IntegerValue);
        }
    }
}