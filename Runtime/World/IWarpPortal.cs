using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IWarpPortal
    {
        event OnEnterWarpPortalEventHandler OnEnterWarpPortalEvent;
    }

    public delegate void OnEnterWarpPortalEventHandler(OnEnterWarpPortalEventArgs e);

    public sealed class OnEnterWarpPortalEventArgs : EventArgs
    {
        public GameObject Target { get; }
        public Vector3 ToPosition { get; }
        public Quaternion ToRotation { get; }
        public bool KeepPosition { get; }
        public bool KeepRotation { get; }

        public OnEnterWarpPortalEventArgs(GameObject target, Vector3 toPosition, Quaternion toRotation,
            bool keepPosition, bool keepRotation)
        {
            Target = target;
            ToPosition = toPosition;
            ToRotation = toRotation;
            KeepPosition = keepPosition;
            KeepRotation = keepRotation;
        }
    }
}
