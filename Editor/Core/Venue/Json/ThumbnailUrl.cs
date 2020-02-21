using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class ThumbnailUrl
    {
        [SerializeField] string url;
        public string Url => url;

        public ThumbnailUrl(string url)
        {
            this.url = url;
        }
    }
}
