using System;
using UnityEngine;

namespace ClusterVR.CreatorKit
{
    [Serializable]
    public sealed class StateValue
    {
        [SerializeField] double doubleValue;

        public static readonly StateValue Default = new StateValue(0);

        public StateValue(bool value)
        {
            doubleValue = value ? 1.0 : 0.0;
        }

        public StateValue(int value)
        {
            doubleValue = value;
        }

        public StateValue(float value)
        {
            doubleValue = value;
        }

        public StateValue(double value)
        {
            doubleValue = value;
        }

        public StateValue(DateTime value)
        {
            doubleValue = DateTimeToDoubleMilliSecFromUNIXEpoch(value);
        }

        public bool ToBool()
        {
            return doubleValue > 0;
        }

        public int ToInt()
        {
            return (int) doubleValue;
        }

        public float ToFloat()
        {
            return (float) doubleValue;
        }

        public double ToDouble()
        {
            return doubleValue;
        }

        public DateTime ToDateTime()
        {
            return DoubleMilliSecFromUNIXEpochToDateTime(doubleValue);
        }

        const long Epoch = 62135596800000L;

        static double DateTimeToDoubleMilliSecFromUNIXEpoch(DateTime value)
        {
            return value.ToUniversalTime().Ticks / 10000.0 - Epoch;
        }

        static DateTime DoubleMilliSecFromUNIXEpochToDateTime(double value)
        {
            return new DateTime((long) ((value + Epoch) * 10000), DateTimeKind.Utc);
        }
    }
}
