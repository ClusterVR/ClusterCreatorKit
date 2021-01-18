using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public class PostNotifyFinishedUploadPayload
    {
        [SerializeField] bool isPublish;

        public bool IsPublish => isPublish;

        public PostNotifyFinishedUploadPayload(bool isPublish)
        {
            this.isPublish = isPublish;
        }
    }
}
