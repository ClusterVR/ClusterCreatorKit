using System.Collections.Generic;
using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class WorldGateExecutor
    {
        public WorldGateExecutor(IEnumerable<IWorldGate> worldGates)
        {
            foreach (var worldGate in worldGates)
            {
                worldGate.OnEnterWorldGateEvent += OnEnterWorldGate;
            }
        }

        static void OnEnterWorldGate(OnEnterWorldGateEventArgs e)
        {
            if (!e.EnterObject.CompareTag("Player") || string.IsNullOrEmpty(e.WorldOrEventId))
            {
                return;
            }
            var readableKey = string.IsNullOrEmpty(e.Key) ? "空" : e.Key;
            var message =
                $"ワールドをアップロードすると以下のIdのワールドまたはイベントに移動します。\n{e.WorldOrEventId}\n移動先のSpawnPointにTypeが{SpawnType.WorldGateDestination}でWorldGateKeyが{readableKey}のものがあればそこに出現します。";
            Debug.Log(message);
        }
    }
}
