using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Extensions;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class RequireTokenAuthView : IDisposable
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/RequireTokenAuthView.uxml";
        const string MainStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthView.uss";
        const string MainDarkStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthViewDarkStyle.uss";
        const string MainLightStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthViewLightStyle.uss";

        const string TokenAuthTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/TokenAuthView.uxml";
        const string TokenAuthStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/TokenAuthView.uss";

        readonly IRequireTokenAuthMainView requireTokenAuthMainView;

        VisualElement mainViewContainer;
        VisualElement tokenAuthView;
        VisualElement mainView;
        VisualElement mainViewHeader;

        IDisposable disposable;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;

        readonly CancellationTokenSource cancellationTokenSource = new();

        public RequireTokenAuthView(IRequireTokenAuthMainView requireTokenAuthMainView)
        {
            this.requireTokenAuthMainView = requireTokenAuthMainView;
        }

        public VisualElement CreateView()
        {
            var themeStyleSheetPath = EditorGUIUtility.isProSkin ? MainDarkStylePath : MainLightStylePath;

            var mainTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath);
            VisualElement view = mainTemplate.CloneTree();
            var mainStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStylePath);
            var themeStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(themeStyleSheetPath);
            view.styleSheets.Add(mainStyle);
            view.styleSheets.Add(themeStyle);

            tokenAuthView = CreateTokenAuthView();

            mainViewContainer = view.Q<VisualElement>("main-view-container");
            mainViewContainer.Add(tokenAuthView);

            mainViewHeader = view.Q<VisualElement>("main-view-header");
            var logoutButton = mainViewHeader.Q<Button>("logout-button");
            logoutButton.text = TranslationTable.cck_switch_account;
            logoutButton.clicked += Logout;
            mainViewHeader.SetVisibility(false);

            disposable = ReactiveBinder.Bind(TokenAuthRepository.UserInfo, OnUserInfoChanged);

            var tokenInputField = tokenAuthView.Q<TextField>("token-input-field");
            var loginErrorLabel = tokenAuthView.Q<Label>("login-error-label");
            var savedAccessToken = EditorPrefsUtils.SavedAccessToken;
            tokenInputField.value = EditorPrefsUtils.SavedAccessToken.RawValue;

            LoginAsync(savedAccessToken, loginErrorLabel).Forget();

            return view;
        }

        VisualElement CreateTokenAuthView()
        {
            var tokenAuthTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(TokenAuthTemplatePath);
            var tokenAuthStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(TokenAuthStylePath);
            VisualElement view = tokenAuthTemplate.CloneTree();
            view.styleSheets.Add(tokenAuthStyle);

            var tokenInputField = view.Q<TextField>("token-input-field");
            var validationErrorLabel = view.Q<Label>("validation-error-label");
            var loginErrorLabel = view.Q<Label>("login-error-label");
            validationErrorLabel.SetVisibility(false);
            loginErrorLabel.SetVisibility(false);

            var openTokenPageButton = view.Q<Button>("open-token-page-button");
            var useTokenButton = view.Q<Button>("use-token-button");
            openTokenPageButton.text = TranslationTable.cck_web_token_issue;
            useTokenButton.text = TranslationTable.cck_use_this_token;
            openTokenPageButton.clicked += () =>
            {
                var url = Api.RPC.Constants.WebBaseUrl + "/account/tokens";
                Application.OpenURL(url);
                PanamaLogger.LogCckOpenLink(url, "RequireTokenAuthView_OpenTokenPage");
            };
            useTokenButton.clicked += () => LoginAsync(new AuthenticationInfo(tokenInputField.value), loginErrorLabel).Forget();

            var pasteAccessTokenLabel = view.Q<Label>("paste-access-token-label");
            pasteAccessTokenLabel.text = TranslationTable.cck_paste_access_token;

            tokenInputField.RegisterValueChangedCallback(ev =>
            {
                var authInfo = new AuthenticationInfo(ev.newValue);
                validationErrorLabel.SetVisibility(!authInfo.IsValid);
                validationErrorLabel.text = authInfo.ValidationError;
                useTokenButton.SetEnabled(authInfo.IsValid);
                loginErrorLabel.SetVisibility(false);
            });

            return view;
        }

        async Task LoginAsync(AuthenticationInfo authInfo, TextElement errorLabel)
        {
            if (!authInfo.IsValid)
            {
                return;
            }

            try
            {
                await TokenAuthRepository.LoginAsync(authInfo, cancellationTokenSource.Token);
                errorLabel.SetVisibility(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (TokenAuthFailedException)
            {
                errorLabel.text = TranslationTable.cck_auth_failed;
                errorLabel.SetVisibility(true);
                throw;
            }
            catch (Exception e)
            {
                errorLabel.text = $"{TranslationTable.cck_auth_failed}\n{e.Message}";
                errorLabel.SetVisibility(true);
                throw;
            }
        }

        void Logout()
        {
            TokenAuthRepository.Logout();
        }

        void OnUserInfoChanged(UserInfo? userInfo)
        {
            if (mainViewContainer == null || tokenAuthView == null || mainViewHeader == null)
            {
                return;
            }
            var userNameLabel = mainViewHeader.Q<Label>("user-name-label");
            var validationErrorLabel = tokenAuthView.Q<Label>("validation-error-label");
            var loginErrorLabel = tokenAuthView.Q<Label>("login-error-label");

            if (mainView != null)
            {
                mainViewContainer.Remove(mainView);
                requireTokenAuthMainView.Logout();
                mainView = null;
            }

            if (userInfo.HasValue)
            {
                mainViewContainer.Remove(tokenAuthView);
                tokenAuthView.SetVisibility(false);
                mainViewHeader.SetVisibility(true);
                userNameLabel.text = userInfo.Value.Username;

                mainView = requireTokenAuthMainView.LoginAndCreateView(userInfo.Value);
                mainViewContainer.Add(mainView);
            }
            else
            {
                mainViewContainer.Add(tokenAuthView);
                tokenAuthView.SetVisibility(true);
                mainViewHeader.SetVisibility(false);
                validationErrorLabel.SetVisibility(false);
                loginErrorLabel.SetVisibility(false);
                userNameLabel.text = string.Empty;
            }
        }

        public void Dispose()
        {
            disposable?.Dispose();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
