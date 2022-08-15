using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PatchVenueSettingService
    {
        readonly string accessToken;
        readonly Venue.Venue venue;
        readonly string name;
        readonly string description;
        List<ThumbnailUrl> thumbnailUrls;
        readonly string thumbnailImagePath;
        readonly Action<Exception> onError;
        readonly Action<Venue.Venue> onSuccess;

        bool isProcessing;

        public PatchVenueSettingService(
            string accessToken,
            Venue.Venue venue,
            string name,
            string description,
            List<ThumbnailUrl> thumbnailUrls,
            string thumbnailImagePath = "",
            Action<Venue.Venue> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.name = name;
            this.description = description;
            this.thumbnailUrls = thumbnailUrls;
            this.thumbnailImagePath = thumbnailImagePath;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run(CancellationToken cancellationToken)
        {
            EditorCoroutine.Start(PatchVenue(cancellationToken));
        }

        IEnumerator PatchVenue(CancellationToken cancellationToken)
        {
            isProcessing = true;
            var isUploading = false;
            var thumbnailUrl = "";

            if (!string.IsNullOrEmpty(thumbnailImagePath))
            {
                var uploadThumbnail = new UploadThumbnailService(
                    accessToken,
                    thumbnailImagePath,
                    policy =>
                    {
                        thumbnailUrl = policy.url;
                        isUploading = false;
                    },
                    e =>
                    {
                        isUploading = false;
                        Debug.LogException(e);
                        onError?.Invoke(e);
                        isProcessing = false;
                    }
                );
                isUploading = true;
                uploadThumbnail.Run(cancellationToken);

                while (isUploading)
                {
                    yield return null;
                }

                if (!isProcessing)
                {
                    yield break;
                }
            }

            var payload = new PatchVenuePayload(name, description,
                string.IsNullOrEmpty(thumbnailUrl)
                    ? thumbnailUrls
                    : new List<ThumbnailUrl> { new ThumbnailUrl(thumbnailUrl) });
            _ = PatchVenueAsync(payload, cancellationToken);
        }

        async Task PatchVenueAsync(PatchVenuePayload payload, CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await APIServiceClient.PatchVenue(venue.VenueId, payload, accessToken, cancellationToken);
                onSuccess?.Invoke(response);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                onError?.Invoke(e);
            }
            finally
            {
                isProcessing = false;
            }
        }
    }
}
