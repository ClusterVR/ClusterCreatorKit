using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Infrastructure;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class TokenAuthFailedException : Exception { }

    public sealed class TokenAuthRepository
    {
        public static readonly TokenAuthRepository Instance = new();

        bool isLoggingIn;
        readonly Reactive<UserInfo?> userInfo = new();

        readonly Reactive<AuthenticationInfo> savedAccessToken = new();
        public IReadOnlyReactive<AuthenticationInfo> SavedAccessToken => savedAccessToken;
        public IReadOnlyReactive<string> SavedUserId => ReactiveEditorPrefs.SavedUserId;

        public IReadOnlyReactive<UserInfo?> UserInfo => userInfo;
        public UserInfo GetLoggedIn() => userInfo.Val ?? throw new TokenAuthFailedException();

        ReactiveEditorPrefs ReactiveEditorPrefs => ReactiveEditorPrefs.Instance;

        TokenAuthRepository()
        {
            ReactiveBinder.Bind(ReactiveEditorPrefs.SavedAccessToken, token =>
            {
                savedAccessToken.Val = new AuthenticationInfo(token);
            });
        }

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
                ReactiveEditorPrefs.SetSavedAccessToken(authInfo.RawValue);
                ReactiveEditorPrefs.SetSavedUserId(user.UserId);
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
            ReactiveEditorPrefs.SetSavedAccessToken("");
            ReactiveEditorPrefs.SetSavedUserId("");
        }
    }
}
