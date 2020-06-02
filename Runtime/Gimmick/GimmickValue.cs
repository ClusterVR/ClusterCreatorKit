using System;

namespace ClusterVR.CreatorKit.Gimmick
{
    public class GimmickValue
    {
        public bool BoolValue { get; }
        public float FloatValue { get; }
        public int IntegerValue { get; }
        public DateTime TimeStamp { get; }

        public GimmickValue(bool value) => BoolValue = value;
        public GimmickValue(float value) => FloatValue = value;
        public GimmickValue(int value) => IntegerValue = value;
        public GimmickValue(DateTime value) => TimeStamp = value;
    }
}