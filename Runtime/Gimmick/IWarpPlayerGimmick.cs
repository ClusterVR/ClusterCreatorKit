using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IWarpPlayerGimmick : IPlayerEffect
    {
        Vector3 TargetPosition { get; }
        Quaternion TargetRotation { get; }
        bool KeepPosition { get; }
        bool KeepRotation { get; }
    }
}