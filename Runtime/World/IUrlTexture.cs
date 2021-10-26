using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IUrlTexture
    {
        string Url { get; }
        GameObject GameObject { get; }
        void SetTexture(Texture2D texture);
    }
}
