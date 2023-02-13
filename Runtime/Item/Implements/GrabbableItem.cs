using System;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(MovableItem)), DisallowMultipleComponent]
    public sealed class GrabbableItem : ContactableItem, IGrabbableItem
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, Tooltip("持ち手（任意）")] Transform grip;

        public override IItem Item => MovableItem != null ? MovableItem.Item : null;

        public IMovableItem MovableItem
        {
            get
            {
                if (movableItem != null)
                {
                    return movableItem;
                }
                if (this == null)
                {
                    return null;
                }
                return movableItem = GetComponent<MovableItem>();
            }
        }

        bool IGrabbableItem.IsDestroyed => this == null;
        public override bool IsContactable => true;
        public override bool RequireOwnership => true;

        public Transform Grip => grip;

        public event Action OnGrabbed;
        public event Action OnReleased;

        public void Construct(Transform grip)
        {
            this.grip = grip;
        }

        void Start()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        public void OnGrab()
        {
            gameObject.SetLayerRecursively(LayerName.GrabbingItem);
            OnGrabbed?.Invoke();
        }

        public void OnRelease()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
            OnReleased?.Invoke();
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject)
            {
                movableItem = GetComponent<MovableItem>();
            }
        }
    }
}
