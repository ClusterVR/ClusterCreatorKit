
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IGrabbableItem : IInteractableItem
    {
        IMovableItem MovableItem { get; }
        Transform Grip { get; }
        void OnGrab();
        void OnRelease();
    }
}
