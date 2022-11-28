namespace ClusterVR.CreatorKit.ItemExporter
{
    public interface IItemTemplateBuilder
    {
        public byte[] Build(byte[] glbBinary, byte[] thumbnailBinary);
    }
}
