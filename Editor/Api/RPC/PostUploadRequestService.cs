using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PostUploadRequestService
    {
        readonly string accessToken;
        readonly bool isBeta;
        readonly bool isPreview;

        public PostUploadRequestService(string accessToken, bool isBeta, bool isPreview)
        {
            this.accessToken = accessToken;
            this.isBeta = isBeta;
            this.isPreview = isPreview;
        }

        public async Task<UploadRequest> PostUploadRequestAsync(VenueID venueId, CancellationToken cancellationToken)
        {
            return await APIServiceClient.PostUploadRequest(venueId, isBeta, isPreview, accessToken, cancellationToken);
        }
    }
}
