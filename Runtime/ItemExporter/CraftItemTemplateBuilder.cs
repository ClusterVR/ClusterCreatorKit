namespace ClusterVR.CreatorKit.ItemExporter
{
    public sealed class CraftItemTemplateBuilder : IItemTemplateBuilder
    {
        const string GlbEntryName = "item_template.glb";
        const string IconEntryName = "icon.png";

        public byte[] Build(byte[] glbBinary, byte[] thumbnailBinary)
        {
            return ItemTemplateBuilder.Build(GlbEntryName, glbBinary, IconEntryName, thumbnailBinary);
        }
    }
}
