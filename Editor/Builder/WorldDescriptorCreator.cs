using ClusterVR.CreatorKit.Proto;
using UnityEngine.SceneManagement;
using DefaultValues = ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting.WorldRuntimeSetting.DefaultValues;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class WorldDescriptorCreator
    {
        public static WorldDescriptor Create(Scene scene)
        {
            var descriptor = new WorldDescriptor();
            var persistedPlayerStateKeys = PersistedPlayerStateKeysGatherer.Gather(scene);
            descriptor.PersistedPlayerStateKeys.AddRange(persistedPlayerStateKeys);

            var worldRuntimeSetting = new WorldRuntimeSetting();
            var hasWorldRuntimeSetting =
                WorldRuntimeSettingGatherer.TryGetWorldRuntimeSetting(scene, out var setting);
            if (hasWorldRuntimeSetting)
            {
                worldRuntimeSetting.UseMovingPlatform = setting.UseMovingPlatform;
                worldRuntimeSetting.UseMovingPlatformHorizontalInertia = setting.MovingPlatformHorizontalInertia;
                worldRuntimeSetting.UseMovingPlatformVerticalInertia = setting.MovingPlatformVerticalInertia;
                worldRuntimeSetting.UseMantling = setting.UseMantling;
                worldRuntimeSetting.UseWorldShadow = setting.UseWorldShadow;
            }
            else
            {
                worldRuntimeSetting.UseMovingPlatform = DefaultValues.UseMovingPlatform;
                worldRuntimeSetting.UseMovingPlatformHorizontalInertia = DefaultValues.MovingPlatformHorizontalInertia;
                worldRuntimeSetting.UseMovingPlatformVerticalInertia = DefaultValues.MovingPlatformVerticalInertia;
                worldRuntimeSetting.UseMantling = DefaultValues.UseMantling;
                worldRuntimeSetting.UseWorldShadow = DefaultValues.UseWorldShadow;
            }
            descriptor.WorldRuntimeSetting = worldRuntimeSetting;

            return descriptor;
        }
    }
}
