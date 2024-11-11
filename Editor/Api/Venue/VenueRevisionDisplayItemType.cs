using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class VenueRevisionDisplayItemType
    {
        [SerializeField] string productUgcId;

        public VenueRevisionDisplayItemType(string productUgcId)
        {
            this.productUgcId = productUgcId;
        }
    }
}
