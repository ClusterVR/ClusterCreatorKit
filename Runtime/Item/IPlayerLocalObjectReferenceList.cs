using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IPlayerLocalObjectReferenceList
    {
        IReadOnlyCollection<IPlayerLocalObjectReferenceListEntry> PlayerLocalObjectReferences { get; }
    }
}
