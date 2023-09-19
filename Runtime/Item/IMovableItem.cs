using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IMovableItem
    {
        IItem Item { get; }
        bool IsDestroyed { get; }
        void Respawn();
        bool IsDynamic { get; }
        float Mass { get; }
        bool UseGravity { get; set; }

        Vector3 Velocity { get; }
        Vector3 AngularVelocity { get; }

        Vector3 Position { get; }
        Quaternion Rotation { get; }

        void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false);
        void EnablePhysics();

        void WarpTo(Vector3 position, Quaternion rotation);
        void SetVelocity(Vector3 velocity);
        void SetAngularVelocity(Vector3 angularVelocity);
        void AddForce(Vector3 force, ForceMode mode);
        void AddTorque(Vector3 force, ForceMode mode);
        void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode);
    }
}
