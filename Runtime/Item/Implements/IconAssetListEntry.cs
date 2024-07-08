using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class IconAssetListEntry : IIconAssetListEntry
    {
        [SerializeField] string id;
        [SerializeField] Texture2D image;

        public string Id => id;
        public IIconAsset IconAsset => new IconAsset(image);

        public IconAssetListEntry(string id, Texture2D image)
        {
            this.id = id;
            this.image = image;
        }
    }
}
