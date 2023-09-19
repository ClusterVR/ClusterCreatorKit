using System.Collections.Generic;
using ClusterVR.CreatorKit.Common;

namespace ClusterVR.CreatorKit.Item
{
    public interface IHumanoidAnimation
    {
        float Length { get; }
        bool IsLoop { get; }
        IReadOnlyList<IHumanoidAnimationCurve> Curves { get; }
        PartialHumanPose Sample(float time);
    }
}
