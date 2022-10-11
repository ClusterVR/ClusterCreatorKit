using System.IO;
using System.IO.Compression;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class ItemTemplateBuilder
    {
        const string GlbEntryName = "item_template.glb";
        const string IconEntryName = "icon.png";

        public byte[] Build(byte[] glbBinary, byte[] thumbnailBinary)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var glbEntry = archive.CreateEntry(GlbEntryName);
                    using (var entryStream = glbEntry.Open())
                    using (var binaryWriter = new BinaryWriter(entryStream))
                    {
                        binaryWriter.Write(glbBinary);
                    }

                    var pngFile = archive.CreateEntry(IconEntryName);
                    using (var entryStream = pngFile.Open())
                    using (var binaryWriter = new BinaryWriter(entryStream))
                    {
                        binaryWriter.Write(thumbnailBinary);
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}
