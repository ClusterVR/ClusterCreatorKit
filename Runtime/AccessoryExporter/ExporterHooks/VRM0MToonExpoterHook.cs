using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.AccessoryExporter.ExporterHooks
{
    public sealed class VRM0MtoonExporterHook : MaterialExporterHook
    {
        public override IndexedResource<Material> Export(IExporterContext context, Material mat)
        {
            if (mat.shader.name != "VRM/MToon")
            {
                return context.Exporters.Materials.ForceExportUnlit(mat);
            }

            var gltfMaterial = new VGltf.Types.Material
            {
                Name = mat.name,
            };

            var bridge = new Bridge.VRM0MToonExporterBridge();
            var vrmMat = bridge.CreateMaterialProp(context, mat);

            var ext = new GltfExtensions.ClusterVRM0MToon { MToonMat = vrmMat };

            gltfMaterial.AddExtra(GltfExtensions.ClusterVRM0MToon.ExtensionName, ext);

            var matIndex = context.Gltf.AddMaterial(gltfMaterial);
            var resource = context.Resources.Materials.Add(mat, matIndex, mat.name, mat);

            return resource;
        }
    }
}
