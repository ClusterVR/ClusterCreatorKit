using System.IO;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class ItemExporter
    {
        public Task<byte[]> Export(GltfContainer gltfContainer)
        {
            return Task.Run(() =>
            {
                using (var s = new MemoryStream())
                {
                    VGltf.Glb.Writer.WriteFromContainer(s, gltfContainer);
                    s.Flush();

                    return s.ToArray();
                }
            });
        }
    }
}
