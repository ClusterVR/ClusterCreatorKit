using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostUploadAssetPolicyPayload
    {
        [SerializeField] string[] assetIdsDependsOn;
        [SerializeField] string fileType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;
        [SerializeField] string sceneType;

        public PostUploadAssetPolicyPayload(string[] assetIdsDependsOn, string fileType, string fileName, long fileSize, string sceneType)
        {
            this.assetIdsDependsOn = assetIdsDependsOn;
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
            this.sceneType = sceneType;
        }
    }
}
