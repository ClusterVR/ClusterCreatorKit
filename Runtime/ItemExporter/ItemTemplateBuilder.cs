using System.IO;
using System.IO.Compression;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public static class ItemTemplateBuilder
    {
        public static byte[] Build(string glbEntryName, byte[] glbBinary, string iconEntryName, byte[] thumbnailBinary)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var glbEntry = archive.CreateEntry(glbEntryName);
                    using (var entryStream = glbEntry.Open())
                    using (var binaryWriter = new BinaryWriter(entryStream))
                    {
                        binaryWriter.Write(glbBinary);
                    }

                    var pngFile = archive.CreateEntry(iconEntryName);
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
