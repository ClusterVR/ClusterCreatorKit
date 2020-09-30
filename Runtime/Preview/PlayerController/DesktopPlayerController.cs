using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    //プレビュー時にインスタンス生成されるプレイヤーキャラを操作するためのComponentです。PreviewOnly内PlayerControllerにアタッチされています。
    public class DesktopPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] Transform cameraTransform;
        [SerializeField] CharacterController characterController;
        [SerializeField] DesktopPointerEventListener desktopPointerEventListener;
        [SerializeField] float baseMoveSpeed;
        [SerializeField] float jumpSpeed;
        float velocityY;
        float moveSpeedRate = 1f;

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

        void Start()
        {
            desktopPointerEventListener.OnMoved += Rotate;
        }

        void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(x, 0, z);
            direction.Normalize();
            direction = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * direction;

            var moveSpeed = baseMoveSpeed * moveSpeedRate;
            var velocity = new Vector3(direction.x * moveSpeed, velocityY, direction.z * moveSpeed);
            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space)) velocityY = jumpSpeed;
                else velocityY = 0f;
            }
            velocityY -= Time.deltaTime * 9.81f;
        }

        void Rotate(Vector2 delta)
        {
            var euler = cameraTransform.eulerAngles;
            delta *= 120;
            euler = new Vector3(ClampAngle(euler.x - delta.y, -80, 80), euler.y + delta.x, 0);
            cameraTransform.rotation = Quaternion.Euler(euler);
        }

        static float ClampAngle(float angle, float min, float max)
        {
            angle += 180;
            angle = Mathf.Repeat(angle, 360);
            angle -= 180;
            angle = Mathf.Clamp(angle, min, max);

            return angle;
        }
    }
}
