using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGlobalGimmick : IGimmick
    {
        ItemId ItemId { get; } // nullable
    }
}