using System.Threading.Tasks;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class ItemExporter
    {
        public static GltfContainer ExportAsGltfContainer(GameObject go)
        {
            using var exporter = new VGltf.Unity.Exporter();
            exporter.AddHook(new ItemExporterHook());
            exporter.Context.Exporters.Nodes.AddHook(new ItemNodeExporterHook());

            exporter.ExportGameObjectAsScene(go);

            return exporter.IntoGlbContainer();
        }

        public static Task<byte[]> ExportAsync(GameObject go)
        {
            var gltfContainer = ExportAsGltfContainer(go);
            return gltfContainer.ExportAsync();
        }
    }
}
