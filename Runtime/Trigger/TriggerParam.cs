using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Trigger
{
    public class TriggerParam
    {
        public TriggerTarget Target { get; }
        public IItem SpecifiedTargetItem { get; }
        public string Key { get; }
        public ParameterType Type { get; }
        public TriggerValue Value { get; }

        public TriggerParam(TriggerTarget target, IItem specifiedTargetItem, string key, ParameterType type, TriggerValue value)
        {
            Target = target;
            SpecifiedTargetItem = specifiedTargetItem;
            Key = key;
            Type = type;
            Value = value;
        }
    }
}