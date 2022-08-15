using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.UrlTexture
{
    [RequireComponent(typeof(Renderer))]
    public sealed class UrlTexture : MonoBehaviour, IUrlTexture
    {
        [SerializeField] string url;
        [SerializeField] new Renderer renderer;
        [SerializeField] string targetMaterialPropertyName = "_MainTex";

        string IUrlTexture.Url => url;
        GameObject IUrlTexture.GameObject => gameObject;
        Material[] materials;

        void IUrlTexture.SetTexture(Texture2D texture)
        {
            if (renderer == null)
            {
                renderer = GetComponent<Renderer>();
            }

            if (materials != null)
            {
                foreach (var material in materials)
                {
                    Destroy(material);
                }
            }

            materials = renderer.materials;
            if (materials != null)
            {
                foreach (var material in materials)
                {
                    material.SetTexture(targetMaterialPropertyName, texture);
                }
            }
        }

        void Reset()
        {
            renderer = GetComponent<Renderer>();
        }

        void OnValidate()
        {
            if (renderer == null || renderer.gameObject != gameObject)
            {
                renderer = GetComponent<Renderer>();
            }
        }

        void OnDestroy()
        {
            if (materials != null)
            {
                foreach (var material in materials)
                {
                    Destroy(material);
                }
            }
        }
    }
}
