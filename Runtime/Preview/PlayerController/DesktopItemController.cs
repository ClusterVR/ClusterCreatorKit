using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Preview.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public class DesktopItemController : MonoBehaviour
    {
        [SerializeField] DesktopPointerEventListener desktopPointerEventListener;
        [SerializeField] Transform grabPoint;
        [SerializeField] DesktopItemView itemView;
        [SerializeField] InteractableItemRaycaster interactableItemRaycaster;

        IGrabbableItem grabbingItem;
        Quaternion grabPointToTargetOffsetRotation;
        Vector3 grabPointToTargetOffsetPosition;

        void Start()
        {
            desktopPointerEventListener.OnClicked += TryGrab;
            itemView.OnRelease += Release;
        }

        void Update()
        {
            MoveItem();
        }

        void TryGrab(Vector2 point)
        {
            if (!interactableItemRaycaster.RaycastItem(point, out var item, out var hitPoint)) return;
            if (item == grabbingItem) return;
            Release();
            Grab(item, hitPoint);
        }

        void Grab(IGrabbableItem target, Vector3 hitPoint)
        {
            itemView.SetGrabbingItem(target);
            target.OnGrab();
            grabbingItem = target;

            if (target.Grip == null)
            {
                // hitPointをgripに重ねたときの値
                SetOffsets(grabPoint, target.MovableItem.Position + (grabPoint.position - hitPoint),
                    target.MovableItem.Rotation);
            }
            else
            {
                // grabPointとGripが同じ姿勢になるようなの値を計算する
                SetOffsets(target.Grip, target.MovableItem.Position, target.MovableItem.Rotation);
            }
        }
        
        void SetOffsets(Transform from, Vector3 targetPosition, Quaternion targetRotation)
        {
            var inversedFromRotation = Quaternion.Inverse(from.rotation);
            grabPointToTargetOffsetPosition = inversedFromRotation * (targetPosition - from.position);
            grabPointToTargetOffsetRotation = inversedFromRotation * targetRotation;
        }

        void MoveItem()
        {
            if (grabbingItem == null) return;
            var grabPointRotation = grabPoint.rotation;
            grabbingItem.MovableItem.SetPositionAndRotation(
                grabPoint.position + grabPointRotation * grabPointToTargetOffsetPosition,
                grabPointRotation * grabPointToTargetOffsetRotation);
        }

        public void Release()
        {
            if (grabbingItem == null) return;
            grabbingItem.MovableItem.EnablePhysics();
            grabbingItem.OnRelease();
            grabbingItem = null;
            itemView.SetGrabbingItem(null);
        }
    }
}