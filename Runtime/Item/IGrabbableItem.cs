using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IGrabbableItem : IContactableItem
    {
        IMovableItem MovableItem { get; }
        bool IsDestroyed { get; }
        Transform Grip { get; }
        void OnGrab();
        void OnRelease();

        event Action OnGrabbed;
        event Action OnReleased;
    }
}
