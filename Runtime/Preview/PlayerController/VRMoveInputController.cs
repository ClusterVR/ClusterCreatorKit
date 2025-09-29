#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    [AddComponentMenu("")]
    public sealed class VRMoveInputController : MonoBehaviour, IMoveInputController
    {
        InputSystem_Actions.PlayerActions playerActions;

        public Vector2 MoveDirection
        {
            get
            {
                return playerActions.Move.ReadValue<Vector2>();
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
            playerActions = new InputSystem_Actions().Player;
            playerActions.Enable();
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
