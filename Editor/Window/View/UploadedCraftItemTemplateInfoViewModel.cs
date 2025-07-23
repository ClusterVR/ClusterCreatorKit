using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using UnityEngine.UIElements;
using UserInfo = ClusterVR.CreatorKit.Editor.Api.User.UserInfo;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoViewModel : IRequireTokenAuthMainView, IDisposable
    {
        UploadedCraftItemTemplateInfoView view;
        bool isCreated;
        UserInfo userInfo;

        bool isUpdating;
        int currentPage;
        int prevPage;
        int nextPage;

        readonly Reactive<(IReadOnlyList<OwnItemTemplate> ownItemTemplates, bool fetchFailed)> ownItemTemplates = new();
        readonly Reactive<bool> prevButtonEnabled = new();
        readonly Reactive<bool> nextButtonEnabled = new();

        public IReadOnlyReactive<(IReadOnlyList<OwnItemTemplate> ownItemTemplates, bool fetchFailed)> OwnItemTemplates => ownItemTemplates;
        public IReadOnlyReactive<bool> PrevButtonEnabled => prevButtonEnabled;
        public IReadOnlyReactive<bool> NextButtonEnabled => nextButtonEnabled;

        CancellationTokenSource ctx;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;

        public (VisualElement, IDisposable) LoginAndCreateView()
        {
            userInfo = TokenAuthRepository.GetLoggedIn();
            if (ctx != null)
            {
                ctx.Cancel();
                ctx.Dispose();
            }
            ctx = new();

            view = new UploadedCraftItemTemplateInfoView();

            currentPage = 1;
            prevPage = 0;
            nextPage = 0;

            RequestUpdatePage(currentPage);

            isCreated = true;
            return (view, view.Bind(this));
        }


        void RequestUpdatePage(int page)
        {
            RequestUpdatePageAsync(page, ctx.Token).Forget();
        }

        async Task RequestUpdatePageAsync(int page, CancellationToken cancellationToken)
        {
            if (isUpdating)
            {
                return;
            }
            isUpdating = true;
            try
            {
                DisablePageButtons();
                var result = await APIServiceClient.GetOwnItemTemplatesAsync(userInfo.VerifiedToken, 30, "not-hidden",
                    page, cancellationToken);
                var pageData = result.PageData;
                currentPage = page;
                prevPage = pageData.Prev;
                nextPage = pageData.Next;
                ownItemTemplates.Val = (result.OwnItemTemplates, false);
                UpdatePageButtons(pageData.Prev, pageData.Next, page);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                ownItemTemplates.Val = (null, true);
                throw;
            }
            finally
            {
                isUpdating = false;
            }
        }

        void DisablePageButtons()
        {
            prevButtonEnabled.Val = false;
            nextButtonEnabled.Val = false;
        }

        void UpdatePageButtons(int prev, int next, int current)
        {
            prevButtonEnabled.Val = prev != 0;
            nextButtonEnabled.Val = next != 0;
        }

        public void OnPrevClicked()
        {
            RequestUpdatePage(prevPage);
        }

        public void OnNextClicked()
        {
            RequestUpdatePage(nextPage);
        }

        public void OnRefreshClicked()
        {
            RequestUpdatePage(currentPage);
        }

        public void Logout()
        {
            Clear();
        }

        void Clear()
        {
            if (!isCreated)
            {
                return;
            }
            if (ctx != null)
            {
                ctx.Cancel();
                ctx.Dispose();
                ctx = null;
            }

            isCreated = false;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
