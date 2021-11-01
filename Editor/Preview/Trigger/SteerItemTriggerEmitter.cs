using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Preview.Common;
using ClusterVR.CreatorKit.Preview.PlayerController;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class SteerItemTriggerEmitter
    {
        readonly RidableItemManager ridableItemManager;
        readonly IMoveInputController moveInputController;
        readonly PlayerPresenter playerPresenter;
        IRidableItem ridingItem;
        ISteerItemTrigger steerItemTrigger;

        Vector2 lastMoveDirection;
        float lastAdditionalAxis;

        public SteerItemTriggerEmitter(RidableItemManager ridableItemManager,
            PlayerPresenter playerPresenter,
            IMoveInputController moveInputController)
        {
            this.ridableItemManager = ridableItemManager;
            this.playerPresenter = playerPresenter;
            this.moveInputController = moveInputController;

            TickGenerator.Instance.OnTick += Tick;
            moveInputController.OnAdditionalAxisChanged += SetAdditionalAxis;
            moveInputController.OnMoveDirectionChanged += SetMoveDirection;
        }

        void Tick()
        {
            var newRidingItem = ridableItemManager.RidingItem;
            if (newRidingItem != ridingItem)
            {
                ridingItem = newRidingItem;
                steerItemTrigger = ridingItem?.GetSteerItemTrigger();

                SetMoveDirection(moveInputController.MoveDirection, true);
                SetAdditionalAxis(moveInputController.AdditionalAxis, true);
            }
            else if (steerItemTrigger != null && GetSteerSpace(steerItemTrigger) == SteerSpace.SeatToCamera)
            {
                SetMoveDirection(moveInputController.MoveDirection);
            }
        }

        void SetMoveDirection(Vector2 moveDirection) => SetMoveDirection(moveDirection, false);
        void SetMoveDirection(Vector2 moveDirection, bool force)
        {
            if (steerItemTrigger == null || ridingItem.Seat == null)
            {
                return;
            }

            var space = GetSteerSpace(steerItemTrigger);
            if (space == SteerSpace.SeatToCamera)
            {
                moveDirection = TransformSeatToCameraSpace(moveDirection, ridingItem.Seat);
            }

            if (!force && moveDirection == lastMoveDirection)
            {
                return;
            }

            lastMoveDirection = moveDirection;
            steerItemTrigger.OnMoveInputValueChanged(moveDirection);
        }

        void SetAdditionalAxis(float additionalAxis) => SetAdditionalAxis(additionalAxis, false);
        void SetAdditionalAxis(float additionalAxis, bool force)
        {
            if (steerItemTrigger == null)
            {
                return;
            }

            if (!force && Mathf.Approximately(additionalAxis, lastAdditionalAxis))
            {
                return;
            }

            lastAdditionalAxis = additionalAxis;
            steerItemTrigger.OnAdditionalAxisInputValueChanged(lastAdditionalAxis);
        }

        static SteerSpace GetSteerSpace(ISteerItemTrigger steerItemTrigger)
        {
            return steerItemTrigger.FirstPersonMoveSpace;
        }

        Vector2 TransformSeatToCameraSpace(Vector2 local, Transform seat)
        {
            var worldSpace = CameraRotation() * new Vector3(local.x, 0, local.y);
            var seatSpace = seat.InverseTransformDirection(worldSpace);
            return new Vector2(seatSpace.x, seatSpace.z);
        }

        Quaternion CameraRotation()
        {
            var rootRotation = playerPresenter.RootRotation;
            var rootToCamera = Quaternion.Inverse(rootRotation) * playerPresenter.CameraTransform.rotation;
            return rootRotation * Quaternion.Euler(0, rootToCamera.eulerAngles.y, 0);
        }
    }
}
