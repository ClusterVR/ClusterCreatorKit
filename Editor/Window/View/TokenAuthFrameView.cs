using System;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class TokenAuthFrameView : VisualElement
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/RequireTokenAuthView.uxml";
        const string MainStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthView.uss";
        const string MainDarkStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthViewDarkStyle.uss";
        const string MainLightStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/RequireTokenAuthViewLightStyle.uss";

        const string TokenAuthTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/TokenAuthView.uxml";
        const string TokenAuthStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/TokenAuthView.uss";

        readonly VisualElement mainViewContainer;
        readonly VisualElement tokenAuthView;
        readonly VisualElement mainViewHeader;
        readonly Label userNameLabel;
        Label loginErrorLabel;
        Label validationErrorLabel;

        event Action<AuthenticationInfo> OnLoginButtonClicked;
        event Action OnLogoutButtonClicked;

        VisualElement mainView;
        IDisposable mainViewDisposable;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;

        public TokenAuthFrameView()
        {
            var themeStyleSheetPath = EditorGUIUtility.isProSkin ? MainDarkStylePath : MainLightStylePath;

            var mainTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath);
            VisualElement view = mainTemplate.CloneTree();
            hierarchy.Add(view);

            var mainStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStylePath);
            var themeStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(themeStyleSheetPath);
            view.styleSheets.Add(mainStyle);
            view.styleSheets.Add(themeStyle);

            tokenAuthView = CreateTokenAuthView();

            mainViewContainer = view.Q<VisualElement>("main-view-container");
            mainViewContainer.Add(tokenAuthView);

            mainViewHeader = view.Q<VisualElement>("main-view-header");
            userNameLabel = mainViewHeader.Q<Label>("user-name-label");
            var logoutButton = mainViewHeader.Q<Button>("logout-button");
            logoutButton.text = TranslationTable.cck_switch_account;
            logoutButton.clicked += () => OnLogoutButtonClicked?.Invoke();
            mainViewHeader.SetVisibility(false);

            var tokenInputField = tokenAuthView.Q<TextField>("token-input-field");
            tokenInputField.value = TokenAuthRepository.SavedAccessToken.Val.RawValue;
        }

        VisualElement CreateTokenAuthView()
        {
            var tokenAuthTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(TokenAuthTemplatePath);
            var tokenAuthStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(TokenAuthStylePath);
            VisualElement view = tokenAuthTemplate.CloneTree();
            view.styleSheets.Add(tokenAuthStyle);

            var tokenInputField = view.Q<TextField>("token-input-field");
            validationErrorLabel = view.Q<Label>("validation-error-label");
            loginErrorLabel = view.Q<Label>("login-error-label");
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
            useTokenButton.clicked += () => OnLoginButtonClicked?.Invoke(new AuthenticationInfo(tokenInputField.text));

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

        public IDisposable Bind(TokenAuthFrameViewModel viewModel)
        {
            OnLoginButtonClicked += viewModel.Login;
            OnLogoutButtonClicked += viewModel.Logout;

            return Disposable.Create(() =>
                {
                    OnLoginButtonClicked -= viewModel.Login;
                    OnLogoutButtonClicked -= viewModel.Logout;
                },
                ReactiveBinder.Bind(viewModel.LoginErrorMessage, SetLoginErrorMessage),
                ReactiveBinder.Bind(viewModel.UserInfo, SetUserInfo),
                ReactiveBinder.Bind(viewModel.MainViewModel, SetMainView));
        }

        void SetLoginErrorMessage(string message)
        {
            if (message != null)
            {
                loginErrorLabel.text = message;
                loginErrorLabel.SetVisibility(true);
            }
            else
            {
                loginErrorLabel.SetVisibility(false);
            }
        }

        void SetUserInfo(UserInfo? userInfo)
        {
            userNameLabel.text = userInfo.HasValue ? userInfo.Value.Username : string.Empty;
        }

        void SetMainView(IRequireTokenAuthMainView mainViewModel)
        {
            if (mainViewContainer == null || tokenAuthView == null || mainViewHeader == null)
            {
                return;
            }
            if (mainView != null)
            {
                mainViewDisposable?.Dispose();
                mainViewContainer.Remove(mainView);
                mainView = null;
            }

            if (mainViewModel != null)
            {
                (mainView, mainViewDisposable) = mainViewModel.LoginAndCreateView();
                mainViewContainer.Remove(tokenAuthView);
                tokenAuthView.SetVisibility(false);
                mainViewHeader.SetVisibility(true);

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
    }
}
