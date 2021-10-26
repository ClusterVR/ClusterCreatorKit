using System;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PostUploadRequestService
    {
        readonly string accessToken;
        readonly VenueID venueId;
        readonly Action<UploadRequest> onSuccess;
        readonly Action<Exception> onError;

        public PostUploadRequestService(
            string accessToken,
            VenueID venueId,
            Action<UploadRequest> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            _ = PostUploadRequestAsync();
        }

        async Task PostUploadRequestAsync()
        {
            try
            {
                var response = await APIServiceClient.PostUploadRequest(venueId, accessToken);
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
