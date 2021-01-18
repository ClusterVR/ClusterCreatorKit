using System;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [Flags]
    public enum CollisionType
    {
        Collision = 1 << 0,
        Trigger = 1 << 1,
        Everything = ~0
    }
}
