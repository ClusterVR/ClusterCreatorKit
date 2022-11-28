using VJson;
using VJson.Schema;

namespace ClusterVR.CreatorKit.GltfExtensions
{
    [JsonSchema(Title = "cluster-vrm0-mtoon", Id = "cluster-vrm0-mtoon.schema.json")]
    public sealed class ClusterVRM0MToon
    {
        public static readonly string ExtensionName = "ClusterVRM0MToon";

        public VGltf.Ext.Vrm0.Types.Material MToonMat;
    }
}
