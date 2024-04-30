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
            public const bool UseMantling = true;
            public const bool UseWorldShadow = true;
        }

        [SerializeField] bool useMovingPlatform = DefaultValues.UseMovingPlatform;
        [SerializeField] bool movingPlatformHorizontalInertia = DefaultValues.MovingPlatformHorizontalInertia;
        [SerializeField] bool movingPlatformVerticalInertia = DefaultValues.MovingPlatformVerticalInertia;
        [SerializeField] bool useMantling = DefaultValues.UseMantling;
        [SerializeField] bool useWorldShadow = DefaultValues.UseWorldShadow;

        public bool UseMovingPlatform => useMovingPlatform;
        public bool MovingPlatformHorizontalInertia => movingPlatformHorizontalInertia;
        public bool MovingPlatformVerticalInertia => movingPlatformVerticalInertia;
        public bool UseMantling => useMantling;
        public bool UseWorldShadow => useWorldShadow;
    }
}
