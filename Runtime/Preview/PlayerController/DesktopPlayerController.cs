#if UNITY_EDITOR
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class DesktopPlayerController : MonoBehaviour, IPlayerController
    {
        const float MaxHeadPitch = 80f;
        const float MinHeadPitch = -80f;
        const float MaxHeadYaw = 90f;

        [SerializeField] Transform rootTransform;
        [SerializeField] Transform cameraTransform;
        [SerializeField] CharacterController characterController;
        [SerializeField] DesktopPointerEventListener desktopPointerEventListener;
        [SerializeField] DesktopMoveInputController desktopMoveInputController;
        [SerializeField] float baseMoveSpeed;
        [SerializeField] float baseJumpSpeed;
        float velocityY;
        float moveSpeedRate = 1f;
        float jumpSpeedRate = 1f;

        IRidableItem ridingItem;
        bool prevIsRiding;

        public Transform PlayerTransform => characterController.transform;
        public Quaternion RootRotation => rootTransform.rotation;
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
            this.jumpSpeedRate = jumpSpeedRate;
        }

        void IPlayerController.SetRidingItem(IRidableItem ridingItem)
        {
            this.ridingItem = ridingItem;
        }

        void IPlayerController.SetRotationKeepingHeadPitch(Quaternion rotation)
        {
            var rootRotation = rootTransform.rotation;
            var cameraRotation = cameraTransform.rotation;
            var cameraLocalPitch = (Quaternion.Inverse(rootRotation) * cameraRotation).eulerAngles.x;
            rootTransform.rotation = rotation;
            cameraTransform.rotation = rotation * ClampPitch(new Vector3(cameraLocalPitch, 0f, 0f));
        }

        void IPlayerController.ResetCameraRotation(Quaternion rotation)
        {
            SetCameraRotation(rotation);
        }

        void SetCameraRotation(Quaternion rotation)
        {
            var rootRotation = rootTransform.rotation;
            var cameraLocalRotation = Quaternion.Inverse(rootRotation) * rotation;
            cameraLocalRotation = ClampAsHeadAngle(cameraLocalRotation.eulerAngles);
            var res = rootRotation * cameraLocalRotation;
            cameraTransform.rotation = res;
        }

        void Start()
        {
            desktopPointerEventListener.OnMoved += Rotate;
        }

        void Update()
        {
            var isRiding = IsRiding;
            if (isRiding)
            {
                SetEyeHeight(CameraControlSettings.SittingEyeHeight);
            }
            else
            {
                SetEyeHeight(CameraControlSettings.StandingEyeHeight);
                if (prevIsRiding)
                {
                    RestoreRotation();
                }
                Move();
            }

            prevIsRiding = isRiding;
        }

        void LateUpdate()
        {
            if (TryGetRidingSeat(out var seat))
            {
                PlayerTransform.position = seat.position;
                rootTransform.rotation = seat.rotation;
            }
        }

        void SetEyeHeight(float eyeHeight)
        {
            cameraTransform.localPosition = new Vector3(0f, eyeHeight, 0f);
        }

        void Move()
        {
            var moveDirection = desktopMoveInputController.MoveDirection;
            var direction = new Vector3(moveDirection.x, 0, moveDirection.y);
            direction.Normalize();
            direction = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * direction;

            var moveSpeed = baseMoveSpeed * moveSpeedRate;
            var velocity = new Vector3(direction.x * moveSpeed, velocityY, direction.z * moveSpeed);
            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
            {
                if (desktopMoveInputController.IsJumpButtonDown)
                {
                    velocityY = baseJumpSpeed * jumpSpeedRate;
                }
                else
                {
                    velocityY = 0f;
                }
            }

            velocityY -= Time.deltaTime * 9.81f;
        }

        void Rotate(Vector2 delta)
        {
            var rootRotation = rootTransform.rotation;
            var cameraLocalRotation = Quaternion.Inverse(rootRotation) * cameraTransform.rotation;
            var euler = cameraLocalRotation.eulerAngles;
            delta *= 120;
            cameraLocalRotation = ClampPitch(new Vector3(euler.x - delta.y, euler.y + delta.x, 0f));
            SetRotation(rootRotation * cameraLocalRotation);
        }

        Quaternion ClampPitch(Vector3 eulerAngles)
        {
            return Quaternion.Euler(ClampAngle(eulerAngles.x, MinHeadPitch, MaxHeadPitch), eulerAngles.y, 0f);
        }

        Quaternion ClampAsHeadAngle(Vector3 eulerAngles)
        {
            return Quaternion.Euler(ClampAngle(eulerAngles.x, MinHeadPitch, MaxHeadPitch), ClampAngle(eulerAngles.y, -MaxHeadYaw, MaxHeadYaw), 0f);
        }

        void SetRotation(Quaternion rotation)
        {
            if (IsRiding)
            {
                SetCameraRotation(rotation);
            }
            else
            {
                var onlyYawRotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
                rootTransform.rotation = onlyYawRotation;
                cameraTransform.rotation = rotation;
            }
        }

        void RestoreRotation()
        {
            var rootRotation = rootTransform.rotation;
            var cameraRotation = cameraTransform.rotation;
            var cameraLocalPitch = (Quaternion.Inverse(rootRotation) * cameraRotation).eulerAngles.x;
            var cameraGlobalYaw = cameraRotation.eulerAngles.y;
            rootRotation = Quaternion.Euler(0f, cameraGlobalYaw, 0f);
            rootTransform.rotation = rootRotation;
            cameraTransform.rotation = rootRotation * ClampPitch(new Vector3(cameraLocalPitch, 0f, 0f));
        }

        bool IsRiding => TryGetRidingSeat(out _);

        bool TryGetRidingSeat(out Transform seat)
        {
            if (ridingItem == null)
            {
                seat = default;
                return false;
            }
            else
            {
                seat = ridingItem.Seat;
                return seat != null;
            }
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
#endif
