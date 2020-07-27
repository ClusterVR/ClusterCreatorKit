using System;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGimmick
    {
        GimmickTarget Target { get; }
        string Key { get; }
        ParameterType ParameterType { get; }
        void Run(GimmickValue value, DateTime current);
    }
}