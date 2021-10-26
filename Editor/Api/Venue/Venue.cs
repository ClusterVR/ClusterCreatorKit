using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class Venue
    {
        [SerializeField] string description;
        [SerializeField] string name;
        [SerializeField] Group group;
        [SerializeField] ThumbnailUrl[] thumbnailUrls;
        [SerializeField] string venueId;
        [SerializeField] string worldDetailUrl;

        public string Description => description;
        public string Name => name;
        public Group Group => group;
        public ThumbnailUrl[] ThumbnailUrls => thumbnailUrls;
        public VenueID VenueId => new VenueID(venueId);
        public string WorldDetailUrl => worldDetailUrl;
    }
}
