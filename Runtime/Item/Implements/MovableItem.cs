using System.Collections;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item), typeof(Rigidbody)), DisallowMultipleComponent]
    public sealed class MovableItem : MonoBehaviour, IMovableItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, HideInInspector] Rigidbody rb;

        public IItem Item => item;
        public Vector3 Position => rb.position;
        public Quaternion Rotation => rb.rotation;

        bool isInitialized;
        bool initialIsKinematic;

        void CacheInitialValue()
        {
            if (isInitialized) return;
            initialIsKinematic = rb.isKinematic;
            isInitialized = true;
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            CacheInitialValue();
            rb.isKinematic = true;
            StartCoroutine(MoveOnFixedUpdate(position, rotation));
        }

        IEnumerator MoveOnFixedUpdate(Vector3 position, Quaternion rotation)
        {
            yield return new WaitForFixedUpdate();
            rb.MovePosition(position);
            rb.MoveRotation(rotation);
        }

        public void EnablePhysics()
        {
            CacheInitialValue();
            rb.isKinematic = initialIsKinematic;
        }

        void Reset()
        {
            item = GetComponent<Item>();
            rb = GetComponent<Rigidbody>();
        }
    }
}