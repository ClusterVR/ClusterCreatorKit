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

        enum State
        {
            Free, Controlled, Interpolated
        }

        bool isInitialized;
        bool initialIsKinematic;

        State state = State.Free;
        Vector3 targetPosition;
        Quaternion targetRotation;
        Vector3 currentPosition;
        Quaternion currentRotation;
        float setAt;
        float interpolateDurationSeconds;

        void CacheInitialValue()
        {
            if (isInitialized) return;
            initialIsKinematic = rb.isKinematic;
            isInitialized = true;
        }

        void Start()
        {
            StartCoroutine(Rewind());
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            CacheInitialValue();
            rb.isKinematic = true;
            if (state == State.Free)
            {
                currentPosition = position;
                currentRotation = rotation;
            }
            else
            {
                currentPosition = transform.position;
                currentRotation = transform.rotation;
            }
            targetPosition = position;
            targetRotation = rotation;
            setAt = Time.realtimeSinceStartup;
            interpolateDurationSeconds = Time.deltaTime;
            state = State.Controlled;
        }

        void LateUpdate()
        {
            if (state == State.Controlled)
            {
                transform.position = targetPosition;
                transform.rotation = targetRotation;
            }
        }

        IEnumerator Rewind()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (state == State.Controlled)
                {
                    transform.position = currentPosition;
                    transform.rotation = currentRotation;
                }
            }
        }

        void FixedUpdate()
        {
            if (state != State.Free)
            {
                var interpolateRate = (Time.realtimeSinceStartup - setAt) / interpolateDurationSeconds;
                rb.MovePosition(Vector3.Lerp(currentPosition, targetPosition, interpolateRate));
                rb.MoveRotation(Quaternion.Slerp(currentRotation, targetRotation, interpolateRate));
                state = State.Interpolated;
            }
        }

        public void EnablePhysics()
        {
            if (state == State.Free) return;
            rb.isKinematic = initialIsKinematic;
            rb.velocity = (targetPosition - currentPosition) / interpolateDurationSeconds;
            rb.angularVelocity = GetAngularVelocity(currentRotation, targetRotation, interpolateDurationSeconds);
            state = State.Free;
        }

        static Vector3 GetAngularVelocity(Quaternion from, Quaternion to, float deltaTime)
        {
            (Quaternion.Inverse(from) * to).ToAngleAxis(out var deltaAngle, out var deltaAngleAxis);
            if (deltaAngle > 180f) deltaAngle -= 360f;
            if (deltaAngle == 0f) return Vector3.zero;
            else return deltaAngle * Mathf.Deg2Rad / deltaTime * (from * deltaAngleAxis);
        }

        void Reset()
        {
            item = GetComponent<Item>();
            rb = GetComponent<Rigidbody>();
        }
    }
}