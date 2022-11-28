using System.Threading;
using System.Threading.Tasks;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public interface IItemUploadService
    {
        void SetAccessToken(string accessToken);

        Task<string> UploadItemAsync(byte[] binary, CancellationToken cancellationToken);
        string UploadedItemsManagementUrl { get; }
    }
}
