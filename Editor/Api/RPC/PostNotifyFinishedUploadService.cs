using System;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public class PostNotifyFinishedUploadService
    {
        readonly string accessToken;
        readonly bool isPublish;
        readonly Action<Exception> onError;
        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;
        readonly UploadRequestID uploadRequestId;
        readonly VenueID venueId;

        public PostNotifyFinishedUploadService(
            string accessToken,
            VenueID venueId,
            UploadRequestID uploadRequestId,
            bool isPublish,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.uploadRequestId = uploadRequestId;
            this.isPublish = isPublish;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            _ = PostNotifyFinishedUploadAsync();
        }

        async Task PostNotifyFinishedUploadAsync()
        {
            var payload = new PostNotifyFinishedUploadPayload(isPublish);
            try
            {
                var response = await APIServiceClient.PostNotifyFinishedUpload(venueId, uploadRequestId, payload, accessToken);
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
