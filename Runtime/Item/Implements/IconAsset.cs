using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class IconAsset : IIconAsset
    {
        readonly Texture2D image;

        public IconAsset(Texture2D image)
        {
            this.image = image;
        }

        Texture2D IIconAsset.GetTexture() => image;
    }
}
