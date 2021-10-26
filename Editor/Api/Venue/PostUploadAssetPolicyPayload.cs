using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostUploadAssetPolicyPayload
    {
        [SerializeField] string fileType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;

        public PostUploadAssetPolicyPayload(string fileType, string fileName, long fileSize)
        {
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}
