
namespace ClusterVR.CreatorKit.Trigger
{
    public interface IUseItemTrigger : IItemTrigger
    {
        void Invoke(bool isDown);
    }
}