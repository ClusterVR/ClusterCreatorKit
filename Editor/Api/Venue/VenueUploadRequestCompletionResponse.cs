using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class VenueUploadRequestCompletionResponse
    {
        [SerializeField] string uploadRequestId;
        [SerializeField] string url;

        public UploadRequestID UploadRequestId => new UploadRequestID(uploadRequestId);
        public string Url => url;
    }
}
