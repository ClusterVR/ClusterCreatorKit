using UnityEngine;

namespace ClusterVR.CreatorKit.Extensions
{
    [RequireComponent(typeof(Renderer))]
    public sealed class RendererMaterialHolder : MonoBehaviour
    {
        Renderer renderer;
        Material[] sharedMaterials;
        Material[] materials;
        Material[] overrideMaterials;
        bool isCached;

        void Awake()
        {
            CacheRendererAndMaterialsOnce();
        }

        public Material[] GetSharedMaterials()
        {
            CacheRendererAndMaterialsOnce();
            return sharedMaterials;
        }

        public void SetMaterials(Material[] materials)
        {
            CacheRendererAndMaterialsOnce();
            this.materials = materials;
            if (overrideMaterials == null)
            {
                renderer.materials = materials;
            }
        }

        public void ClearMaterials()
        {
            CacheRendererAndMaterialsOnce();
            materials = null;
            renderer.materials = overrideMaterials ?? sharedMaterials;
        }

        public void SetOverrideMaterials(Material[] materials)
        {
            CacheRendererAndMaterialsOnce();
            overrideMaterials = materials;
            renderer.materials = overrideMaterials;
        }

        public void ClearOverrideMaterials()
        {
            CacheRendererAndMaterialsOnce();
            overrideMaterials = null;
            renderer.materials = materials ?? sharedMaterials;
        }

        void CacheRendererAndMaterialsOnce()
        {
            if (isCached) return;
            renderer = GetComponent<Renderer>();
            sharedMaterials = renderer.sharedMaterials;
            isCached = true;
        }
    }
}
