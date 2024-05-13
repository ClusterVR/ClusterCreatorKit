using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Proto;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PostNotifyFinishedUploadService
    {
        readonly string accessToken;

        public PostNotifyFinishedUploadService(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<VenueUploadRequestCompletionResponse> PostNotifyFinishedUploadAsync(VenueID venueId,
            UploadRequestID uploadRequestId, WorldDescriptor worldDescriptor, bool isPreview, CancellationToken cancellationToken)
        {
            var payload = new PostNotifyFinishedUploadPayload(worldDescriptor, isPreview);
            return await APIServiceClient.PostNotifyFinishedUpload(venueId, uploadRequestId, payload, accessToken, cancellationToken);
        }
    }
}
