using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;
using UnityEngine;
using Random = System.Random;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class SpawnPointManager
    {
        readonly IEnumerable<ISpawnPoint> spawnPointList;

        public SpawnPointManager(IEnumerable<ISpawnPoint> spawnPointList)
        {
            this.spawnPointList = spawnPointList;
        }

        public void Respawn(PermissionType permissionType,Transform playerTransform, Transform cameraTransform)
        {
            var rnd = new Random();
            ISpawnPoint[] spawnCandidates;
            ISpawnPoint targetSpawnPoint;
            if (permissionType == PermissionType.Performer)
            {
                spawnCandidates = spawnPointList.Where(x => x.SpawnType == SpawnType.OnStage1).ToArray();
                if (spawnCandidates.Length != 0)
                {
                    targetSpawnPoint = spawnCandidates[rnd.Next(spawnCandidates.Length)];
                    playerTransform.position = targetSpawnPoint.Position;
                    cameraTransform.eulerAngles = Vector3.up * targetSpawnPoint.YRotation;
                    return;
                }
            }
            spawnCandidates = spawnPointList.Where(x => x.SpawnType == SpawnType.Entrance).ToArray();
            targetSpawnPoint = spawnCandidates[rnd.Next(spawnCandidates.Length)];
            playerTransform.position = targetSpawnPoint.Position;
            cameraTransform.eulerAngles = Vector3.up * targetSpawnPoint.YRotation;
        }
    }
}
