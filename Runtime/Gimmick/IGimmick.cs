using System;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGimmick
    {
        Target Target { get; }
        string Key { get; }
        ParameterType ParameterType { get; }
        void Run(GimmickValue value, DateTime current);
    }
}