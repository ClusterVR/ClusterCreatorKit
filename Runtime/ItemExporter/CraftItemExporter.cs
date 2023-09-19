using System.Threading.Tasks;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class CraftItemExporter : IItemExporter
    {
        static VGltf.Unity.Exporter CreateExporter(bool isBeta)
        {
            var exporter = new VGltf.Unity.Exporter();
            exporter.AddHook(new ItemExporterHook(useDynamic: isBeta));
            exporter.Context.Exporters.Nodes.AddHook(new ItemNodeExporterHook());

            return exporter;
        }

        public GltfContainer ExportAsGltfContainer(GameObject go, bool isBeta)
        {
            using var exporter = CreateExporter(isBeta);

            return ItemExporter.ExportAsGltfContainer(go, exporter);
        }

        public async Task<byte[]> ExportAsync(GameObject go, bool isBeta)
        {
            using var exporter = CreateExporter(isBeta);

            return await ItemExporter.ExportAsync(go, exporter);
        }
    }
}
