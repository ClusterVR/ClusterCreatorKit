using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.SpawnPoints
{
    [ExecuteInEditMode, DisallowMultipleComponent]
    public sealed class SpawnPoint : MonoBehaviour, ISpawnPoint
    {
        [SerializeField] SpawnType spawnType;
        [SerializeField] string worldGateKey;

        public SpawnType SpawnType => spawnType;
        public Vector3 Position => transform.position;
        public float YRotation => transform.rotation.eulerAngles.y;
        public string WorldGateKey => worldGateKey;

        Quaternion Rotation => Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        void Update()
        {
            transform.rotation = Rotation;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 0, 0.8f);
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.75f, 0.75f);
            Gizmos.color = new Color(1, 0, 0, 0.8f);
            Gizmos.DrawLine(transform.position + Vector3.up * 0.75f,
                transform.position + Vector3.up * 0.75f + Rotation * Vector3.forward);
        }
    }
}
