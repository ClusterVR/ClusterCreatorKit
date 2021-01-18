using System;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public class VenueID : StringValueObject
    {
        public VenueID(string value) : base(value)
        {
        }
    }
}
