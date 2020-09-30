using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public class VRPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] float baseMoveSpeed;
        [SerializeField] Transform cameraTransform;
        float moveSpeedRate = 1f;
        float fallingSpeed;

        public Transform PlayerTransform => characterController.transform;
        public Transform CameraTransform => cameraTransform;

        public void ActivateCharacterController(bool isActive)
        {
            characterController.enabled = isActive;
        }

        void IPlayerController.SetMoveSpeedRate(float moveSpeedRate)
        {
            this.moveSpeedRate = moveSpeedRate;
        }

        void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(x, 0, z);
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
