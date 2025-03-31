using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Exceptions;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Extensions;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    public sealed class ExternalEndpointView : IRequireTokenAuthMainView, IDisposable
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uxml/ExternalEndpointView.uxml";
        const string StyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/ExternalEndpoint/Uss/ExternalEndpointView.uss";

        UserInfo userInfo;

        VisualElement endpointsPanel;
        Label endpointsLabel;
        ListView endpointsList;
        TextField newEndpointUrlField;
        Button createNewEndpointButton;
        HelpBox createNewEndpointNotice;

        VisualElement verifyTokensPanel;
        Label verifyTokensLabel;
        ListView verifyTokensList;
        Button createNewVerifyTokenButton;
        HelpBox createNewVerifyTokenNotice;

        readonly List<ExternalCallEndpoint> endpoints = new();
        readonly List<ExternalCallVerifyToken> verifyTokens = new();

        static ExternalCallEndpointRepository EndpointRepository => ExternalCallEndpointRepository.Instance;
        static ExternalCallVerifyTokenRepository VerifyTokenRepository => ExternalCallVerifyTokenRepository.Instance;

        readonly CancellationTokenSource cancellationTokenSource = new();
        readonly List<IDisposable> disposables = new();
        readonly Dictionary<object, IDisposable> listItemDisposables = new();

        public VisualElement LoginAndCreateView(UserInfo userInfo)
        {
            this.userInfo = userInfo;

            var mainView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath).CloneTree();
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
            mainView.styleSheets.Add(styleSheet);

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
                Action onDeleteButtonClicked = () => OnDeleteEndpointButtonClicked(endpoint.EndpointId);
                listItemView.OnDeleteButtonClicked += onDeleteButtonClicked;
                listItemDisposables.Add(listItemView, new Disposable(() =>
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
                Action onDeleteButtonClicked = () => OnDeleteVerifyTokenButtonClicked(verifyToken.TokenId);
                listItemView.OnDeleteButtonClicked += onDeleteButtonClicked;
                listItemDisposables.Add(listItemView, new Disposable(() =>
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

            createNewEndpointButton.clicked += OnCreateNewEndpointButtonClicked;
            createNewVerifyTokenButton.clicked += OnCreateNewVerifyTokenButtonClicked;

            disposables.Add(ReactiveBinder.Bind(EndpointRepository.EndpointList, SetEndpoints));
            disposables.Add(ReactiveBinder.Bind(VerifyTokenRepository.VerifyTokenList, SetVerifyTokens));

            InitializeAsync(cancellationTokenSource.Token).Forget();

            return mainView;
        }

        void OnCreateNewVerifyTokenButtonClicked()
        {
            CreateNewVerifyTokenAsync(cancellationTokenSource.Token).Forget();
        }

        void OnCreateNewEndpointButtonClicked()
        {
            CreateNewEndpointAsync(cancellationTokenSource.Token).Forget();
        }

        void OnDeleteEndpointButtonClicked(string endpointId)
        {
            DeleteEndpointAsync(endpointId, cancellationTokenSource.Token).Forget();
        }

        void OnDeleteVerifyTokenButtonClicked(string tokenId)
        {
            DeleteVerifyTokenAsync(tokenId, cancellationTokenSource.Token).Forget();
        }

        async Task InitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await EndpointRepository.LoadEndpointListAsync(userInfo.VerifiedToken, cancellationToken);
                await VerifyTokenRepository.LoadVerifyTokenListAsync(userInfo.VerifiedToken, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                SetEndpoints(null);
                SetVerifyTokens(null);
                throw;
            }
        }

        async Task CreateNewEndpointAsync(CancellationToken cancellationToken)
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm,
                    TranslationTable.cck_external_call_new_endpoint_confirm_message,
                    TranslationTable.cck_ok, TranslationTable.cck_cancel))
            {
                try
                {
                    await EndpointRepository.RegisterEndpointAsync(userInfo.VerifiedToken, newEndpointUrlField.value,
                        cancellationToken);
                    newEndpointUrlField.value = "";
                    const string message = TranslationTable.cck_external_call_new_endpoint_success_message;
                    EditorUtility.DisplayDialog(message, message, TranslationTable.cck_ok);
                }
                catch (ExternalCallInvalidUrlException)
                {
                    const string message = TranslationTable.cck_external_call_invalid_url_exception_message;
                    EditorUtility.DisplayDialog(TranslationTable.cck_error, message, TranslationTable.cck_ok);
                    Debug.LogError(message);
                }
                catch (ExternalCallUrlAlreadyExistsException)
                {
                    const string message = TranslationTable.cck_external_call_url_already_exists_exception_message;
                    EditorUtility.DisplayDialog(TranslationTable.cck_error, message, TranslationTable.cck_ok);
                    Debug.LogError(message);
                }
                catch (ExternalCallEndpointCountLimitExceededException)
                {
                    const string message = TranslationTable.cck_external_call_endpoint_count_limit_exceeded_exception_message;
                    EditorUtility.DisplayDialog(TranslationTable.cck_error, message, TranslationTable.cck_ok);
                    Debug.LogError(message);
                }
            }
        }

        async Task CreateNewVerifyTokenAsync(CancellationToken cancellationToken)
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm,
                    TranslationTable.cck_external_call_new_verify_token_confirm_message,
                    TranslationTable.cck_ok, TranslationTable.cck_cancel))
            {
                try
                {
                    await VerifyTokenRepository.RegisterVerifyTokenAsync(userInfo.VerifiedToken, cancellationToken);
                    const string message = TranslationTable.cck_external_call_new_verify_token_success_message;
                    EditorUtility.DisplayDialog(message, message, TranslationTable.cck_ok);
                }
                catch (ExternalCallVerifyTokenCountLimitExceededException)
                {
                    const string message = TranslationTable.cck_external_call_verify_token_count_limit_exceeded_exception_message;
                    EditorUtility.DisplayDialog(TranslationTable.cck_error, message, TranslationTable.cck_ok);
                    Debug.LogError(message);
                }
            }
        }

        async Task DeleteEndpointAsync(string endpointId, CancellationToken cancellationToken)
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm,
                    TranslationTable.cck_external_call_delete_endpoint_confirm_message,
                    TranslationTable.cck_delete_action, TranslationTable.cck_cancel))
            {
                await EndpointRepository.DeleteEndpointAsync(userInfo.VerifiedToken, endpointId, cancellationToken);
                const string message = TranslationTable.cck_external_call_delete_endpoint_success_message;
                EditorUtility.DisplayDialog(message, message, TranslationTable.cck_ok);
            }
        }

        async Task DeleteVerifyTokenAsync(string tokenId, CancellationToken cancellationToken)
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm,
                    TranslationTable.cck_external_call_delete_verify_token_confirm_message,
                    TranslationTable.cck_delete_action, TranslationTable.cck_cancel))
            {
                await VerifyTokenRepository.DeleteVerifyTokenAsync(userInfo.VerifiedToken, tokenId, cancellationToken);
                const string message = TranslationTable.cck_external_call_delete_verify_token_success_message;
                EditorUtility.DisplayDialog(message, message, TranslationTable.cck_ok);
            }
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

        public void Logout()
        {
            SetEndpoints(null);
            SetVerifyTokens(null);
        }

        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
            foreach (var disposable in listItemDisposables.Values)
            {
                disposable.Dispose();
            }
            listItemDisposables.Clear();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
