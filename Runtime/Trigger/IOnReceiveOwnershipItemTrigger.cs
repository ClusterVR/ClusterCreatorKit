namespace ClusterVR.CreatorKit.Trigger
{
    public interface IOnReceiveOwnershipItemTrigger : IItemTrigger
    {
        void Invoke(bool voluntary);
    }
}
