using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class InteractableItemRaycaster : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;
        [SerializeField] InteractableItemFinder interactableItemFinder;

        const float RaycastMaxDistance = 20f;
        int raycastLayerMask;

        void Start()
        {
            raycastLayerMask = ~LayerName.PostProcessingMask;
        }

        public bool RaycastItem(Vector2 raycastPoint, out IGrabbableItem item, out Vector3 hitPoint)
        {
            item = default;
            hitPoint = default;
            var ray = targetCamera.ScreenPointToRay(raycastPoint);
            if (!Physics.Raycast(ray, out var hitInfo, RaycastMaxDistance, raycastLayerMask)) return false;
            item = hitInfo.collider.gameObject.GetComponentInParent<IGrabbableItem>();
            if (item == null || !interactableItemFinder.InteractableItems.Contains(item)) return false;
            hitPoint = hitInfo.point;
            return true;
        }
    }
}
