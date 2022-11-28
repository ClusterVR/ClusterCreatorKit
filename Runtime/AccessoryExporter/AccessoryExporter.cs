using System.Threading.Tasks;
using ClusterVR.CreatorKit.AccessoryExporter.ExporterHooks;
using ClusterVR.CreatorKit.ItemExporter;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.AccessoryExporter
{
    public sealed class AccessoryExporter : IItemExporter
    {
        static VGltf.Unity.Exporter CreateExporter()
        {
            var exporter = new VGltf.Unity.Exporter();
            exporter.AddHook(new ItemExporterHook());
            exporter.Context.Exporters.Nodes.AddHook(new ItemNodeExporterHook());
            exporter.Context.Exporters.Materials.AddHook(new VRM0MtoonExporterHook());

            return exporter;
        }

        public GltfContainer ExportAsGltfContainer(GameObject go)
        {
            using var exporter = CreateExporter();

            return ItemExporter.ItemExporter.ExportAsGltfContainer(go, exporter);
        }

        public async Task<byte[]> ExportAsync(GameObject go)
        {
            using var exporter = CreateExporter();

            return await ItemExporter.ItemExporter.ExportAsync(go, exporter);
        }
    }
}
