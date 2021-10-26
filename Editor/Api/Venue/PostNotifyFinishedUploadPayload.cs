using System;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class PostNotifyFinishedUploadPayload
    {
        [SerializeField] bool isPublish;
        [SerializeField] string worldDescriptor;

        public bool IsPublish => isPublish;

        public PostNotifyFinishedUploadPayload(bool isPublish, WorldDescriptor worldDescriptor)
        {
            this.isPublish = isPublish;
            this.worldDescriptor = worldDescriptor.ToByteArray().ToSafeBase64();
        }
    }
}
