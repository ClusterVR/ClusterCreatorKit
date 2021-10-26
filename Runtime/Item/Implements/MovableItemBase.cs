using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public abstract class MovableItemBase : MonoBehaviour
    {
        public abstract IItem Item { get; }
        public abstract Vector3 Velocity { get; }
        public abstract Vector3 AngularVelocity { get; }
        public abstract void WarpTo(Vector3 position, Quaternion rotation);
    }
}
