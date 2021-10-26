using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PatchVenuePayload
    {
        [SerializeField] string name;
        [SerializeField] string description;
        [SerializeField] List<ThumbnailUrl> thumbnailUrls;

        public PatchVenuePayload(string name, string description, List<ThumbnailUrl> thumbnailUrls)
        {
            this.name = name;
            this.description = description;
            this.thumbnailUrls = thumbnailUrls;
        }
    }
}
