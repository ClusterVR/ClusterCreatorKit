using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class UploadRequest
    {
        [SerializeField] string uploadRequestId;

        public UploadRequestID UploadRequestId => new UploadRequestID(uploadRequestId);
    }
}
