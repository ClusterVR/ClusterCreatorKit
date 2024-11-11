using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Proto;

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
            UploadRequestID uploadRequestId, WorldDescriptor worldDescriptor, bool isPreview, string[] productUgcIds,
            CancellationToken cancellationToken)
        {
            var payload = new PostNotifyFinishedUploadPayload(
                productUgcIds.Select(x => new VenueRevisionDisplayItemType(x)).ToArray(),
                worldDescriptor,
                isPreview);
            return await APIServiceClient.PostNotifyFinishedUpload(venueId, uploadRequestId, payload, accessToken, cancellationToken);
        }
    }
}
