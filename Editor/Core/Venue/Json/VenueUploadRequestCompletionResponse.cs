using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class VenueUploadRequestCompletionResponse
    {
        [SerializeField] string uploadRequestId;
        [SerializeField] string url;

        public UploadRequestID UploadRequestId => new UploadRequestID(uploadRequestId);
        public string Url => url;
    }
}
