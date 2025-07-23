using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Observer
{
    public static class LogoutObserver
    {
        static TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;
        static VenueRepository VenueRepository => VenueRepository.Instance;
        static GroupRepository GroupRepository => GroupRepository.Instance;
        static ExternalCallEndpointRepository ExternalCallEndpointRepository => ExternalCallEndpointRepository.Instance;
        static ExternalCallVerifyTokenRepository ExternalCallVerifyTokenRepository => ExternalCallVerifyTokenRepository.Instance;


        [InitializeOnLoadMethod]
        static void ObserveUserInfoChanged()
        {
            ReactiveBinder.Bind(TokenAuthRepository.UserInfo, _ =>
            {
                VenueRepository.Clear();
                GroupRepository.Clear();
                ExternalCallEndpointRepository.Clear();
                ExternalCallVerifyTokenRepository.Clear();
            });
        }
    }
}
