using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IGrabbableItem : IContactableItem
    {
        IMovableItem MovableItem { get; }
        Transform Grip { get; }
        void OnGrab();
        void OnRelease();
    }
}
