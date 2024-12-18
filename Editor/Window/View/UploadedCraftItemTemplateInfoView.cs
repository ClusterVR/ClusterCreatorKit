using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UserInfo = ClusterVR.CreatorKit.Editor.Api.User.UserInfo;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoView : IRequireTokenAuthMainView, IDisposable
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/UploadedCraftItemTemplateInfoView.uxml";
        const string MainStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/UploadedCraftItemTemplateInfoView.uss";

        VisualElement mainView;
        TextField textField;
        Button prevButton;
        Button nextButton;
        Button refreshButton;

        bool isCreated;
        UserInfo userInfo;

        bool isUpdating;
        int currentPage;
        int prevPage;
        int nextPage;

        CancellationTokenSource ctx;

        public VisualElement LoginAndCreateView(UserInfo userInfo)
        {
            this.userInfo = userInfo;
            if (ctx != null)
            {
                ctx.Cancel();
                ctx.Dispose();
            }
            ctx = new();

            mainView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath).CloneTree();
            mainView.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStyleSheetPath));

            textField = mainView.Q<TextField>("main-field");

            prevButton = mainView.Q<Button>("prev-button");
            nextButton = mainView.Q<Button>("next-button");
            refreshButton = mainView.Q<Button>("refresh-button");
            prevButton.text = TranslationTable.cck_previous;
            nextButton.text = TranslationTable.cck_next;
            refreshButton.text = TranslationTable.cck_refresh;

            prevButton.clicked += OnPrevClicked;
            nextButton.clicked += OnNextClicked;
            refreshButton.clicked += () => RequestUpdatePage(currentPage);

            currentPage = 1;
            prevPage = 0;
            nextPage = 0;

            RequestUpdatePage(currentPage);

            isCreated = true;
            return mainView;
        }

        void RequestUpdatePage(int page)
        {
            _ = RequestUpdatePageAsync(page, ctx.Token);
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
                var result = await APIServiceClient.GetOwnItemTemplatesAsync(userInfo.VerifiedToken, 30, "not-hidden", page, cancellationToken);
                var pageData = result.PageData;
                currentPage = page;
                prevPage = pageData.Prev;
                nextPage = pageData.Next;
                textField.value = AsHumanReadableText(result.OwnItemTemplates);
                UpdatePageButtons(pageData.Prev, pageData.Next, page);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                textField.value = TranslationTable.cck_information_fetch_failed;
                Debug.LogException(e);
            }
            finally
            {
                isUpdating = false;
            }
        }

        void DisablePageButtons()
        {
            prevButton.SetEnabled(false);
            nextButton.SetEnabled(false);
        }

        void UpdatePageButtons(int prev, int next, int current)
        {
            prevButton.SetEnabled(prev != 0);
            nextButton.SetEnabled(next != 0);
        }

        string AsHumanReadableText(IReadOnlyList<OwnItemTemplate> ownItemTemplates)
        {
            var sb = new StringBuilder();
            int count = 0;
            foreach (var uploadedItemInfo in ownItemTemplates)
            {
                if (uploadedItemInfo.IsBeta)
                {
                    sb.Append("[beta] ");
                }
                sb.AppendLine($"{uploadedItemInfo.Name}: ItemTemplateId = {uploadedItemInfo.ItemTemplateId}");
                ++count;
            }
            if (count > 0)
            {
                return sb.ToString();
            }
            else
            {
                return TranslationTable.cck_no_uploaded_item_info;
            }
        }

        void OnPrevClicked()
        {
            RequestUpdatePage(prevPage);
        }

        void OnNextClicked()
        {
            RequestUpdatePage(nextPage);
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
