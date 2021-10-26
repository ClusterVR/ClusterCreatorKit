using System;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class GroupID : StringValueObject
    {
        public GroupID(string value) : base(value)
        {
        }
    }
}
