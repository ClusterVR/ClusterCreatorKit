namespace ClusterVR.CreatorKit.Item
{
    public interface IContactableItem
    {
        bool IsContactable { get; }
        bool RequireOwnership { get; }
        IItem Item { get; }
    }
}
