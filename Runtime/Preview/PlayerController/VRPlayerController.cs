#if UNITY_EDITOR
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class VRPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] VRMoveInputController moveInputController;
        [SerializeField] float baseMoveSpeed;
        [SerializeField] Transform cameraTransform;
        float moveSpeedRate = 1f;
        float fallingSpeed;

        public Transform PlayerTransform => characterController.transform;
        public Quaternion RootRotation => PlayerTransform.rotation;
        public Transform CameraTransform => cameraTransform;

        public void WarpTo(Vector3 position)
        {
            characterController.enabled = false;
            characterController.transform.position = position;
            characterController.enabled = true;
        }

        void IPlayerController.SetMoveSpeedRate(float moveSpeedRate)
        {
            this.moveSpeedRate = moveSpeedRate;
        }

        void IPlayerController.SetJumpSpeedRate(float jumpSpeedRate)
        {
        }

        void IPlayerController.SetRidingItem(IRidableItem ridingItem)
        {
        }

        void IPlayerController.SetRotationKeepingHeadPitch(Quaternion rotation)
        {
        }

        void IPlayerController.ResetCameraRotation(Quaternion rotation)
        {
        }

        void Update()
        {
            var moveDirection = moveInputController.MoveDirection;
            var direction = new Vector3(moveDirection.x, 0, moveDirection.y);
            direction.Normalize();
            direction = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * direction;
            var velocity = baseMoveSpeed * moveSpeedRate * direction;
            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
            {
                fallingSpeed = 0;
            }
            else
            {
                fallingSpeed += Time.deltaTime * 9.81f;
            }

            velocity.y = -fallingSpeed;
        }
    }
}
#endif
