using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public interface IPlayerController
    {
        Transform PlayerTransform { get; }
        Transform RootTransform { get; }
        Transform CameraTransform { get; }
        void ActivateCharacterController(bool isActive);
        void SetMoveSpeedRate(float moveSpeedRate);
        void SetJumpSpeedRate(float jumpSpeedRate);
        void SetRidingItem(IRidableItem ridingItem);
        void SetRotationKeepingHeadPitch(Quaternion rotation);
        void ResetCameraRotation(Quaternion rotation);
    }
}
