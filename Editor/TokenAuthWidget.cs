using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Core;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor
{
    public class TokenAuthWidget
    {
        public readonly Reactive<UserInfo?> reactiveUserInfo = new Reactive<UserInfo?>();
        readonly Reactive<bool> reactiveIsValidToken = new Reactive<bool>();
        readonly Reactive<string> reactiveErrorMessage = new Reactive<string>();

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
            container.Add(new Label(){text="アクセストークンを貼り付けてください"});

            var accessToken = new TextField();
            accessToken.RegisterValueChangedCallback(ev =>
            {
                Validate(ev.newValue);
            });
            container.Add(accessToken);

            var messageLabel = new Label();
            container.Add(messageLabel);
            ReactiveBinder.Bind(reactiveErrorMessage, msg => { messageLabel.text = msg; });

            var useTokenButton = new Button(() => ValidateAndLogin(accessToken.value))
            {
                text = "このトークンを使用",
            };
            ReactiveBinder.Bind(reactiveIsValidToken, useTokenButton.SetEnabled);
            container.Add(useTokenButton);

            // TODO: 他のwindowでloginしたときにも自動で同期する
            if (!string.IsNullOrEmpty(EditorPrefsUtils.SavedAccessToken))
            {
                accessToken.value = EditorPrefsUtils.SavedAccessToken;
            }

            // 初期状態 or 既存のトークンをvalidateして何かのメッセージを出すのに必要
            ValidateAndLogin(EditorPrefsUtils.SavedAccessToken);
            return container;
        }

        public void Logout()
        {
            reactiveUserInfo.Val = null;
            EditorPrefsUtils.SavedAccessToken = null;
        }

        async Task ValidateAndLogin(string token)
        {
            Validate(token);
            if (!reactiveIsValidToken.Val)
            {
                return;
            }

            // Call auth API
            if (isLoggingIn)
            {
                return;
            }
            try
            {
                isLoggingIn = true;
                var user = await APIServiceClient.GetMyUser.Call(Empty.Value, token);

                if (string.IsNullOrEmpty(user.Username))
                {
                    reactiveErrorMessage.Val = "認証に失敗しました";
                    return;
                }
                reactiveUserInfo.Val = new UserInfo(user.Username, token);
                reactiveErrorMessage.Val = "";

                EditorPrefsUtils.SavedAccessToken = token;
            }
            finally
            {
                isLoggingIn = false;
            }
        }

        void Validate(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                reactiveUserInfo.Val = null;
                reactiveIsValidToken.Val = false;
                reactiveErrorMessage.Val = "";
                return;
            }

            if (token.Length != 64)
            {
                reactiveUserInfo.Val = null;
                reactiveIsValidToken.Val = false;
                reactiveErrorMessage.Val = "不正なアクセストークンです";
                return;
            }

            reactiveIsValidToken.Val = true;
        }
    }
}
