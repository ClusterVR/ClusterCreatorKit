using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item))]
    [DisallowMultipleComponent]
    public sealed class IconAssetList : MonoBehaviour, IIconAssetList, IIdContainer
    {
        [SerializeField] IconAssetListEntry[] iconAssets;

        IReadOnlyCollection<IIconAssetListEntry> IIconAssetList.IconAssets => iconAssets;
        IEnumerable<string> IIdContainer.Ids => iconAssets.Select(a => a.Id);

        public void Construct(IconAssetListEntry[] iconAssets)
        {
            this.iconAssets = iconAssets;
        }
    }
}
