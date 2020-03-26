using UnityEngine;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(MovableItem)), DisallowMultipleComponent]
    public sealed class GrabbableItem : MonoBehaviour, IGrabbableItem
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, Tooltip("持ち手（任意）")] Transform grip;

        public IMovableItem MovableItem => movableItem;
        public Transform Grip => grip;

        public void OnGrab()
        {
            gameObject.SetLayerRecursively(LayerName.GrabbingItem);
        }

        public void OnRelease()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }
    }
}