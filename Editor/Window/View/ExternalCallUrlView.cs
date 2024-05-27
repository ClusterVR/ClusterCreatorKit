using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ExternalCall;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class ExternalCallUrlView : IRequireTokenAuthMainView, IDisposable
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/ExternalCallUrlView.uxml";

        UserInfo userInfo;
        Label currentUrlLabel;
        TextField updateUrlTextField;
        Button updateButton;
        Button deleteButton;
        VisualElement tokenView;
        TextField tokenField;
        readonly CancellationTokenSource cancellationTokenSource = new();

        public VisualElement LoginAndCreateView(UserInfo userInfo)
        {
            this.userInfo = userInfo;

            var mainView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath).CloneTree();
            currentUrlLabel = mainView.Q<Label>("current-url-label");
            updateUrlTextField = mainView.Q<TextField>("update-url-field");

            var titleLabel = mainView.Q<Label>("current-url-title-label");
            var urlLabel = mainView.Q<Label>("update-url-title-label");
            titleLabel.text = TranslationTable.cck_registered_url;
            urlLabel.text = TranslationTable.cck_url_registration;

            var tokenViewLabel = mainView.Q<Label>("verify-token");
            var tokenNoticeLabel = mainView.Q<Label>("token-notice");
            tokenViewLabel.text = TranslationTable.cck_token_for_verify;
            tokenNoticeLabel.text = TranslationTable.cck_verify_token_notice;

            updateButton = mainView.Q<Button>("update-button");
            deleteButton = mainView.Q<Button>("delete-button");
            updateButton.text = TranslationTable.cck_register;
            deleteButton.text = TranslationTable.cck_delete;

            tokenView = mainView.Q<VisualElement>("token-view");
            tokenField = mainView.Q<TextField>("token-field");

            updateButton.clicked += OnUpdateClicked;
            deleteButton.clicked += OnDeleteClicked;

            _ = InitializeAsync(cancellationTokenSource.Token);
            return mainView;
        }

        void OnUpdateClicked()
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm, TranslationTable.cck_update_url_confirm, TranslationTable.cck_ok, TranslationTable.cck_cancel))
            {
                _ = UpdateWebRPCUrlAsync(cancellationTokenSource.Token);
            }
        }

        void OnDeleteClicked()
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm, TranslationTable.cck_delete_url_confirm, TranslationTable.cck_ok, TranslationTable.cck_cancel))
            {
                _ = DeleteWebRPCUrlAsync(cancellationTokenSource.Token);
            }
        }

        async Task InitializeAsync(CancellationToken cancellationToken)
        {
            tokenView.visible = false;
            var currentUrl = await GetWebRPCUrlAsync(cancellationToken);
            SetCurrentURL(currentUrl);
        }

        void SetCurrentURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                currentUrlLabel.text = TranslationTable.cck_unregistered;
                deleteButton.SetEnabled(false);
            }
            else
            {
                currentUrlLabel.text = url;
                deleteButton.SetEnabled(true);
            }
        }

        async Task<string> GetWebRPCUrlAsync(CancellationToken cancellationToken)
        {
            try
            {
                var res = await APIServiceClient.GetWebRPCURLAsync(userInfo.VerifiedToken, cancellationToken);
                return res.Url;
            }
            catch (Failure e) when (e.StatusCode == 404)
            {
                return null;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                Debug.LogException(e);
                throw;
            }
        }

        async Task UpdateWebRPCUrlAsync(CancellationToken cancellationToken)
        {
            var url = updateUrlTextField.value;
            if (string.IsNullOrEmpty(url)) return;

            try
            {
                var res = await APIServiceClient.RegisterWebRPCURLAsync(new RegisterWebRPCURLPayload(url), userInfo.VerifiedToken, cancellationToken);
                SetCurrentURL(res.Url);
                tokenView.visible = true;
                tokenField.value = res.VerifyToken;
            }
            catch (Failure e) when (e.StatusCode == 400)
            {
                EditorUtility.DisplayDialog(TranslationTable.cck_error, e.Error.Detail, TranslationTable.cck_ok);
                throw;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                Debug.LogException(e);
                throw;
            }
        }

        async Task DeleteWebRPCUrlAsync(CancellationToken cancellationToken)
        {
            try
            {
                await APIServiceClient.DeleteUserWebRPCURLAsync(userInfo.VerifiedToken, cancellationToken);
                SetCurrentURL(null);
                tokenView.visible = false;
                tokenField.value = null;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public void Logout()
        {
            SetCurrentURL(null);
            tokenField.visible = false;
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
