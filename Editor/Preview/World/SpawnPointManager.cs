using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;
using Random = System.Random;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class SpawnPointManager
    {
        readonly SpawnPoint[] spawnPoints;

        public SpawnPointManager(IEnumerable<ISpawnPoint> spawnPoints)
        {
            this.spawnPoints = spawnPoints.Select(s => new SpawnPoint(s.SpawnType, s.Position, s.YRotation)).ToArray();
        }

        public SpawnPoint GetRespawnPoint(PermissionType permissionType)
        {
            var rnd = new Random();
            SpawnPoint[] spawnCandidates;
            if (permissionType == PermissionType.Performer)
            {
                spawnCandidates = spawnPoints.Where(x => x.SpawnType == SpawnType.OnStage1).ToArray();
                if (spawnCandidates.Length != 0)
                {
                    return spawnCandidates[rnd.Next(spawnCandidates.Length)];
                }
            }

            spawnCandidates = spawnPoints.Where(x => x.SpawnType == SpawnType.Entrance).ToArray();
            return spawnCandidates[rnd.Next(spawnCandidates.Length)];
        }
    }
}
