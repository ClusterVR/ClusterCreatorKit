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
            public const Proto.WorldRuntimeSetting.Types.HUDType HUDType = Proto.WorldRuntimeSetting.Types.HUDType.LegacyHud;
        }

        [SerializeField] bool useMovingPlatform = DefaultValues.UseMovingPlatform;
        [SerializeField] bool movingPlatformHorizontalInertia = DefaultValues.MovingPlatformHorizontalInertia;
        [SerializeField] bool movingPlatformVerticalInertia = DefaultValues.MovingPlatformVerticalInertia;
        [SerializeField] bool useMantling = DefaultValues.UseMantling;
        [SerializeField] bool useWorldShadow = DefaultValues.UseWorldShadow;
        [SerializeField] Proto.WorldRuntimeSetting.Types.HUDType useHUDType = DefaultValues.HUDType;

        public bool UseMovingPlatform => useMovingPlatform;
        public bool MovingPlatformHorizontalInertia => movingPlatformHorizontalInertia;
        public bool MovingPlatformVerticalInertia => movingPlatformVerticalInertia;
        public bool UseMantling => useMantling;
        public bool UseWorldShadow => useWorldShadow;
        public Proto.WorldRuntimeSetting.Types.HUDType UseHUDType
        {
            get
            {
                return useHUDType;
            }
            set
            {
                useHUDType = value;
            }
        }

        public static bool HUDTypeToBool(Proto.WorldRuntimeSetting.Types.HUDType hudType)
        {
            switch (hudType)
            {
                case Proto.WorldRuntimeSetting.Types.HUDType.LegacyHud:
                    return false;
                case Proto.WorldRuntimeSetting.Types.HUDType.ClusterHudV2:
                    return true;
                default:
                    return false;
            }
        }
        public static Proto.WorldRuntimeSetting.Types.HUDType BoolToHUDType(bool hudType)
        {
            return hudType ? Proto.WorldRuntimeSetting.Types.HUDType.ClusterHudV2 : Proto.WorldRuntimeSetting.Types.HUDType.LegacyHud;
        }
    }
}
