using UnityEngine;
using System.Collections.Generic;
using ClusterVR.CreatorKit.World;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Preview.WarpPortal
{
    public sealed class WorldGateEventExecutor : MonoBehaviour
    {
        readonly List<IWorldGate> worldGates = new List<IWorldGate>();

        void Start()
        {
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                var gates = rootGameObject.GetComponentsInChildren<IWorldGate>(true);
                worldGates.AddRange(gates);
            }

            foreach (var worldGate in worldGates)
            {
                worldGate.OnEnterWorldGateEvent += ShowLog;
            }
        }

        void ShowLog(OnEnterWorldGateEventArgs e)
        {
            var readableKey = string.IsNullOrEmpty(e.Key) ? "空" : e.Key;
            var message = $"ワールドをアップロードすると以下のIdのワールドまたはイベントに移動します。\n{e.WorldOrEventId}\n移動先のSpawnPointにTypeが{SpawnType.WorldGateDestination}でWorldGateKeyが{readableKey}のものがあればそこに出現します。";
            Debug.Log(message);
        }

        void OnDestroy()
        {
            foreach (var worldGate in worldGates)
            {
                worldGate.OnEnterWorldGateEvent -= ShowLog;
            }
        }
    }
}