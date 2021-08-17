using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.DespawnHeights
{
    [ExecuteInEditMode, DisallowMultipleComponent]
    public sealed class DespawnHeight : MonoBehaviour, IDespawnHeight
    {
        public float Height => transform.position.y;

        void Update()
        {
            transform.position = transform.position.y * Vector3.up;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.8f);
            Gizmos.DrawCube(transform.position.y * Vector3.up, new Vector3(50, 0.001f, 50));
        }
    }
}
