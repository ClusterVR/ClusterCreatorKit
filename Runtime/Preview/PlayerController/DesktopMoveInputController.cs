#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class DesktopMoveInputController : MonoBehaviour, IMoveInputController
    {
        public Vector2 MoveDirection
        {
            get
            {
                return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }

        public float AdditionalAxis
        {
            get
            {
                var result = 0f;
                if (Input.GetKey(KeyCode.Space)) result += 1f;
                if (Input.GetKey(KeyCode.LeftShift)) result -= 1f;
                return result;
            }
        }

        public bool RideOffButtonPressed => Input.GetKey(KeyCode.X);

        Vector2 prevMoveDirection;
        public event Action<Vector2> OnMoveDirectionChanged;

        float prevAdditionalAxis;
        public event Action<float> OnAdditionalAxisChanged;

        public bool IsJumpButtonDown => Input.GetKeyDown(KeyCode.Space);

        void Start()
        {
            prevMoveDirection = MoveDirection;
            OnMoveDirectionChanged?.Invoke(prevMoveDirection);

            prevAdditionalAxis = AdditionalAxis;
            OnAdditionalAxisChanged?.Invoke(prevAdditionalAxis);
        }

        void Update()
        {
            var moveDirection = MoveDirection;
            if (moveDirection != prevMoveDirection)
            {
                prevMoveDirection = moveDirection;
                OnMoveDirectionChanged?.Invoke(moveDirection);
            }

            var additionalAxis = AdditionalAxis;
            if (!Mathf.Approximately(additionalAxis, prevAdditionalAxis))
            {
                prevAdditionalAxis = additionalAxis;
                OnAdditionalAxisChanged?.Invoke(additionalAxis);
            }
        }
    }
}
#endif
