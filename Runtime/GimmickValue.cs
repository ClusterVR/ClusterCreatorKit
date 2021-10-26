using System;
using UnityEngine;

namespace ClusterVR.CreatorKit
{
    public sealed class GimmickValue
    {
        public bool BoolValue { get; }
        public float FloatValue { get; }
        public int IntegerValue { get; }
        public DateTime TimeStamp { get; }
        public Vector2 Vector2Value { get; }
        public Vector3 Vector3Value { get; }

        public GimmickValue(bool value)
        {
            BoolValue = value;
        }

        public GimmickValue(float floatValue)
        {
            FloatValue = floatValue;
        }

        public GimmickValue(int integerValue)
        {
            IntegerValue = integerValue;
        }

        public GimmickValue(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }

        public GimmickValue(Vector2 vector2Value)
        {
            Vector2Value = vector2Value;
        }

        public GimmickValue(Vector3 vector3Value)
        {
            Vector3Value = vector3Value;
        }
    }
}
