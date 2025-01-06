using VJson;
using VJson.Schema;

namespace ClusterVR.CreatorKit.GltfExtensions
{
    [JsonSchema(Title = "cluster-humanoid-animation", Id = "cluster-humanoid-animation.schema.json")]
    public sealed class ClusterHumanoidAnimation
    {
        public const string ExtensionName = "ClusterHumanoidAnimation";

        [JsonField(Name = "isLoop", Order = 0)]
        public bool IsLoop;
    }
}
