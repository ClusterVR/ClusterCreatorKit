using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Operation
{
    public interface ILogic : IGimmick, ITrigger
    {
        Logic Logic { get; }
    }
}
