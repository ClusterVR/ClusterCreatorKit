using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class PersistedPlayerStateKeysGatherer
    {
        public static IReadOnlyCollection<string> Gather(Scene scene)
        {
            var persistedPlayerStateKeys = new HashSet<string>();
            foreach (var initializePlayerTrigger in scene.GetRootGameObjects()
                .SelectMany(o => o.GetComponentsInChildren<IInitializePlayerTrigger>(true)))
            {
                foreach (var param in
                initializePlayerTrigger.TriggerParams.Where(p => p.Target == TriggerTarget.Player))
                {
                    persistedPlayerStateKeys.Add(param.Key);
                }
            }

            return persistedPlayerStateKeys;
        }
    }
}
