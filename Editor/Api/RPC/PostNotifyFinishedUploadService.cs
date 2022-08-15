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
        readonly bool isPublish;
        readonly Action<Exception> onError;
        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;
        readonly UploadRequestID uploadRequestId;
        readonly VenueID venueId;
        readonly WorldDescriptor worldDescriptor;

        public PostNotifyFinishedUploadService(
            string accessToken,
            VenueID venueId,
            UploadRequestID uploadRequestId,
            bool isPublish,
            WorldDescriptor worldDescriptor,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.uploadRequestId = uploadRequestId;
            this.isPublish = isPublish;
            this.worldDescriptor = worldDescriptor;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run(CancellationToken cancellationToken)
        {
            _ = PostNotifyFinishedUploadAsync(cancellationToken);
        }

        async Task PostNotifyFinishedUploadAsync(CancellationToken cancellationToken)
        {
            var payload = new PostNotifyFinishedUploadPayload(isPublish, worldDescriptor);
            try
            {
                var response =
                    await APIServiceClient.PostNotifyFinishedUpload(venueId, uploadRequestId, payload, accessToken,
                        cancellationToken);
                onSuccess?.Invoke(response);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                onError?.Invoke(e);
            }
        }
    }
}
