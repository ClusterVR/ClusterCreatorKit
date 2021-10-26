using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public interface IMoveInputController
    {
        Vector2 MoveDirection { get; }
        event Action<Vector2> OnMoveDirectionChanged;
        float AdditionalAxis { get; }
        event Action<float> OnAdditionalAxisChanged;
        bool RideOffButtonPressed { get; }
        bool IsJumpButtonDown { get; }
    }
}
