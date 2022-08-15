using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IMovableItem
    {
        IItem Item { get; }
        bool IsDestroyed { get; }
        void Respawn();

        Vector3 Position { get; }
        Quaternion Rotation { get; }

        void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false);
        void EnablePhysics();
    }
}
