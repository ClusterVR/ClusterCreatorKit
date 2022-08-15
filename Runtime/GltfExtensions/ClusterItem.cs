using VJson;
using VJson.Schema;

namespace ClusterVR.CreatorKit.GltfExtensions
{
    [JsonSchema(Title = "cluster-item", Id = "cluster-item.schema.json")]
    public sealed class ClusterItem
    {
        public const string ExtensionName = "ClusterItem";

        [JsonField(Name = "item", Order = 0), JsonSchemaRequired]
        public string Item;
    }
}
