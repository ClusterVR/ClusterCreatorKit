using System;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    sealed class ExternalCallEndpointListItemView : VisualElement
    {
        const string UxmlPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uxml/ExternalCallEndpointListItemView.uxml";

        readonly TextField endpointIdField;
        readonly TextField urlField;

        readonly Button deleteButton;

        public event Action OnDeleteButtonClicked;

        public ExternalCallEndpointListItemView()
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            VisualElement view = template.CloneTree();

            endpointIdField = view.Q<TextField>("endpoint-id-field");
            urlField = view.Q<TextField>("url-field");
            deleteButton = view.Q<Button>("delete-button");

            endpointIdField.label = TranslationTable.cck_external_call_endpoint_id;
            urlField.label = TranslationTable.cck_url;
            deleteButton.text = TranslationTable.cck_delete;

            deleteButton.clicked += () => OnDeleteButtonClicked?.Invoke();
            Add(view);
        }

        public void SetEndpoint(ExternalCallEndpoint endpoint)
        {
            endpointIdField.value = endpoint.EndpointId;
            urlField.value = endpoint.Url;
        }
    }
}
