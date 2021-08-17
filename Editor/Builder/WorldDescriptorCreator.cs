using ClusterVR.CreatorKit.Proto;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class WorldDescriptorCreator
    {
        public static WorldDescriptor Create(Scene scene)
        {
            var descriptor = new WorldDescriptor();
            var persistedPlayerStateKeys = PersistedPlayerStateKeysGatherer.Gather(scene);
            descriptor.PersistedPlayerStateKeys.AddRange(persistedPlayerStateKeys);

            return descriptor;
        }
    }
}
