using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.ItemExporter;
using ClusterVR.CreatorKit.ItemExporter;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.AccessoryExporter
{
    public sealed class AccessoryTemplateBuilder : IItemTemplateBuilder
    {
        const string GlbEntryName = "accessory_template.glb";
        const string IconEntryName = "icon.png";

        public async Task<byte[]> Build(GameObject go)
        {
            var exporter = new CreatorKit.AccessoryExporter.AccessoryExporter();
            var glbBinary = await exporter.ExportAsync(go);

            using (var itemPreviewRenderer = new ItemPreviewImage())
            {
                var pngBinary = itemPreviewRenderer.CreatePNG(go);

                return Build(glbBinary, pngBinary);
            }
        }

        public byte[] Build(byte[] glbBinary, byte[] thumbnailBinary)
        {
            return ItemTemplateBuilder.Build(GlbEntryName, glbBinary, IconEntryName, thumbnailBinary);
        }
    }
}
