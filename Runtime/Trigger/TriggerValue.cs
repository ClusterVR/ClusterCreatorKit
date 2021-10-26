using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger
{
    public sealed class TriggerValue
    {
        public bool BoolValue { get; }
        public float FloatValue { get; }
        public int IntegerValue { get; }
        public Vector2 Vector2Value { get; }
        public Vector3 Vector3Value { get; }

        public TriggerValue()
        {
        }

        public TriggerValue(bool boolValue, float floatValue, int integerValue, Vector2 vector2Value, Vector3 vector3Value)
        {
            BoolValue = boolValue;
            FloatValue = floatValue;
            IntegerValue = integerValue;
            Vector2Value = vector2Value;
            Vector3Value = vector3Value;
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

        public TriggerValue(Vector2 vector2Value)
        {
            Vector2Value = vector2Value;
        }

        public TriggerValue(Vector3 vector3Value)
        {
            Vector3Value = vector3Value;
        }
    }
}
