using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PatchVenueSettingService
    {
        readonly string accessToken;
        readonly Venue.Venue venue;
        readonly string name;
        readonly string description;
        readonly List<ThumbnailUrl> thumbnailUrls;
        readonly string thumbnailImagePath;

        public PatchVenueSettingService(
            string accessToken,
            Venue.Venue venue,
            string name,
            string description,
            List<ThumbnailUrl> thumbnailUrls,
            string thumbnailImagePath = ""
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.name = name;
            this.description = description;
            this.thumbnailUrls = thumbnailUrls;
            this.thumbnailImagePath = thumbnailImagePath;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await PatchVenueAsync(cancellationToken);
        }

        async Task PatchVenueAsync(CancellationToken cancellationToken)
        {
            var thumbnailUrl = "";

            if (!string.IsNullOrEmpty(thumbnailImagePath))
            {
                var uploadThumbnail = new UploadThumbnailService(accessToken, thumbnailImagePath);
                var policy = await uploadThumbnail.RunAsync(cancellationToken);
                thumbnailUrl = policy.url;
            }

            var payload = new PatchVenuePayload(name, description,
                string.IsNullOrEmpty(thumbnailUrl)
                    ? thumbnailUrls
                    : new List<ThumbnailUrl> { new ThumbnailUrl(thumbnailUrl) });
            await PatchVenueAsync(payload, cancellationToken);
        }

        async Task PatchVenueAsync(PatchVenuePayload payload, CancellationToken cancellationToken)
        {
            await APIServiceClient.PatchVenue(venue.VenueId, payload, accessToken, cancellationToken);
        }
    }
}
