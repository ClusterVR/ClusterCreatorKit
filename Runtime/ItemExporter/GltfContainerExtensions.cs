using System.IO;
using System.Threading.Tasks;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public static class GltfContainerExtensions
    {
        public static Task<byte[]> ExportAsync(this GltfContainer gltfContainer)
        {
            return Task.Run(() =>
            {
                using var s = new MemoryStream();
                VGltf.Glb.Writer.WriteFromContainer(s, gltfContainer);
                s.Flush();
                return s.ToArray();
            });
        }
    }
}
