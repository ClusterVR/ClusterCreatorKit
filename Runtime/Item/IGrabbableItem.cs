using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IGrabbableItem : IContactableItem
    {
        IMovableItem MovableItem { get; }
        bool IsDestroyed { get; }
        Transform Grip { get; }
        void OnGrab(bool isLeftHand);
        void OnRelease(bool isLeftHand);

        event Action<bool> OnGrabbed;
        event Action<bool> OnReleased;
    }
}
