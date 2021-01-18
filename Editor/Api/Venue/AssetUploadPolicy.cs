using System;
using Newtonsoft.Json.Linq;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public class AssetUploadPolicy
    {
        public string fileType { get; set; }
        public string fileName { get; set; }
        public JObject form { get; set; }
        public string uploadUrl { get; set; }
        public string url { get; set; }
    }
}
