using System;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    sealed class ExternalCallVerifyTokenListItemView : VisualElement
    {
        const string UxmlPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uxml/ExternalCallVerifyTokenListItemView.uxml";
        const string DateTimeFormat = "yyyy/MM/dd HH:mm:ss";

        readonly TextField registeredAtField;
        readonly VisualElement verifyTokenContainer;
        readonly TextField verifyTokenField;

        readonly Button deleteButton;

        public event Action OnDeleteButtonClicked;

        public ExternalCallVerifyTokenListItemView()
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            VisualElement view = template.CloneTree();

            view.Q<HelpBox>("verify-token-notice").text = TranslationTable.cck_external_call_verify_token_notice;

            registeredAtField = view.Q<TextField>("registered-at-field");
            verifyTokenContainer = view.Q<VisualElement>("verify-token-container");
            verifyTokenField = view.Q<TextField>("verify-token-field");
            deleteButton = view.Q<Button>("delete-button");

            registeredAtField.label = TranslationTable.cck_registered_at;
            verifyTokenField.label = TranslationTable.cck_external_call_verify_token;
            deleteButton.text = TranslationTable.cck_delete;

            deleteButton.clicked += () => OnDeleteButtonClicked?.Invoke();

            Add(view);
        }

        public void SetVerifyToken(ExternalCallVerifyToken verifyToken)
        {
            registeredAtField.value = verifyToken.RegisteredAt.ToString(DateTimeFormat);
            verifyTokenContainer.SetVisibility(!string.IsNullOrEmpty(verifyToken.VerifyToken));
            verifyTokenField.value = verifyToken.VerifyToken;
        }
    }
}
