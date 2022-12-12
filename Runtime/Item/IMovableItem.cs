using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IMovableItem
    {
        IItem Item { get; }
        bool IsDestroyed { get; }
        void Respawn();
        bool IsDynamic { get; }

        Vector3 Position { get; }
        Quaternion Rotation { get; }

        void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false);
        void EnablePhysics();

        void WarpTo(Vector3 position, Quaternion rotation);
    }
}
