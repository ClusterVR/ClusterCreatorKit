using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Trigger
{
    public sealed class TriggerParam
    {
        public TriggerTarget Target { get; }
        public IItem SpecifiedTargetItem { get; }
        public string RawKey { get; }
        public ParameterType ParameterType { get; }
        public TriggerValue Value { get; }

        public TriggerParam(TriggerTarget target, IItem specifiedTargetItem, string rawKey, ParameterType parameterType,
            TriggerValue value)
        {
            Target = target;
            SpecifiedTargetItem = specifiedTargetItem;
            RawKey = rawKey;
            ParameterType = parameterType;
            Value = value;
        }
    }

    public static class TriggerParamExtensions
    {
        public static IEnumerable<KeyValuePair<string, StateValue>> ToTriggerStates(this TriggerParam triggerParam, string keyPrefix, StateValue signal)
        {
            return triggerParam.ToStateValueSet(signal).ToTriggerStates(keyPrefix, triggerParam.RawKey);
        }

        public static IStateValueSet ToStateValueSet(this TriggerParam triggerParam, StateValue signal)
        {
            switch (triggerParam.ParameterType)
            {
                case ParameterType.Signal:
                    return new SignalStateValueSet(signal);
                case ParameterType.Bool:
                    return new BoolStateValueSet(triggerParam.Value.BoolValue);
                case ParameterType.Float:
                    return new FloatStateValueSet(triggerParam.Value.FloatValue);
                case ParameterType.Integer:
                    return new IntegerStateValueSet(triggerParam.Value.IntegerValue);
                case ParameterType.Vector2:
                    return new Vector2StateValueSet(triggerParam.Value.Vector2Value);
                case ParameterType.Vector3:
                    return new Vector3StateValueSet(triggerParam.Value.Vector3Value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IEnumerable<string> GetKeyWithFieldNames(this TriggerParam triggerParam)
        {
            return triggerParam.ToTriggerStates("", StateValue.Default)
                .Select(s => s.Key);
        }
    }
}
