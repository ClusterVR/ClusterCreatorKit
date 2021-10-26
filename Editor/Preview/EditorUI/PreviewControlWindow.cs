using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Editor.Window.View;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public sealed class PreviewControlWindow : EditorWindow
    {
        const string messageWhenNotPlayMode = "プレビューオプションは実行中のみ使用可能です";

        [MenuItem("Cluster/Preview/ControlWindow", priority = 113)]
        public static void ShowWindow()
        {
            var window = GetWindow<PreviewControlWindow>();
            window.titleContent = new GUIContent("Preview Control Window");
        }

        public void OnEnable()
        {
            var root = rootVisualElement;
            root.Add(GenerateCommentSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateMainScreenSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateUserDataSection());
            root.Add(UiUtils.Separator());
        }

        static void SendComment(string displayName, string userName, string content)
        {
            if (!Bootstrap.IsInPlayMode)
            {
                Debug.LogWarning(messageWhenNotPlayMode);
                return;
            }

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "DisplayName";
            }
            if (string.IsNullOrEmpty(userName))
            {
                userName = "UserName";
            }
            Bootstrap.CommentScreenPresenter.SendCommentFromEditorUI(displayName, userName, content);
        }

        static void ShowMainScreenPicture()
        {
            if (!Bootstrap.IsInPlayMode)
            {
                Debug.LogWarning(messageWhenNotPlayMode);
                return;
            }

            Bootstrap.MainScreenPresenter.SetImage(AssetDatabase.LoadAssetAtPath<Texture>(
                "Packages/mu.cluster.cluster-creator-kit/Editor/Preview/Textures/cluster_logo.png"));
        }

        static VisualElement GenerateCommentSection()
        {
            var commentSection = EditorUIGenerator.GenerateSection();
            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, "コメント"));
            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, "表示名"));
            var displayNameField = new TextField();
            commentSection.Add(displayNameField);

            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, "ユーザー名"));
            var userNameField = new TextField();
            commentSection.Add(userNameField);

            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, "コメント内容"));

            var commentContentField = new TextField();
            commentContentField.style.unityTextAlign = TextAnchor.UpperLeft;
            commentContentField.multiline = true;
            commentContentField.style.height = 50;
            foreach (var child in commentContentField.Children())
            {
                child.style.unityTextAlign = TextAnchor.UpperLeft;
            }

            commentSection.Add(commentContentField);

            var commentSendButton = new Button(() =>
            {
                SendComment(displayNameField.value, userNameField.value, commentContentField.value);
                displayNameField.value = "";
                userNameField.value = "";
                commentContentField.value = "";
            }) { text = "コメントを送信" };
            commentSection.Add(commentSendButton);
            return commentSection;
        }

        static VisualElement GenerateMainScreenSection()
        {
            var mainScreenSection = EditorUIGenerator.GenerateSection();
            mainScreenSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, "メインスクリーン"));
            var sampleImageSendButton = new Button(ShowMainScreenPicture) { text = "サンプル画像を投影" };
            mainScreenSection.Add(sampleImageSendButton);
            return mainScreenSection;
        }

        static VisualElement GenerateUserDataSection()
        {
            var userDataSection = EditorUIGenerator.GenerateSection();
            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, "プレイヤー情報"));
            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, "権限"));
            var currentPermission = EditorUIGenerator.GenerateLabel(LabelType.h2, "現在の権限:参加者");
            var permissionChangeButton = new Button(() =>
            {
                if (!Bootstrap.IsInPlayMode)
                {
                    Debug.LogWarning(messageWhenNotPlayMode);
                    return;
                }

                if (Bootstrap.PlayerPresenter.PermissionType == PermissionType.Audience)
                {
                    Bootstrap.PlayerPresenter.ChangePermissionType(PermissionType.Performer);
                    currentPermission.text = "現在の権限:パフォーマー";
                }
                else
                {
                    Bootstrap.PlayerPresenter.ChangePermissionType(PermissionType.Audience);
                    currentPermission.text = "現在の権限:参加者";
                }
            }) { text = "権限変更" };
            userDataSection.Add(currentPermission);
            userDataSection.Add(permissionChangeButton);

            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, "リスポーン"));
            var respawnButton = new Button(() => { Bootstrap.PlayerPresenter.Respawn(); }) { text = "リスポーンする" };
            userDataSection.Add(respawnButton);
            return userDataSection;
        }
    }
}
