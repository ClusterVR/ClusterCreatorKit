using UnityEngine;

namespace ClusterVR.CreatorKit.Extensions
{
    public static class RendererMaterialUtility
    {
        public static Material[] GetSharedMaterials(Renderer renderer)
        {
            return GetOrAddMaterialHolder(renderer).GetSharedMaterials();
        }

        public static void SetMaterials(Renderer renderer, Material[] materials)
        {
            GetOrAddMaterialHolder(renderer).SetMaterials(materials);
        }

        public static void SetOverrideMaterials(Renderer renderer, Material[] materials)
        {
            GetOrAddMaterialHolder(renderer).SetOverrideMaterials(materials);
        }

        public static void ClearOverrideMaterials(Renderer renderer)
        {
            if (renderer.TryGetComponent<RendererMaterialHolder>(out var holder))
            {
                holder.ClearOverrideMaterials();
            }
        }

        public static void ClearMaterials(Renderer renderer)
        {
            if (renderer.TryGetComponent<RendererMaterialHolder>(out var holder))
            {
                holder.ClearMaterials();
            }
        }

        static RendererMaterialHolder GetOrAddMaterialHolder(Renderer renderer)
        {
            if (!renderer.TryGetComponent<RendererMaterialHolder>(out var holder))
            {
                holder = renderer.gameObject.AddComponent<RendererMaterialHolder>();
            }
            return holder;
        }
    }
}
