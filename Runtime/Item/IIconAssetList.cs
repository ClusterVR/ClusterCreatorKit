using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IIconAssetList
    {
        IReadOnlyCollection<IIconAssetListEntry> IconAssets { get; }
    }
}
