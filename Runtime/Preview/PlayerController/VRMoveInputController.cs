#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class VRMoveInputController : MonoBehaviour, IMoveInputController
    {
        public Vector2 MoveDirection
        {
            get
            {
                return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }

        public float AdditionalAxis => 0f;

        public bool RideOffButtonPressed => false;

        Vector2 prevMoveDirection;
        public event Action<Vector2> OnMoveDirectionChanged;

        public event Action<float> OnAdditionalAxisChanged;

        public bool IsJumpButtonDown => false;

        void Start()
        {
            prevMoveDirection = MoveDirection;
            OnMoveDirectionChanged?.Invoke(prevMoveDirection);
        }

        void Update()
        {
            var moveDirection = MoveDirection;
            if (moveDirection != prevMoveDirection)
            {
                prevMoveDirection = moveDirection;
                OnMoveDirectionChanged?.Invoke(moveDirection);
            }
        }
    }
}
#endif
