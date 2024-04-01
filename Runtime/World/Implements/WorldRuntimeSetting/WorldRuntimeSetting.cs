using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting
{
    [DisallowMultipleComponent]
    public sealed class WorldRuntimeSetting : MonoBehaviour
    {
        public static class DefaultValues
        {
            public const bool UseMovingPlatform = true;
            public const bool MovingPlatformHorizontalInertia = false;
            public const bool MovingPlatformVerticalInertia = true;
        }

        [SerializeField] bool useMovingPlatform = DefaultValues.UseMovingPlatform;
        [SerializeField] bool movingPlatformHorizontalInertia = DefaultValues.MovingPlatformHorizontalInertia;
        [SerializeField] bool movingPlatformVerticalInertia = DefaultValues.MovingPlatformVerticalInertia;

        public bool UseMovingPlatform => useMovingPlatform;
        public bool MovingPlatformHorizontalInertia => movingPlatformHorizontalInertia;
        public bool MovingPlatformVerticalInertia => movingPlatformVerticalInertia;
    }
}
