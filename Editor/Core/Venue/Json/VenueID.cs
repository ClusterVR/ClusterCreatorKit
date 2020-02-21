using System;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class VenueID : StringValueObject
    {
        public VenueID(string value) : base(value)
        {
        }
    }
}
