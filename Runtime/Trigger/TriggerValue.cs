namespace ClusterVR.CreatorKit.Trigger
{
    public class TriggerValue
    {
        public bool BoolValue { get; }
        public float FloatValue { get; }
        public int IntegerValue { get; }

        public TriggerValue(bool boolValue, float floatValue, int integerValue)
        {
            BoolValue = boolValue;
            FloatValue = floatValue;
            IntegerValue = integerValue;
        }

        public TriggerValue(bool boolValue)
        {
            BoolValue = boolValue;
        }

        public TriggerValue(float floatValue)
        {
            FloatValue = floatValue;
        }

        public TriggerValue(int integerValue)
        {
            IntegerValue = integerValue;
        }
    }
}