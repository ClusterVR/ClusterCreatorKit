using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IWarpPlayerEffect : IPlayerEffect
    {
        Vector3 TargetPosition { get; }
        Quaternion TargetRotation { get; }
        bool KeepPosition { get; }
        bool KeepRotation { get; }
    }
}
