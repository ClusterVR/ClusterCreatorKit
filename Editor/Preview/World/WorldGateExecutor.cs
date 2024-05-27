using System.Collections.Generic;
using ClusterVR.CreatorKit.Translation;
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
            var readableKey = string.IsNullOrEmpty(e.Key) ? TranslationTable.cck_empty_world_gate_key : e.Key;
            var message =
                TranslationUtility.GetMessage(TranslationTable.cck_upload_world_move_to_id, e.WorldOrEventId, SpawnType.WorldGateDestination, readableKey);
            Debug.Log(message);
        }
    }
}
