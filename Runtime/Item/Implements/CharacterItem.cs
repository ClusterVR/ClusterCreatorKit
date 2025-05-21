using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item), typeof(CharacterController)), DisallowMultipleComponent]
    public sealed class CharacterItem : MovableItemBase, IMovableItem
    {
        const float Gravity = -9.81f;

        [SerializeField, HideInInspector] Item item;
        [SerializeField, HideInInspector] CharacterController characterController;

        public override IItem Item => item != null ? item : item = GetComponent<Item>();
        bool IMovableItem.IsDestroyed => this == null;
        bool IMovableItem.IsRespawning => Time.frameCount == lastRespawnedFrameCount;
        bool IMovableItem.IsDynamic => true;
        float IMovableItem.Mass => throw new NotImplementedException();

        bool useGravity = true;

        bool IMovableItem.UseGravity
        {
            get => useGravity;
            set => useGravity = value;
        }

        Vector3 IMovableItem.Position => transform.position;
        Quaternion IMovableItem.Rotation => transform.rotation;

        public override Vector3 Velocity => characterController.velocity;

        public override Vector3 AngularVelocity => angularVelocity;

        public bool IsGrounded => characterController.isGrounded;

        bool controlling = true;

        bool isInitialized;
        Vector3 initialPosition;
        Quaternion initialRotation;
        Vector3 lastPosition;
        Quaternion lastRotation;
        Vector3 velocity;
        Vector3 angularVelocity;
        int lastRespawnedFrameCount = -1;

        void CacheInitialValue()
        {
            if (isInitialized)
            {
                return;
            }
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
            initialPosition = lastPosition = transform.position;
            initialRotation = lastRotation = transform.rotation;
            isInitialized = true;
        }

        void Start()
        {
            CacheInitialValue();
        }

        void Update()
        {
            if (controlling)
            {
                var isGrounded = characterController.isGrounded;
                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = 0f;
                }

                if (useGravity)
                {
                    velocity.y += Gravity * Time.unscaledDeltaTime;
                }
                characterController.Move(velocity * Time.unscaledDeltaTime);
                transform.Rotate(Vector3.up, angularVelocity.y * Mathf.Rad2Deg * Time.unscaledDeltaTime);
            }
            else
            {
                velocity = (lastPosition - transform.position) / Time.unscaledDeltaTime;
                var angularVelocityY = Mathf.Deg2Rad * Mathf.DeltaAngle(0, (Quaternion.Inverse(lastRotation) * transform.rotation).eulerAngles.y) / Time.unscaledDeltaTime;
                angularVelocity = new Vector3(0f, angularVelocityY, 0f);
            }
            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }

        void IMovableItem.SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp)
        {
            CacheInitialValue();
            characterController.enableOverlapRecovery = false;
            if (isWarp)
            {
                lastPosition = position;
                lastRotation = rotation;
            }
            controlling = false;
            transform.SetPositionAndRotation(position, rotation);
        }

        void IMovableItem.EnablePhysics()
        {
            if (controlling)
            {
                return;
            }

            CacheInitialValue();
            characterController.enableOverlapRecovery = true;
            controlling = true;
        }

        void IMovableItem.Respawn()
        {
            lastRespawnedFrameCount = Time.frameCount;
            WarpTo(initialPosition, initialRotation);
        }

        public override void WarpTo(Vector3 position, Quaternion rotation)
        {
            if (!controlling)
            {
                return;
            }

            CacheInitialValue();
            transform.SetPositionAndRotation(position, rotation);
            velocity = Vector3.zero;
            angularVelocity = Vector3.zero;
            lastPosition = position;
            lastRotation = rotation;
        }


        public void SetVelocityXZ(Vector2 value)
        {
            if (!controlling)
            {
                return;
            }
            velocity.x = value.x;
            velocity.z = value.y;
        }

        public void SetVelocityY(float value)
        {
            if (!controlling)
            {
                return;
            }
            velocity.y = value;
        }

        public void SetAngularVelocityY(float value)
        {
            if (!controlling)
            {
                return;
            }
            angularVelocity.y = value;
        }

        public void SetVelocity(Vector3 value)
        {
            if (!controlling)
            {
                return;
            }
            velocity = value;
        }

        public void SetAngularVelocity(Vector3 value)
        {
            SetAngularVelocityY(value.y);
        }

        void IMovableItem.AddForce(Vector3 force, ForceMode mode)
        {
        }

        void IMovableItem.AddTorque(Vector3 force, ForceMode mode)
        {
        }

        void IMovableItem.AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode)
        {
        }

        void Reset()
        {
            item = GetComponent<Item>();
            characterController = GetComponent<CharacterController>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject)
            {
                item = GetComponent<Item>();
            }
            if (characterController == null || characterController.gameObject != gameObject)
            {
                characterController = GetComponent<CharacterController>();
            }
        }
    }
}
