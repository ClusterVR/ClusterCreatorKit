using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IItemGimmick : IGimmick
    {
        IItem Item { get; }
    }
}