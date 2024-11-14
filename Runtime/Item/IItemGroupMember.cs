namespace ClusterVR.CreatorKit.Item
{
    public interface IItemGroupMember
    {
        IItem Item { get; }
        IItemGroupHost Host { get; }
    }
}
