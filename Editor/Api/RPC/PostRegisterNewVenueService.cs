using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    [Obsolete]
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

        public void Run(CancellationToken cancellationToken)
        {
            RunAsync(cancellationToken).Forget();
        }

        public async Task<Venue.Venue> RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await PostRegisterNewVenueAsync(cancellationToken);
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
                throw;
            }
        }

        async Task<Venue.Venue> PostRegisterNewVenueAsync(CancellationToken cancellationToken)
        {
            var response = await APIServiceClient.PostRegisterNewVenue(payload, accessToken, cancellationToken);
            onSuccess?.Invoke(response);
            return response;
        }
    }
}
