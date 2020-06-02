using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [Serializable]
    public class ItemTrigger
    {
        [SerializeField] ItemTriggerTarget target;
        [SerializeField] Item.Implements.Item specifiedTargetItem;
        [SerializeField] string key;
        [SerializeField] ParameterType type;
        [SerializeField] Value value;

        public ItemTriggerTarget Target => target;
        public IItem SpecifiedTargetItem => specifiedTargetItem;
        public string Key => key;
        public ParameterType Type => type;
        public TriggerValue Value => value.ToTriggerValue();
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