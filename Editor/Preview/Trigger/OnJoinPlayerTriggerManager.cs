using System.Collections.Generic;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class OnJoinPlayerTriggerManager
    {
        public void Invoke(IEnumerable<IOnJoinPlayerTrigger> onJoinPlayerTriggers)
        {
            foreach (var onJoinPlayerTrigger in onJoinPlayerTriggers)
            {
                onJoinPlayerTrigger.Invoke();
            }
        }
    }
}