using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostNewVenuePayload
    {
        [SerializeField] string name;
        [SerializeField] string description;
        [SerializeField] string groupId;

        public PostNewVenuePayload(string name, string description, string groupId)
        {
            this.name = name;
            this.description = description;
            this.groupId = groupId;
        }
    }
}
