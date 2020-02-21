using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class ThumbnailUploadPolicy
    {
        public string contentType { get; set; }
        public string fileName { get; set; }
        public int fileSize { get; set; }
        public JObject form { get; set; }
        public string uploadUrl { get; set; }
        public string url { get; set; }
    }
}
