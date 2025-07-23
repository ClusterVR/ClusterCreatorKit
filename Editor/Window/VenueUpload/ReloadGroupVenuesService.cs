using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Repository;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class ReloadGroupVenuesService
    {
        public static ReloadGroupVenuesService Instance => new();

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;
        GroupRepository GroupRepository => GroupRepository.Instance;
        VenueRepository VenueRepository => VenueRepository.Instance;

        public async Task ReloadGroupVenuesAsync(CancellationToken cancellationToken)
        {
            await VenueRepository.ReloadGroupVenuesAsync(
                GroupRepository.CurrentGroup.Val.Id,
                TokenAuthRepository.GetLoggedIn().VerifiedToken,
                cancellationToken);
        }
    }
}
