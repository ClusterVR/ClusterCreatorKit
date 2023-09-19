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
        [SerializeField] bool isBeta;

        public PostNewVenuePayload(string name, string description, string groupId, bool isBeta)
        {
            this.name = name;
            this.description = description;
            this.groupId = groupId;
            this.isBeta = isBeta;
        }
    }
}
