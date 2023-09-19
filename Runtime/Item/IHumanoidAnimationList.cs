using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IHumanoidAnimationList
    {
        IReadOnlyCollection<IHumanoidAnimationListEntry> HumanoidAnimations { get; }
    }
}
