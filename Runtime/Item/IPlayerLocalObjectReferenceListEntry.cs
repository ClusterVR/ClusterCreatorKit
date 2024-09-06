using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IPlayerLocalObjectReferenceListEntry
    {
        string Id { get; }
        GameObject GameObject { get; }
    }
}
