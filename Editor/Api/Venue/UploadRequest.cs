using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class UploadRequest
    {
        [SerializeField] string uploadRequestId;

        public UploadRequestID UploadRequestId => new UploadRequestID(uploadRequestId);
    }
}
