using System;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGimmick
    {
        GimmickTarget Target { get; }
        string Key { get; }
        ItemId ItemId { get; }
        ParameterType ParameterType { get; }
        void Run(GimmickValue value, DateTime current);
    }
}