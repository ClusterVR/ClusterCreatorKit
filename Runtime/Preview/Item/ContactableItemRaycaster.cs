#if UNITY_EDITOR
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class ContactableItemRaycaster : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;
        [SerializeField] ContactableItemFinder contactableItemFinder;

        const float RaycastMaxDistance = 20f;
        int raycastLayerMask;

        void Start()
        {
            raycastLayerMask = ~LayerName.PostProcessingMask;
        }

        public bool RaycastItem(Vector2 raycastPoint, out IContactableItem item, out Vector3 hitPoint)
        {
            item = default;
            hitPoint = default;
            var ray = targetCamera.ScreenPointToRay(raycastPoint);
            if (!Physics.Raycast(ray, out var hitInfo, RaycastMaxDistance, raycastLayerMask))
            {
                return false;
            }
            item = hitInfo.collider.gameObject.GetComponentInParent<IContactableItem>();
            if (item == null || !contactableItemFinder.ContactableItems.Contains(item))
            {
                return false;
            }
            hitPoint = hitInfo.point;
            return true;
        }
    }
}
#endif
