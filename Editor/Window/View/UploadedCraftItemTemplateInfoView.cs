using System;
using System.Collections.Generic;
using System.Text;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoView : VisualElement
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/UploadedCraftItemTemplateInfoView.uxml";
        const string MainStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/UploadedCraftItemTemplateInfoView.uss";

        readonly TextField textField;
        readonly Button prevButton;
        readonly Button nextButton;
        readonly Button refreshButton;

        public UploadedCraftItemTemplateInfoView()
        {
            VisualElement mainView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath).CloneTree();
            mainView.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStyleSheetPath));
            hierarchy.Add(mainView);

            textField = mainView.Q<TextField>("main-field");

            prevButton = mainView.Q<Button>("prev-button");
            nextButton = mainView.Q<Button>("next-button");
            refreshButton = mainView.Q<Button>("refresh-button");
            prevButton.text = TranslationTable.cck_previous;
            nextButton.text = TranslationTable.cck_next;
            refreshButton.text = TranslationTable.cck_refresh;
        }

        public IDisposable Bind(UploadedCraftItemTemplateInfoViewModel viewModel)
        {
            prevButton.clicked += viewModel.OnPrevClicked;
            nextButton.clicked += viewModel.OnNextClicked;
            refreshButton.clicked += viewModel.OnRefreshClicked;
            var disposables = new[]
            {
                new Disposable(() =>
                {
                    prevButton.clicked -= viewModel.OnPrevClicked;
                    nextButton.clicked -= viewModel.OnNextClicked;
                    refreshButton.clicked -= viewModel.OnRefreshClicked;
                }),
                ReactiveBinder.Bind(viewModel.PrevButtonEnabled, enabled => prevButton.SetEnabled(enabled)),
                ReactiveBinder.Bind(viewModel.NextButtonEnabled, enabled => nextButton.SetEnabled(enabled)),
                ReactiveBinder.Bind(viewModel.OwnItemTemplates, RenderOwnItemTemplates),
            };

            return new Disposable(() =>
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            });
        }

        void RenderOwnItemTemplates((IReadOnlyList<OwnItemTemplate> ownItemTemplates, bool fetchFailed) v)
        {
            textField.value = v switch
            {
                (_, true) => TranslationTable.cck_information_fetch_failed,
                ({ } ownItemTemplates, false) => AsHumanReadableText(ownItemTemplates),
                _ => ""
            };
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
    }
}
