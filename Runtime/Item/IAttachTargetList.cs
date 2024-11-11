using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IAttachTargetList
    {
        IReadOnlyList<AttachTarget> AttachTargets { get; }
    }
}
