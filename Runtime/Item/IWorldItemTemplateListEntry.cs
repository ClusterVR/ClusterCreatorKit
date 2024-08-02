namespace ClusterVR.CreatorKit.Item
{
    public interface IWorldItemTemplateListEntry
    {
        string Id { get; }
        IItem WorldItemTemplate { get; }
        ItemTemplateId ItemTemplateId { get; }
    }
}
