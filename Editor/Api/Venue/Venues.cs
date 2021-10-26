using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class Venues
    {
        [SerializeField] List<Venue> venues;

        public List<Venue> List => venues;
    }
}
