using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Extensions
{
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("")]
    public sealed class RendererMaterialHolder : MonoBehaviour
    {
        Renderer renderer;
        Material[] sharedMaterials;
        Material[] materials;
        Material[] overrideMaterials;
        bool isCached;

        void Awake()
        {
            TryCacheRendererAndMaterialsOnce();
        }

        public Material[] GetSharedMaterials()
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            return sharedMaterials;
        }

        public void SetSharedMaterials(Material[] materials)
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            sharedMaterials = materials;
            renderer.sharedMaterials = materials;
        }

        public void SetMaterials(Material[] materials)
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            this.materials = materials;
            if (overrideMaterials == null)
            {
                renderer.materials = materials;
            }
        }

        public void ClearMaterials()
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            materials = null;
            renderer.materials = overrideMaterials ?? sharedMaterials;
        }

        public void SetOverrideMaterials(Material[] materials)
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            overrideMaterials = materials;
            renderer.materials = overrideMaterials;
        }

        public void ClearOverrideMaterials()
        {
            if (!TryCacheRendererAndMaterialsOnce())
            {
                throw new InvalidOperationException();
            }
            overrideMaterials = null;
            renderer.materials = materials ?? sharedMaterials;
        }

        bool TryCacheRendererAndMaterialsOnce()
        {
            if (isCached) return true;
            if (!TryGetComponent(out renderer))
            {
                return false;
            }
            sharedMaterials = renderer.sharedMaterials;
            isCached = true;
            return true;
        }
    }
}
