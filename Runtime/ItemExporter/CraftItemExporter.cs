using System.Threading.Tasks;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class CraftItemExporter : IItemExporter
    {
        static VGltf.Unity.Exporter CreateExporter()
        {
            var exporter = new VGltf.Unity.Exporter();
            exporter.AddHook(new ItemExporterHook());
            exporter.Context.Exporters.Nodes.AddHook(new ItemNodeExporterHook());

            return exporter;
        }

        public GltfContainer ExportAsGltfContainer(GameObject go)
        {
            using var exporter = CreateExporter();

            return ItemExporter.ExportAsGltfContainer(go, exporter);
        }

        public async Task<byte[]> ExportAsync(GameObject go)
        {
            using var exporter = CreateExporter();

            return await ItemExporter.ExportAsync(go, exporter);
        }
    }
}
