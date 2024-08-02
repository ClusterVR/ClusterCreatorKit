using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IWorldItemReferenceList
    {
        IReadOnlyCollection<IWorldItemReferenceListEntry> WorldItemReferences { get; }
    }
}
