using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Trigger
{
    public interface ITrigger
    {
        IEnumerable<TriggerParam> TriggerParams { get; }
    }
}
