using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IUrlTexture
    {
        UrlTextureSettings Settings { get; }
        GameObject GameObject { get; }
        void SetTexture(Texture2D texture);
    }

    public readonly struct UrlTextureSettings : IEquatable<UrlTextureSettings>
    {
        public readonly string Url;
        public readonly TextureWrapMode WrapMode;

        public UrlTextureSettings(string url, TextureWrapMode wrapMode)
        {
            Url = url;
            WrapMode = wrapMode;
        }

        public bool Equals(UrlTextureSettings other)
        {
            return Url == other.Url && WrapMode == other.WrapMode;
        }

        public override bool Equals(object obj)
        {
            return obj is UrlTextureSettings other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Url, (int) WrapMode);
        }
    }
}
