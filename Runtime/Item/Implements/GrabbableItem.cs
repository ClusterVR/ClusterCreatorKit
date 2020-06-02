using System;
using UnityEngine;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(MovableItem)), DisallowMultipleComponent]
    public sealed class GrabbableItem : InteractableItem, IGrabbableItem
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, Tooltip("持ち手（任意）")] Transform grip;

        public override IItem Item => MovableItem.Item;
        public IMovableItem MovableItem => movableItem != null ? movableItem : movableItem = GetComponent<MovableItem>();
        public Transform Grip => grip;

        public event Action OnGrabbed;
        public event Action OnReleased;

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
            if (movableItem == null || movableItem.gameObject != gameObject) movableItem = GetComponent<MovableItem>();
        }
    }
}