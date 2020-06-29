using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface ISpawnPoint
    {
        SpawnType SpawnType { get; }
        Vector3 Position { get; }
        float YRotation { get; }
        string WorldGateKey { get; }
    }
}
