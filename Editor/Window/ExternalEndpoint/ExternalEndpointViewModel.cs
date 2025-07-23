using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Exceptions;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    public sealed class ExternalEndpointViewModel : IRequireTokenAuthMainView, IDisposable
    {
        UserInfo userInfo;

        readonly Reactive<string> newEndpointUrl = new();
        public IReadOnlyReactive<string> NewEndpointUrl => newEndpointUrl;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;
        static ExternalCallEndpointRepository EndpointRepository => ExternalCallEndpointRepository.Instance;
        static ExternalCallVerifyTokenRepository VerifyTokenRepository => ExternalCallVerifyTokenRepository.Instance;

        public IReadOnlyReactive<ExternalCallEndpoint[]> EndpointList => EndpointRepository.EndpointList;
        public IReadOnlyReactive<ExternalCallVerifyToken[]> VerifyTokenList => VerifyTokenRepository.VerifyTokenList;

        readonly CancellationTokenSource cancellationTokenSource = new();

        public (VisualElement, IDisposable) LoginAndCreateView()
        {
            userInfo = TokenAuthRepository.GetLoggedIn();

            var view = new ExternalEndpointView();

            InitializeAsync(cancellationTokenSource.Token).Forget();

            return (view, view.Bind(this));
        }

        public void OnCreateNewVerifyTokenButtonClicked()
        {
            CreateNewVerifyTokenAsync(cancellationTokenSource.Token).Forget();
        }

        public void OnCreateNewEndpointButtonClicked()
        {
            CreateNewEndpointAsync(cancellationTokenSource.Token).Forget();
        }

        public void OnDeleteEndpointButtonClicked(string endpointId)
        {
            DeleteEndpointAsync(endpointId, cancellationTokenSource.Token).Forget();
        }

        public void OnDeleteVerifyTokenButtonClicked(string tokenId)
        {
            DeleteVerifyTokenAsync(tokenId, cancellationTokenSource.Token).Forget();
        }

        public void OnNewEndpointUrlFieldChanged(string url)
        {
            newEndpointUrl.Val = url;
        }

        async Task InitializeAsync(CancellationToken cancellationToken)
        {
            await EndpointRepository.LoadEndpointListAsync(userInfo.VerifiedToken, cancellationToken);
            await VerifyTokenRepository.LoadVerifyTokenListAsync(userInfo.VerifiedToken, cancellationToken);
        }

        async Task CreateNewEndpointAsync(CancellationToken cancellationToken)
        {
            if (EditorUtility.DisplayDialog(TranslationTable.cck_confirm,
                    TranslationTable.cck_external_call_new_endpoint_confirm_message,
                    TranslationTable.cck_ok, TranslationTable.cck_cancel))
            {
                try
                {
                    await EndpointRepository.RegisterEndpointAsync(userInfo.VerifiedToken, newEndpointUrl.Val,
                        cancellationToken);
                    newEndpointUrl.Val = "";
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

        public void Logout()
        {
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
