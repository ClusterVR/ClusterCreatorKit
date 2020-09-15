using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Core;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using ClusterVR.CreatorKit.Editor.Custom;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor
{
    public class TokenAuthWidget
    {
        public readonly Reactive<UserInfo?> reactiveUserInfo = new Reactive<UserInfo?>();

        bool isLoggingIn;

        public VisualElement CreateView()
        {
            var container = new VisualElement();
            container.Add(
                new Button(() => Application.OpenURL(ClusterVR.CreatorKit.Editor.Core.Constants.WebBaseUrl + "/account/tokens"))
                {
                    text = "Webでトークンを発行"
                });

            container.Add(UiUtils.Separator());
            container.Add(new Label {text="アクセストークンを貼り付けてください"});

            var accessToken = new TextField();
            container.Add(accessToken);

            var validationErrorLabel = new Label();
            validationErrorLabel.SetVisibility(false);
            container.Add(validationErrorLabel);

            var loginErrorLabel = new Label();
            loginErrorLabel.SetVisibility(false);
            container.Add(loginErrorLabel);

            var useTokenButton = new Button(() => _ = Login(new AuthenticationInfo(accessToken.value), loginErrorLabel))
            {
                text = "このトークンを使用",
            };
            container.Add(useTokenButton);

            accessToken.RegisterValueChangedCallback(ev =>
            {
                var authInfo = new AuthenticationInfo(ev.newValue);
                validationErrorLabel.SetVisibility(!authInfo.IsValid);
                validationErrorLabel.text = authInfo.ValidationError;
                useTokenButton.SetEnabled(authInfo.IsValid);
                loginErrorLabel.SetVisibility(false);
            });

            // TODO: 他のwindowでloginしたときにも自動で同期する
            if (!string.IsNullOrEmpty(EditorPrefsUtils.SavedAccessToken))
            {
                accessToken.value = EditorPrefsUtils.SavedAccessToken;
            }

            // 初期状態 or 既存のトークンをvalidateして何かのメッセージを出すのに必要
            _ = Login(new AuthenticationInfo(EditorPrefsUtils.SavedAccessToken), loginErrorLabel);
            return container;
        }

        public void Logout()
        {
            reactiveUserInfo.Val = null;
            EditorPrefsUtils.SavedAccessToken = null;
        }

        async Task Login(AuthenticationInfo authInfo, TextElement errorLabel)
        {
            if (!authInfo.IsValid || isLoggingIn)
            {
                return;
            }

            Core.Constants.OverrideHost(authInfo.Host);

            isLoggingIn = true;
            try
            {
                var user = await APIServiceClient.GetMyUser.Call(Empty.Value, authInfo.Token);

                if (string.IsNullOrEmpty(user.Username))
                {
                    errorLabel.text = "認証に失敗しました";
                    errorLabel.SetVisibility(true);
                    return;
                }
                reactiveUserInfo.Val = new UserInfo(user.Username, authInfo.Token);
                errorLabel.SetVisibility(false);

                EditorPrefsUtils.SavedAccessToken = authInfo.RawValue;
            }
            finally
            {
                errorLabel.text = "認証に失敗しました";
                errorLabel.SetVisibility(true);
                isLoggingIn = false;
            }
        }

        sealed class AuthenticationInfo
        {
            public string RawValue { get; }
            public string Host { get; }
            public string Token { get; }
            public bool IsValid { get; }
            public string ValidationError { get; }

            public AuthenticationInfo(string raw)
            {
                RawValue = raw;
                var split = raw.Split(':');
                if (split.Length > 1)
                {
                    Host = split[0];
                    Token = split[1];
                }
                else
                {
                    Token = raw;
                }

                var validationError = "";
                IsValid = IsValidToken(Token, ref validationError);
                ValidationError = validationError;
            }

            static bool IsValidToken(string token, ref string errorMessage)
            {
                if (string.IsNullOrEmpty(token)) return false;
                if (token.Length != 64)
                {
                    errorMessage = "不正なアクセストークンです";
                    return false;
                }

                return true;
            }
        }
    }
}
