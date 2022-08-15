using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ItemTemplate
{
    [Serializable]
    public sealed class UploadItemTemplatePoliciesPayload
    {
        [SerializeField] string contentType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;

        public UploadItemTemplatePoliciesPayload(string contentType, string fileName, long fileSize)
        {
            this.contentType = contentType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}
