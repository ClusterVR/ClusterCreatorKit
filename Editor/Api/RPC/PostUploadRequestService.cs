using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class PostUploadRequestService
    {
        readonly string accessToken;
        readonly bool isBeta;

        public PostUploadRequestService(string accessToken, bool isBeta)
        {
            this.accessToken = accessToken;
            this.isBeta = isBeta;
        }

        public async Task<UploadRequest> PostUploadRequestAsync(VenueID venueId, CancellationToken cancellationToken)
        {
            return await APIServiceClient.PostUploadRequest(venueId, isBeta, accessToken, cancellationToken);
        }
    }
}
