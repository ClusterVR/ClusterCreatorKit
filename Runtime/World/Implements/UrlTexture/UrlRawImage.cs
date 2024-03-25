using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.UrlTexture
{
    [RequireComponent(typeof(RawImage))]
    public sealed class UrlRawImage : MonoBehaviour, IUrlTexture
    {
        [SerializeField] string url;
        [SerializeField] TextureWrapMode wrapMode = TextureWrapMode.Repeat;
        [SerializeField] RawImage rawImage;
        UrlTextureSettings IUrlTexture.Settings => new(url, wrapMode);
        GameObject IUrlTexture.GameObject => gameObject;

        void IUrlTexture.SetTexture(Texture2D texture)
        {
            if (rawImage == null)
            {
                rawImage = GetComponent<RawImage>();
            }
            rawImage.texture = texture;
        }

        void Reset()
        {
            rawImage = GetComponent<RawImage>();
        }

        void OnValidate()
        {
            if (rawImage == null || rawImage.gameObject != gameObject)
            {
                rawImage = GetComponent<RawImage>();
            }
        }
    }
}
