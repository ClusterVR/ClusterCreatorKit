using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public class Venues
    {
        [SerializeField] List<Venue> venues;

        public List<Venue> List => venues;
    }
}
