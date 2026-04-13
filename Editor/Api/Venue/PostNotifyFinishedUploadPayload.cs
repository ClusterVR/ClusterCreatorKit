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
        [SerializeField] VenueRevisionDisplayItemType[] displayItems;
        [SerializeField] string worldDescriptor;
        [SerializeField] bool isPreview;
        [SerializeField] string renderPipelinePresetId;

        public PostNotifyFinishedUploadPayload(
            VenueRevisionDisplayItemType[] displayItems,
            WorldDescriptor worldDescriptor,
            bool isPreview,
            string renderPipelinePresetId
            )
        {
            this.displayItems = displayItems;
            this.worldDescriptor = worldDescriptor.ToByteArray().ToSafeBase64();
            this.isPreview = isPreview;
            this.renderPipelinePresetId = renderPipelinePresetId;
        }
    }
}
