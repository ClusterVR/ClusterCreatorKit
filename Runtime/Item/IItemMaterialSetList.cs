using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IItemMaterialSetList
    {
        IEnumerable<ItemMaterialSet> ItemMaterialSets { get; }
    }
}
