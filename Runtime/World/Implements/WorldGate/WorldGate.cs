using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WorldGate
{
    [RequireComponent(typeof(Collider))]
    public sealed class WorldGate : MonoBehaviour, IWorldGate
    {
        [SerializeField, Tooltip("ワールドまたはイベントのId")] string worldOrEventId;
        [SerializeField, Tooltip("ワープ先のSpawnPointのKey(任意)")] string key;
        [SerializeField, Tooltip("移動前に確認UIを表示するか")] bool confirmTransition;

        public event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;
        public event OnExitWorldGateEventHandler OnExitWorldGateEvent;

        void OnTriggerEnter(Collider other)
        {
            OnEnterWorldGateEvent?.Invoke(new OnEnterWorldGateEventArgs(worldOrEventId, other.gameObject, key, confirmTransition));
        }

        void OnTriggerExit(Collider other)
        {
            OnExitWorldGateEvent?.Invoke(new OnExitWorldGateEventArgs(other.gameObject));
        }

        void OnValidate()
        {
            foreach (var col in GetComponentsInChildren<Collider>(true))
            {
                col.isTrigger = true;
            }
        }

        void Reset()
        {
            confirmTransition = true;
        }
    }
}
