using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    public sealed class ExternalEndpointView : VisualElement
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uxml/ExternalEndpointView.uxml";
        const string StyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uss/ExternalEndpointView.uss";

        readonly List<ExternalCallEndpoint> endpoints = new();
        readonly List<ExternalCallVerifyToken> verifyTokens = new();

        readonly VisualElement endpointsPanel;
        readonly Label endpointsLabel;
        readonly ListView endpointsList;
        readonly TextField newEndpointUrlField;
        readonly Button createNewEndpointButton;
        readonly HelpBox createNewEndpointNotice;

        readonly VisualElement verifyTokensPanel;
        readonly Label verifyTokensLabel;
        readonly ListView verifyTokensList;
        readonly Button createNewVerifyTokenButton;
        readonly HelpBox createNewVerifyTokenNotice;

        readonly Dictionary<object, IDisposable> listItemDisposables = new();

        event Action<string> OnNewEndpointUrlFieldChanged;
        event Action<string> OnDeleteEndpointButtonClicked;
        event Action<string> OnDeleteVerifyTokenButtonClicked;

        public ExternalEndpointView()
        {
            var mainView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath).CloneTree();
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
            mainView.styleSheets.Add(styleSheet);
            hierarchy.Add(mainView);

            mainView.Q<Label>("new-endpoint-title-label").text = TranslationTable.cck_external_call_new_endpoint_title_label;

            endpointsPanel = mainView.Q<VisualElement>("endpoints-panel");
            endpointsLabel = endpointsPanel.Q<Label>("endpoints-title-label");
            endpointsList = endpointsPanel.Q<ListView>("endpoints-list");

            var newEndpointItem = endpointsPanel.Q<VisualElement>("new-endpoint-item");
            newEndpointUrlField = newEndpointItem.Q<TextField>("url-field");
            createNewEndpointButton = newEndpointItem.Q<Button>("create-button");
            createNewEndpointNotice = newEndpointItem.Q<HelpBox>("create-notice");

            verifyTokensPanel = mainView.Q<VisualElement>("verify-tokens-panel");
            verifyTokensLabel = verifyTokensPanel.Q<Label>("verify-tokens-title-label");
            verifyTokensList = verifyTokensPanel.Q<ListView>("verify-tokens-list");

            var newVerifyTokenItem = verifyTokensPanel.Q<VisualElement>("new-verify-token-item");
            createNewVerifyTokenButton = newVerifyTokenItem.Q<Button>("create-button");
            createNewVerifyTokenNotice = newVerifyTokenItem.Q<HelpBox>("create-notice");

            newEndpointUrlField.label = TranslationTable.cck_url;
            createNewEndpointButton.text = TranslationTable.cck_register;
            createNewVerifyTokenButton.text = TranslationTable.cck_external_call_new_verify_token_button;

            endpointsList.itemsSource = endpoints;
            endpointsList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            endpointsList.makeItem = () => new ExternalCallEndpointListItemView();
            endpointsList.bindItem = (element, i) =>
            {
                var listItemView = (ExternalCallEndpointListItemView) element;
                var endpoint = endpoints[i];
                listItemView.SetEndpoint(endpoint);

                if (listItemDisposables.Remove(element, out var disposable))
                {
                    disposable.Dispose();
                }
                Action onDeleteButtonClicked = () => OnDeleteEndpointButtonClicked?.Invoke(endpoint.EndpointId);
                listItemView.OnDeleteButtonClicked += onDeleteButtonClicked;
                listItemDisposables.Add(listItemView, Disposable.Create(() =>
                {
                    listItemView.OnDeleteButtonClicked -= onDeleteButtonClicked;
                }));
            };
            endpointsList.unbindItem = (element, i) =>
            {
                if (listItemDisposables.Remove(element, out var disposable))
                {
                    disposable.Dispose();
                }
            };

            verifyTokensList.itemsSource = verifyTokens;
            verifyTokensList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            verifyTokensList.makeItem = () => new ExternalCallVerifyTokenListItemView();
            verifyTokensList.bindItem = (element, i) =>
            {
                var listItemView = (ExternalCallVerifyTokenListItemView) element;
                var verifyToken = verifyTokens[i];
                listItemView.SetVerifyToken(verifyToken);

                if (listItemDisposables.Remove(element, out var disposable))
                {
                    disposable.Dispose();
                }
                Action onDeleteButtonClicked = () => OnDeleteVerifyTokenButtonClicked?.Invoke(verifyToken.TokenId);
                listItemView.OnDeleteButtonClicked += onDeleteButtonClicked;
                listItemDisposables.Add(listItemView, Disposable.Create(() =>
                {
                    listItemView.OnDeleteButtonClicked -= onDeleteButtonClicked;
                }));
            };
            verifyTokensList.unbindItem = (element, i) =>
            {
                if (listItemDisposables.Remove(element, out var disposable))
                {
                    disposable.Dispose();
                }
            };

            newEndpointUrlField.RegisterValueChangedCallback(ev => OnNewEndpointUrlFieldChanged?.Invoke(ev.newValue));
        }

        public IDisposable Bind(ExternalEndpointViewModel viewModel)
        {
            createNewEndpointButton.clicked += viewModel.OnCreateNewEndpointButtonClicked;
            createNewVerifyTokenButton.clicked += viewModel.OnCreateNewVerifyTokenButtonClicked;
            OnDeleteEndpointButtonClicked += viewModel.OnDeleteEndpointButtonClicked;
            OnDeleteVerifyTokenButtonClicked += viewModel.OnDeleteVerifyTokenButtonClicked;
            OnNewEndpointUrlFieldChanged += viewModel.OnNewEndpointUrlFieldChanged;

            return Disposable.Create(() =>
                {
                    createNewEndpointButton.clicked -= viewModel.OnCreateNewEndpointButtonClicked;
                    createNewVerifyTokenButton.clicked -= viewModel.OnCreateNewVerifyTokenButtonClicked;
                    OnDeleteEndpointButtonClicked -= viewModel.OnDeleteEndpointButtonClicked;
                    OnDeleteVerifyTokenButtonClicked -= viewModel.OnDeleteVerifyTokenButtonClicked;
                    OnNewEndpointUrlFieldChanged -= viewModel.OnNewEndpointUrlFieldChanged;
                },
                ReactiveBinder.Bind(viewModel.EndpointList, SetEndpoints),
                ReactiveBinder.Bind(viewModel.VerifyTokenList, SetVerifyTokens),
                ReactiveBinder.Bind(viewModel.NewEndpointUrl, url => newEndpointUrlField.SetValueWithoutNotify(url)));
        }

        void SetEndpoints(ExternalCallEndpoint[] endpoints)
        {
            if (endpoints == null)
            {
                endpointsPanel.SetVisibility(false);
                return;
            }
            endpointsPanel.SetVisibility(true);

            endpointsLabel.text = TranslationUtility.GetMessage(TranslationTable.cck_external_call_endpoints_label,
                endpoints.Length, ExternalCallEndpoint.MaxEndpointCount);
            this.endpoints.Clear();
            this.endpoints.AddRange(endpoints);
            endpointsList.RefreshItems();
            var canCreateNewEndpoint = endpoints.Length < ExternalCallEndpoint.MaxEndpointCount;
            createNewEndpointButton.SetEnabled(canCreateNewEndpoint);
            createNewEndpointNotice.SetVisibility(!canCreateNewEndpoint);
            if (!canCreateNewEndpoint)
            {
                createNewEndpointNotice.messageType = HelpBoxMessageType.Info;
                createNewEndpointNotice.text = TranslationUtility.GetMessage(TranslationTable.cck_external_call_create_new_endpoint_notice,
                     ExternalCallEndpoint.MaxEndpointCount);
            }
        }

        void SetVerifyTokens(ExternalCallVerifyToken[] tokens)
        {
            if (tokens == null)
            {
                verifyTokensPanel.SetVisibility(false);
                return;
            }
            verifyTokensPanel.SetVisibility(true);
            verifyTokensLabel.text = TranslationUtility.GetMessage(TranslationTable.cck_external_call_verify_tokens_label,
                tokens.Length, ExternalCallVerifyToken.MaxVerifyTokenCount);
            this.verifyTokens.Clear();
            this.verifyTokens.AddRange(tokens);
            verifyTokensList.RefreshItems();
            var canCreateNewVerifyToken = tokens.Length < ExternalCallVerifyToken.MaxVerifyTokenCount;
            createNewVerifyTokenButton.SetEnabled(canCreateNewVerifyToken);
            createNewVerifyTokenNotice.SetVisibility(!canCreateNewVerifyToken);
            if (!canCreateNewVerifyToken)
            {
                createNewVerifyTokenNotice.messageType = HelpBoxMessageType.Info;
                createNewVerifyTokenNotice.text = TranslationUtility.GetMessage(TranslationTable.cck_external_call_create_new_verify_token_notice,
                    ExternalCallVerifyToken.MaxVerifyTokenCount);
            }
        }
    }
}
