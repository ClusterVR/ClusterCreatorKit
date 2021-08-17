using System.Collections.Generic;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class InitialPlayerTriggerManager
    {
        public void Invoke(IEnumerable<IInitializePlayerTrigger> initializePlayerTriggers,
            IEnumerable<IOnJoinPlayerTrigger> onJoinPlayerTriggers)
        {
            foreach (var initializePlayerTrigger in initializePlayerTriggers)
            {
                initializePlayerTrigger.Invoke();
            }

            foreach (var onJoinPlayerTrigger in onJoinPlayerTriggers)
            {
                onJoinPlayerTrigger.Invoke();
            }
        }
    }
}
