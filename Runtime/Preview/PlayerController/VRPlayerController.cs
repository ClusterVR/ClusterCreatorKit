using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public class VRPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] float moveSpeed;
        [SerializeField] Transform cameraTransform;
        float fallingSpeed;

        public Transform PlayerTransform => characterController.transform;
        public Transform CameraTransform => cameraTransform;

        public void ActivateCharacterController(bool isActive)
        {
            characterController.enabled = isActive;
        }

        void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(x, 0, z);
            direction.Normalize();
            direction = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * direction;
            var velocity = direction * moveSpeed;
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
