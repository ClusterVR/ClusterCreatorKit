﻿#if UNITY_EDITOR
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Preview.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class DesktopItemController : MonoBehaviour, IItemController
    {
        [SerializeField] DesktopPointerEventListener desktopPointerEventListener;
        [SerializeField] Transform grabPoint;
        [SerializeField] DesktopItemView itemView;
        [SerializeField] ContactableItemRaycaster contactableItemRaycaster;

        IGrabbableItem grabbingItem;
        Quaternion grabPointToTargetOffsetRotation;
        Vector3 grabPointToTargetOffsetPosition;

        bool isCursorLocked = false;
        bool isUsingDown = false;

        void Start()
        {
            desktopPointerEventListener.OnClicked += OnClicked;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Release(false);
            }
            if (grabbingItem != null && !grabbingItem.Item.gameObject.activeInHierarchy)
            {
                Release(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetCursorLock(false);
            }

            if (isCursorLocked && Input.GetMouseButtonDown(0))
            {
                InvokeUseTrigger(true);
            }
            else if (isUsingDown && Input.GetMouseButtonUp(0))
            {
                InvokeUseTrigger(false);
            }
            MoveItem();
        }

        void OnClicked(Vector2 point)
        {
            if (isCursorLocked)
            {
                return;
            }
            if (contactableItemRaycaster.RaycastItem(point, out var item, out var hitPoint))
            {
                if (item == grabbingItem)
                {
                    return;
                }
                switch (item)
                {
                    case IGrabbableItem grabbableItem:
                        Release(false);
                        Grab(grabbableItem, hitPoint, false);
                        break;
                    case IInteractableItem interactableItem:
                        interactableItem.Invoke();
                        break;
                }
            }
            else if (grabbingItem != null)
            {
                SetCursorLock(true);
            }
        }

        void Grab(IGrabbableItem target, Vector3 hitPoint, bool isLeftHand)
        {
            itemView.SetGrabbingItem(target);
            target.OnGrab(isLeftHand);
            grabbingItem = target;

            if (target.Grip == null)
            {
                SetOffsets(grabPoint, target.MovableItem.Position + (grabPoint.position - hitPoint),
                    target.MovableItem.Rotation);
            }
            else
            {
                SetOffsets(target.Grip, target.MovableItem.Position, target.MovableItem.Rotation);
            }

            isUsingDown = false;
            SetCursorLock(true);
        }

        void SetOffsets(Transform from, Vector3 targetPosition, Quaternion targetRotation)
        {
            var inversedFromRotation = Quaternion.Inverse(from.rotation);
            grabPointToTargetOffsetPosition = inversedFromRotation * (targetPosition - from.position);
            grabPointToTargetOffsetRotation = inversedFromRotation * targetRotation;
        }

        void MoveItem()
        {
            if (grabbingItem == null)
            {
                return;
            }
            var grabPointRotation = grabPoint.rotation;
            grabbingItem.MovableItem.SetPositionAndRotation(
                grabPoint.position + grabPointRotation * grabPointToTargetOffsetPosition,
                grabPointRotation * grabPointToTargetOffsetRotation);
        }

        void Release(bool isLeftHand)
        {
            if (grabbingItem == null)
            {
                return;
            }
            grabbingItem.MovableItem.EnablePhysics();
            grabbingItem.OnRelease(isLeftHand);
            grabbingItem = null;
            itemView.SetGrabbingItem(null);
            SetCursorLock(false);
        }

        void InvokeUseTrigger(bool isDown)
        {
            if (grabbingItem == null)
            {
                return;
            }
            var useItemTrigger = grabbingItem.Item.gameObject.GetComponent<IUseItemTrigger>();
            if (useItemTrigger == null)
            {
                return;
            }
            useItemTrigger.Invoke(isDown);
            isUsingDown = isDown;
        }

        void SetCursorLock(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
            isCursorLocked = isLocked;
        }

        void IItemController.OnDestroyItem(IItem item)
        {
            if (grabbingItem != null && grabbingItem.Item == item)
            {
                grabbingItem = null;
                itemView.SetGrabbingItem(null);
                SetCursorLock(false);
            }
        }
    }
}
#endif
