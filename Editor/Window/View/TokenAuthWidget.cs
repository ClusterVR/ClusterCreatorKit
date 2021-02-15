using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Custom;
using ClusterVR.CreatorKit.Editor.Builder;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public class TokenAuthWidget
    {
        public readonly Reactive<UserInfo?> reactiveUserInfo = new Reactive<UserInfo?>();

        bool isLoggingIn;

        public VisualElement CreateView()
        {
            var container = new VisualElement();
            container.Add(
                new Button(() => Application.OpenURL(Api.RPC.Constants.WebBaseUrl + "/account/tokens"))
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

            var savedAccessToken = EditorPrefsUtils.SavedAccessToken;
            accessToken.value = EditorPrefsUtils.SavedAccessToken.RawValue;

            _ = Login(savedAccessToken, loginErrorLabel);
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

            Api.RPC.Constants.OverrideHost(authInfo.Host);

            isLoggingIn = true;
            try
            {
                var user = await APIServiceClient.GetMyUser(authInfo.Token);

                if (string.IsNullOrEmpty(user.Username))
                {
                    errorLabel.text = "認証に失敗しました";
                    errorLabel.SetVisibility(true);
                    return;
                }
                reactiveUserInfo.Val = new UserInfo(user.Username, authInfo.Token);
                errorLabel.SetVisibility(false);

                EditorPrefsUtils.SavedAccessToken = authInfo;
            }
            finally
            {
                errorLabel.text = "認証に失敗しました";
                errorLabel.SetVisibility(true);
                isLoggingIn = false;
            }
        }
    }
}
