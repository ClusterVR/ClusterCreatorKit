using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostUploadVenueAssetPoliciesPayload
    {
        [SerializeField] string fileType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;

        public PostUploadVenueAssetPoliciesPayload(string fileType, string fileName, long fileSize)
        {
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}
