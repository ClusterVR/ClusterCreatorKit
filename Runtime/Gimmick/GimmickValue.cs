using System;

namespace ClusterVR.CreatorKit.Gimmick
{
    public class GimmickValue
    {
        public bool BoolValue { get; }
        public float FloatValue { get; }
        public int IntegerValue { get; }
        public DateTime TimeStamp { get; }

        public GimmickValue(ParameterType type, StateValue value)
        {
            switch (type)
            {
                case ParameterType.Signal:
                    TimeStamp = value.ToDateTime();
                    return;
                case ParameterType.Bool:
                    BoolValue = value.ToBool();
                    return;
                case ParameterType.Integer:
                    IntegerValue = value.ToInt();
                    return;
                case ParameterType.Float:
                    FloatValue = value.ToFloat();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
