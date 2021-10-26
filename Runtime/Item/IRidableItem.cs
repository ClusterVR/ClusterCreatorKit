using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IRidableItem : IInteractableItem
    {
        IItem Item { get; }
        Transform Seat { get; }
        Transform ExitTransform { get; }

        Transform LeftGrip { get; }
        Transform RightGrip { get; }
        AnimationClip AvatarOverrideAnimation { get; }

        event Action OnInvoked;
        void GetOn();
        void GetOff();
    }
}
