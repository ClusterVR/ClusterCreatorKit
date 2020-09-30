using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public interface IPlayerController
    {
        Transform PlayerTransform { get; }
        Transform CameraTransform { get; }
        void ActivateCharacterController(bool isActive);
        void SetMoveSpeedRate(float moveSpeedRate);
    }
}
