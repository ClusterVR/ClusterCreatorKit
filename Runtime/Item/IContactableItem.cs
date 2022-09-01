namespace ClusterVR.CreatorKit.Item
{
    public interface IContactableItem
    {
        bool IsContactable { get; }
        IItem Item { get; }
    }
}
