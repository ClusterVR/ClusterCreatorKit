using System.Threading.Tasks;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class ItemExporter
    {
        public static GltfContainer ExportAsGltfContainer(GameObject go, VGltf.Unity.Exporter exporter)
        {
            exporter.ExportGameObjectAsScene(go);

            return exporter.IntoGlbContainer();
        }

        public static Task<byte[]> ExportAsync(GameObject go, VGltf.Unity.Exporter exporter)
        {
            var gltfContainer = ExportAsGltfContainer(go, exporter);
            return gltfContainer.ExportAsync();
        }
    }
}
