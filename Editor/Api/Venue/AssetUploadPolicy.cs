using System;
using Newtonsoft.Json.Linq;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class AssetUploadPolicy
    {
        public string fileName { get; set; }
        public string fileType { get; set; }
        public JObject form { get; set; }
        public string uploadRequestId { get; set; }
        public string uploadUrl { get; set; }
    }
}
