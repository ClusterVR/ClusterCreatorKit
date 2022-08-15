using System;
using Newtonsoft.Json.Linq;

namespace ClusterVR.CreatorKit.Editor.Api.ItemTemplate
{
    [Serializable]
    public sealed class UploadItemTemplatePoliciesResponse
    {
        public JObject form { get; set; }
        public string itemTemplateID { get; set; }
        public string statusApiUrl { get; set; }
        public string uploadUrl { get; set; }
    }
}
