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
                worldRuntimeSetting.HudType = setting.UseHUDType;
                worldRuntimeSetting.UseCustomClippingPlanes = setting.UseCustomClippingPlanes;
                worldRuntimeSetting.NearPlane = setting.NearPlane;
                worldRuntimeSetting.FarPlane = setting.FarPlane;
                worldRuntimeSetting.EnableCrouchWalk = setting.EnableCrouchWalk;
                worldRuntimeSetting.DisplayAvatarSilhouette = setting.WriteOwnAvatarSilhouetteStencil;
            }
            else
            {
                worldRuntimeSetting.UseMovingPlatform = DefaultValues.UseMovingPlatform;
                worldRuntimeSetting.UseMovingPlatformHorizontalInertia = DefaultValues.MovingPlatformHorizontalInertia;
                worldRuntimeSetting.UseMovingPlatformVerticalInertia = DefaultValues.MovingPlatformVerticalInertia;
                worldRuntimeSetting.UseMantling = DefaultValues.UseMantling;
                worldRuntimeSetting.UseWorldShadow = DefaultValues.UseWorldShadow;
                worldRuntimeSetting.HudType = DefaultValues.HUDType;
                worldRuntimeSetting.UseCustomClippingPlanes = DefaultValues.UseCustomClippingPlanes;
                worldRuntimeSetting.NearPlane = DefaultValues.NearPlane;
                worldRuntimeSetting.FarPlane = DefaultValues.FarPlane;
                worldRuntimeSetting.EnableCrouchWalk = DefaultValues.EnableCrouchWalk;
                worldRuntimeSetting.DisplayAvatarSilhouette = DefaultValues.WriteOwnAvatarSilhouetteStencil;
            }
            descriptor.WorldRuntimeSetting = worldRuntimeSetting;

            return descriptor;
        }
    }
}
