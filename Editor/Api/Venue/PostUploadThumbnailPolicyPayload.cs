using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostUploadThumbnailPolicyPayload
    {
        [SerializeField] string contentType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;

        public PostUploadThumbnailPolicyPayload(string contentType, string fileName, long fileSize)
        {
            this.contentType = contentType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}
