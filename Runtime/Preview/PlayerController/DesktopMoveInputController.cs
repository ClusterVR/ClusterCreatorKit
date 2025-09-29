#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    [AddComponentMenu("")]
    public sealed class DesktopMoveInputController : MonoBehaviour, IMoveInputController
    {
        InputSystem_Actions.PlayerActions playerActions;

        public Vector2 MoveDirection
        {
            get
            {
                return playerActions.Move.ReadValue<Vector2>();
            }
        }

        public float AdditionalAxis
        {
            get
            {
                return playerActions.AdditionalAxis.ReadValue<float>();
            }
        }

        public bool RideOffButtonPressed => playerActions.RideOff.IsPressed();

        Vector2 prevMoveDirection;
        public event Action<Vector2> OnMoveDirectionChanged;

        float prevAdditionalAxis;
        public event Action<float> OnAdditionalAxisChanged;

        public bool IsJumpButtonDown => playerActions.Jump.WasPressedThisFrame();

        void Start()
        {
            playerActions = new InputSystem_Actions().Player;
            playerActions.Enable();

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
