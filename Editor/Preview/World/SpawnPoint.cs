using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public readonly struct SpawnPoint
    {
        public readonly SpawnType SpawnType;
        public readonly Vector3 Position;
        public readonly float YRotation;

        public SpawnPoint(SpawnType spawnType, Vector3 position, float yRotation)
        {
            this.SpawnType = spawnType;
            this.Position = position;
            this.YRotation = yRotation;
        }
    }
}