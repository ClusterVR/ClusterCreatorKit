using System;
using Newtonsoft.Json.Linq;

namespace ClusterVR.CreatorKit.Editor.Api.AccessoryTemplate
{
    [Serializable]
    public sealed class UploadAccessoryTemplatePoliciesResponse
    {
        public JObject form { get; set; }
        public string accessoryTemplateID { get; set; }
        public string statusApiUrl { get; set; }
        public string uploadUrl { get; set; }
    }
}
