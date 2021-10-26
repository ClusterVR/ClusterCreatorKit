using System;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PostRegisterNewVenueService
    {
        readonly string accessToken;
        readonly Action<Exception> onError;
        readonly Action<Venue.Venue> onSuccess;
        readonly PostNewVenuePayload payload;

        public PostRegisterNewVenueService(
            string accessToken,
            PostNewVenuePayload payload,
            Action<Venue.Venue> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.payload = payload;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            _ = PostRegisterNewVenueAsync();
        }

        async Task PostRegisterNewVenueAsync()
        {
            try
            {
                var response = await APIServiceClient.PostRegisterNewVenue(payload, accessToken);
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
