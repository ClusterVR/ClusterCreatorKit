using System.Threading;
using System.Threading.Tasks;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public interface IItemUploadService
    {
        void SetAccessToken(string accessToken);

        Task<string> UploadItemAsync(byte[] binary, bool isBeta, CancellationToken cancellationToken);
        string UploadedItemsManagementUrl { get; }
        bool ApplyBeta { get; }
    }
}
