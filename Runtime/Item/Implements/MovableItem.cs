using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item), typeof(Rigidbody)), DisallowMultipleComponent]
    public sealed class MovableItem : MonoBehaviour, IMovableItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, HideInInspector] Rigidbody rb;

        public IItem Item => item != null ? item : item = GetComponent<Item>();
        public Vector3 Position => rb.position;
        public Quaternion Rotation => rb.rotation;

        enum State
        {
            Free, Controlled, Interpolated
        }

        bool isInitialized;
        Vector3 initialPosition;
        Quaternion initialRotation;
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
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            initialIsKinematic = rb.isKinematic;
            isInitialized = true;
        }

        void Start()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();

            CacheInitialValue();
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false)
        {
            CacheInitialValue();
            rb.isKinematic = true;
            if (state == State.Free || isWarp)
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
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }

        void FixedUpdate()
        {
            if (state == State.Free) return;

            if (state == State.Controlled)
            {
                rb.position = currentPosition;
                rb.rotation = currentRotation;
                state = State.Interpolated;
            }

            var interpolateRate = (Time.realtimeSinceStartup - setAt) / interpolateDurationSeconds;
            rb.MovePosition(Vector3.Lerp(currentPosition, targetPosition, interpolateRate));
            rb.MoveRotation(Quaternion.Slerp(currentRotation, targetRotation, interpolateRate));
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

        public void Respawn()
        {
            WarpTo(initialPosition, initialRotation);
        }

        public void WarpTo(Vector3 position, Quaternion rotation)
        {
            if (state != State.Free) return;
            transform.position = position;
            transform.rotation = rotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            if (state != State.Free) return;
            rb.AddForce(force, mode);
        }

        public void AddTorque(Vector3 torque, ForceMode mode)
        {
            if (state != State.Free) return;
            rb.AddTorque(torque, mode);
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (state != State.Free) return;
            rb.velocity = velocity;
        }

        public void SetAngularVelocity(Vector3 angularVelocity)
        {
            if (state != State.Free) return;
            rb.angularVelocity = angularVelocity;
        }

        void Reset()
        {
            item = GetComponent<Item>();
            rb = GetComponent<Rigidbody>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item>();
            if (rb == null || rb.gameObject != gameObject) rb = GetComponent<Rigidbody>();
        }
    }
}