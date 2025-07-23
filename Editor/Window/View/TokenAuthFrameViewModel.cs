using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class TokenAuthFrameViewModel : IDisposable
    {
        readonly IRequireTokenAuthMainView requireTokenAuthMainView;

        readonly IDisposable disposable;

        readonly Reactive<UserInfo?> userInfo = new();
        readonly Reactive<IRequireTokenAuthMainView> mainViewModel = new();
        readonly Reactive<string> loginErrorMessage = new();

        public IReadOnlyReactive<UserInfo?> UserInfo => userInfo;
        public IReadOnlyReactive<IRequireTokenAuthMainView> MainViewModel => mainViewModel;
        public IReadOnlyReactive<string> LoginErrorMessage => loginErrorMessage;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;

        readonly CancellationTokenSource cancellationTokenSource = new();

        public TokenAuthFrameViewModel(IRequireTokenAuthMainView requireTokenAuthMainView)
        {
            this.requireTokenAuthMainView = requireTokenAuthMainView;

            disposable = ReactiveBinder.Bind(TokenAuthRepository.UserInfo, OnUserInfoChanged);

            var savedAccessToken = TokenAuthRepository.SavedAccessToken.Val;
            LoginAsync(savedAccessToken).Forget();
        }

        public void Login(AuthenticationInfo authInfo)
        {
            LoginAsync(authInfo).Forget();
        }

        async Task LoginAsync(AuthenticationInfo authInfo)
        {
            if (!authInfo.IsValid)
            {
                return;
            }

            try
            {
                await TokenAuthRepository.LoginAsync(authInfo, cancellationTokenSource.Token);
                loginErrorMessage.Val = null;
            }
            catch (TokenAuthFailedException)
            {
                loginErrorMessage.Val = TranslationTable.cck_auth_failed;
                throw;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                loginErrorMessage.Val = $"{TranslationTable.cck_auth_failed}\n{e.Message}";
                throw;
            }
        }

        public void Logout()
        {
            TokenAuthRepository.Logout();
        }

        void OnUserInfoChanged(UserInfo? userInfo)
        {
            requireTokenAuthMainView.Logout();

            this.userInfo.Val = userInfo;
            mainViewModel.Val = userInfo.HasValue ? requireTokenAuthMainView : null;
        }

        public void Dispose()
        {
            disposable?.Dispose();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
