using VJson;
using VJson.Schema;

namespace ClusterVR.CreatorKit.GltfExtensions
{
    [JsonSchema(Title = "cluster-item-node", Id = "cluster-item-node.schema.json")]
    public sealed class ClusterItemNode
    {
        public const string ExtensionName = "ClusterItemNode";

        [JsonField(Name = "itemNode", Order = 0), JsonSchemaRequired]
        public string ItemNode;
    }
}
