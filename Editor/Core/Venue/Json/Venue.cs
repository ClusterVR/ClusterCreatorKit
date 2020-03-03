using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class Venue
    {
        [SerializeField] string description;
        [SerializeField] string name;
        [SerializeField] Owner owner;
        [SerializeField] Group group;
        [SerializeField] ThumbnailUrl[] thumbnailUrls;
        [SerializeField] string venueId;
        [SerializeField] string worldDetailUrl;

        public string Description => description;
        public string Name => name;
        public Owner Owner => owner;
        public Group Group => group;
        public ThumbnailUrl[] ThumbnailUrls => thumbnailUrls;
        public VenueID VenueId => new VenueID(venueId);
        public string WorldDetailUrl => worldDetailUrl;
    }
}
