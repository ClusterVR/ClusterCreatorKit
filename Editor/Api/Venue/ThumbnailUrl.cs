using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class ThumbnailUrl
    {
        [SerializeField] string url;
        public string Url => url;

        public ThumbnailUrl(string url)
        {
            this.url = url;
        }
    }
}
