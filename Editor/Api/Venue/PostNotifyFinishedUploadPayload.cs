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
        [SerializeField] string worldDescriptor;

        public PostNotifyFinishedUploadPayload(WorldDescriptor worldDescriptor)
        {
            this.worldDescriptor = worldDescriptor.ToByteArray().ToSafeBase64();
        }
    }
}
