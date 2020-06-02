using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IMovableItem
    {
        IItem Item { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        // Physics を無効化する / 最大で毎フレーム呼ばれる
        void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false);
        // Physics を使う Item の場合は有効化する
        void EnablePhysics();
        void Respawn();
    }
}