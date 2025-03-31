using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Window.View;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class TokenAuthFailedException : Exception { }

    public sealed class TokenAuthRepository
    {
        public static readonly TokenAuthRepository Instance = new();

        bool isLoggingIn;
        readonly Reactive<UserInfo?> userInfo = new();

        public Reactive<UserInfo?> UserInfo => userInfo;

        private TokenAuthRepository() { }

        public async Task LoginAsync(AuthenticationInfo authInfo, CancellationToken cancellationToken)
        {
            if (authInfo.Token == userInfo.Val?.VerifiedToken)
            {
                return;
            }

            if (isLoggingIn)
            {
                return;
            }

            Api.RPC.Constants.OverrideHost(authInfo.Host);
            isLoggingIn = true;
            try
            {
                var user = await APIServiceClient.GetMyUser(authInfo.Token, cancellationToken);

                if (string.IsNullOrEmpty(user.Username))
                {
                    throw new TokenAuthFailedException();
                }

                userInfo.Val = new UserInfo(user.Username, authInfo.Token);
                EditorPrefsUtils.SavedAccessToken = authInfo;
                EditorPrefsUtils.SavedUserId = user.UserId;
            }
            finally
            {
                isLoggingIn = false;
            }
        }

        public void Logout()
        {
            if (isLoggingIn)
            {
                return;
            }

            userInfo.Val = null;
            EditorPrefsUtils.SavedAccessToken = null;
            EditorPrefsUtils.SavedUserId = null;
        }
    }
}
