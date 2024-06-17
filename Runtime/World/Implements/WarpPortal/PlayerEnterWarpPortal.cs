using ClusterVR.CreatorKit.Validator;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WarpPortal
{
    [RequireComponent(typeof(Collider))]
    [RequireIsTriggerSettings]
    public sealed class PlayerEnterWarpPortal : MonoBehaviour, IWarpPortal
    {
        [SerializeField] Transform target;
        [SerializeField] bool keepPosition;
        [SerializeField] bool keepRotation;

        public event OnEnterWarpPortalEventHandler OnEnterWarpPortalEvent;

        void OnTriggerEnter(Collider other)
        {
            if (target == null) return;

            OnEnterWarpPortalEvent?.Invoke(
                new OnEnterWarpPortalEventArgs(other.gameObject, target.position, target.rotation, keepPosition,
                    keepRotation));
        }

        void OnValidate()
        {
            foreach (var col in GetComponentsInChildren<Collider>(true))
            {
                col.isTrigger = true;
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (target != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(target.position, Quaternion.Euler(0, target.rotation.eulerAngles.y, 0), Vector3.one);
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(new Vector3(0, 0.75f, 0), new Vector3(0, 0.75f, 1));
                Gizmos.color = new Color(0, 0, 1, 0.5f);
                Gizmos.DrawSphere(new Vector3(0, 0.75f, 0), 0.75f);
            }
        }
#endif
    }
}
