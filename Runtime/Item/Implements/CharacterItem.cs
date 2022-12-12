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
        bool IMovableItem.IsDynamic => true;
        Vector3 IMovableItem.Position => transform.position;
        Quaternion IMovableItem.Rotation => transform.rotation;
        public override Vector3 Velocity => characterController.velocity;
        public override Vector3 AngularVelocity => angularVelocity;
        public bool IsGrounded => characterController.isGrounded;

        bool controlling = true;

        bool isInitialized;
        Vector3 initialPosition;
        Quaternion initialRotation;
        Vector3 velocity;
        Vector3 angularVelocity;

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
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            isInitialized = true;
        }

        void Start()
        {
            CacheInitialValue();
        }

        void Update()
        {
            if (!controlling)
            {
                return;
            }

            var isGrounded = characterController.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = 0f;
            }

            velocity.y += Gravity * Time.unscaledDeltaTime;
            characterController.Move(velocity * Time.unscaledDeltaTime);
            transform.Rotate(Vector3.up, angularVelocity.y * Mathf.Rad2Deg * Time.unscaledDeltaTime);
        }

        void IMovableItem.SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp)
        {
            CacheInitialValue();
            transform.SetPositionAndRotation(position, rotation);
            controlling = false;
        }

        void IMovableItem.EnablePhysics()
        {
            if (controlling)
            {
                return;
            }

            controlling = true;
        }

        void IMovableItem.Respawn()
        {
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
        }


        public void SetVelocityXZ(Vector2 value)
        {
            velocity.x = value.x;
            velocity.z = value.y;
        }

        public void SetVelocityY(float value)
        {
            velocity.y = value;
        }

        public void SetAngularVelocityY(float value)
        {
            angularVelocity.y = value;
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
