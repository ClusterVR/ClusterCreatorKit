using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public sealed class PreviewControlWindow : EditorWindow
    {
        const string messageWhenNotPlayMode = TranslationTable.cck_preview_option_runtime_only;

        [MenuItem("Cluster/Preview/ControlWindow", priority = 113)]
        public static void ShowWindow()
        {
            var window = GetWindow<PreviewControlWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_preview_control_window);
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
            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_comment));
            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_display_name));
            var displayNameField = new TextField();
            commentSection.Add(displayNameField);

            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_username));
            var userNameField = new TextField();
            commentSection.Add(userNameField);

            commentSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_comment_content));

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
            });
            commentSendButton.text = TranslationTable.cck_send_comment;
            commentSection.Add(commentSendButton);
            return commentSection;
        }

        static VisualElement GenerateMainScreenSection()
        {
            var mainScreenSection = EditorUIGenerator.GenerateSection();
            mainScreenSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_main_screen));
            var sampleImageSendButton = new Button(ShowMainScreenPicture);
            sampleImageSendButton.text = TranslationTable.cck_project_sample_image;
            mainScreenSection.Add(sampleImageSendButton);
            return mainScreenSection;
        }

        static VisualElement GenerateUserDataSection()
        {
            var userDataSection = EditorUIGenerator.GenerateSection();
            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_player_info));
            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_authority));
            var currentPermission =
                EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_current_authority_participant);
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
                    currentPermission.text = TranslationTable.cck_current_authority_performer;
                }
                else
                {
                    Bootstrap.PlayerPresenter.ChangePermissionType(PermissionType.Audience);
                    currentPermission.text = TranslationTable.cck_current_authority_participant;
                }
            });
            permissionChangeButton.text = TranslationTable.cck_change_authority;
            userDataSection.Add(currentPermission);
            userDataSection.Add(permissionChangeButton);

            userDataSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h2, TranslationTable.cck_respawn));
            var respawnButton = new Button(() => { Bootstrap.PlayerPresenter.Respawn(); });
            respawnButton.text = TranslationTable.cck_respawn_action;
            userDataSection.Add(respawnButton);
            return userDataSection;
        }
    }
}
