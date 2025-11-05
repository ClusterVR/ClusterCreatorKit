using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IAudioConfigurationSetList
    {
        IEnumerable<AudioConfigurationSet> AudioConfigurationSets { get; }
    }
}
