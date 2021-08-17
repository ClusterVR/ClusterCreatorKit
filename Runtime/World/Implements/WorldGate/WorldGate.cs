using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WorldGate
{
    [RequireComponent(typeof(Collider))]
    public sealed class WorldGate : MonoBehaviour, IWorldGate
    {
        [SerializeField, Tooltip("ワールドまたはイベントのId")] string worldOrEventId;
        [SerializeField, Tooltip("ワープ先のSpawnPointのKey(任意)")] string key;

        public event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;

        void OnTriggerEnter(Collider other)
        {
            OnEnterWorldGateEvent?.Invoke(new OnEnterWorldGateEventArgs(worldOrEventId, other.gameObject, key));
        }

        void OnValidate()
        {
            foreach (var col in GetComponentsInChildren<Collider>(true))
            {
                col.isTrigger = true;
            }
        }
    }
}
