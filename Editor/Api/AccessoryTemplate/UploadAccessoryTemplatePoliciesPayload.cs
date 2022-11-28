using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.AccessoryTemplate
{
    [Serializable]
    public sealed class UploadAccessoryTemplatePoliciesPayload
    {
        [SerializeField] string accessoryTemplateID;
        [SerializeField] string contentType;
        [SerializeField] string fileName;
        [SerializeField] long fileSize;

        public UploadAccessoryTemplatePoliciesPayload(string accessoryTemplateID, string contentType, string fileName, long fileSize)
        {
            this.accessoryTemplateID = accessoryTemplateID;
            this.contentType = contentType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}
