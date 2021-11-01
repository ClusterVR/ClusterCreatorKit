using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public interface IPlayerController
    {
        Transform PlayerTransform { get; }
        Quaternion RootRotation { get; }
        Transform CameraTransform { get; }
        void WarpTo(Vector3 position);
        void SetMoveSpeedRate(float moveSpeedRate);
        void SetJumpSpeedRate(float jumpSpeedRate);
        void SetRidingItem(IRidableItem ridingItem);
        void SetRotationKeepingHeadPitch(Quaternion rotation);
        void ResetCameraRotation(Quaternion rotation);
    }
}
