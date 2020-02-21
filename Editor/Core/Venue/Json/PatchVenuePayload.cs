using System;
using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class PatchVenuePayload
    {
        public string description;
        public string name;
        public List<ThumbnailUrl> thumbnailUrls;
    }
}
