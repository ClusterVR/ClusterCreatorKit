using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemUploadView : VisualElement
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ItemUploaderWindow.uxml";
        const string MainStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ItemUploaderWindow.uss";
        const string ItemViewStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ItemView.uss";

        const int ItemViewHeight = 95;

        readonly string editorTypeName;

        readonly Label itemCountLabel;
        readonly ListView itemList;
        readonly VisualElement itemContainer;
        readonly VisualElement helpViewContainer;
        readonly VisualElement dragAreaPanel;

        readonly Button clearItemViewsButton;
        readonly Button uploadItemsButton;

        readonly Button addItemButton;

        readonly List<ItemViewModel> itemViewModels = new();

        ItemUploadProgressWindow progressWindow;
        event Action OnProgressWindowClosed;

        event Action<Object[]> OnDropItems;

        public ItemUploadView(string editorTypeName)
        {
            this.editorTypeName = editorTypeName;
            var mainTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath);
            VisualElement view = mainTemplate.CloneTree();
            var mainStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStyleSheetPath);
            var itemViewStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ItemViewStyleSheetPath);
            view.styleSheets.Add(mainStyleSheet);
            view.styleSheets.Add(itemViewStyleSheet);
            hierarchy.Add(view);

            itemCountLabel = view.Q<Label>("item-count-label");

            addItemButton = view.Q<Button>("add-item-button");
            addItemButton.text = TranslationUtility.GetMessage(TranslationTable.cck_editor_type_addition, editorTypeName);
            var addItemButtonLabel = view.Q<Label>("select-prefab-to-upload");
            addItemButtonLabel.text = TranslationTable.cck_select_prefab_to_upload;

            clearItemViewsButton = view.Q<Button>("clear-item-list-button");
            clearItemViewsButton.text = TranslationTable.cck_delete_all;

            uploadItemsButton = view.Q<Button>("upload-button");

            itemList = view.Q<ListView>("item-list-view");
            itemList.makeItem = () =>
            {
                var template = new ItemView();
                return template;
            };
            itemList.bindItem = (itemElement, itemIdx) =>
            {
                var itemView = itemViewModels[itemIdx];
                ItemView.Bind(itemView, (ItemView) itemElement);
            };
            itemList.itemsSource = itemViewModels;
            itemList.fixedItemHeight = ItemViewHeight;
            itemList.selectionType = SelectionType.None;

            itemContainer = view.Q<VisualElement>("item-list-view-container");
            helpViewContainer = view.Q<VisualElement>("help-view-container");
            dragAreaPanel = view.Q<VisualElement>("drag-area-panel");
            var dragAreaLabel = view.Q<Label>("drag-and-drop-prefab");
            dragAreaLabel.text = TranslationTable.cck_drag_and_drop_prefab;

            var itemListPanel = view.Q<VisualElement>("item-list-panel");
            itemListPanel.RegisterCallback<DragEnterEvent>(OnDragEnter);
            itemListPanel.RegisterCallback<DragLeaveEvent>(OnDragLeave);
            itemListPanel.RegisterCallback<DragUpdatedEvent>(OnDragUpdate);
            itemListPanel.RegisterCallback<DragPerformEvent>(OnDragPerform);

            EnableDragAreaPanel(false);
        }

        public IDisposable Bind(ItemUploadViewModel viewModel)
        {
            OnDropItems += viewModel.OnDropItems;
            addItemButton.clicked += viewModel.OnAddItemButtonClicked;
            clearItemViewsButton.clicked += viewModel.OnClearItemViewsButtonClicked;
            uploadItemsButton.clicked += viewModel.OnUploadItemsButtonClicked;
            OnProgressWindowClosed += viewModel.OnProgressWindowClosed;

            var disposables = new[]
            {
                new Disposable(() =>
                {
                    OnDropItems -= viewModel.OnDropItems;
                    addItemButton.clicked -= viewModel.OnAddItemButtonClicked;
                    clearItemViewsButton.clicked -= viewModel.OnClearItemViewsButtonClicked;
                    uploadItemsButton.clicked -= viewModel.OnUploadItemsButtonClicked;
                    OnProgressWindowClosed -= viewModel.OnProgressWindowClosed;
                }),
                new Disposable(() =>
                {
                    progressWindow?.Close();
                    progressWindow = null;
                }),
                ReactiveBinder.Bind(viewModel.ItemViewModels, SetItemList),
                ReactiveBinder.Bind(viewModel.UploadAsBeta, SetUploadItemsButtonAsBeta),
                ReactiveBinder.Bind(viewModel.UploadProgress, rate => progressWindow?.SetProgressRate(rate)),
                ReactiveBinder.Bind(viewModel.UploadStatus, SetUploadStatus)
            };

            return new Disposable(() =>
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            });
        }

        void SetUploadStatus(ItemUploadProgressWindow.ItemUploadStatus status)
        {
            switch (status)
            {
                case ItemUploadProgressWindow.ItemUploadStatus.Standby:
                    progressWindow?.Close();
                    progressWindow = null;
                    break;
                case ItemUploadProgressWindow.ItemUploadStatus.Uploading:
                    var viewRect = GUIUtility.GUIToScreenRect(worldBound);
                    progressWindow = CreateProgressWindow(viewRect);
                    progressWindow.SetStatus(ItemUploadProgressWindow.ItemUploadStatus.Uploading);
                    break;
                case ItemUploadProgressWindow.ItemUploadStatus.Finish:
                    progressWindow?.SetStatus(ItemUploadProgressWindow.ItemUploadStatus.Finish);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        ItemUploadProgressWindow CreateProgressWindow(Rect viewRect)
        {
            var window = ItemUploadProgressWindow.CreateWindow(viewRect, editorTypeName);
            window.OnClose += () =>
            {
                progressWindow = null;
                OnProgressWindowClosed?.Invoke();
            };
            return window;
        }

        void OnDragEnter(DragEnterEvent arg)
        {
            EnableDragAreaPanel(CanDrop());
        }

        void OnDragLeave(DragLeaveEvent arg)
        {
            EnableDragAreaPanel(false);
        }

        void OnDragPerform(DragPerformEvent arg)
        {
            EnableDragAreaPanel(false);
            OnDropItems?.Invoke(DragAndDrop.objectReferences);
            DragAndDrop.AcceptDrag();
        }

        void OnDragUpdate(DragUpdatedEvent arg)
        {
            if (CanDrop())
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }
        }

        bool CanDrop()
        {
            if (DragAndDrop.objectReferences == null)
            {
                return false;
            }
            return DragAndDrop.objectReferences.Any(IsPrefabAsset);
        }

        bool IsPrefabAsset(Object obj)
        {
            return (obj is GameObject gameObject) && EditorUtility.IsPersistent(gameObject);
        }

        void EnableDragAreaPanel(bool isEnable)
        {
            if (dragAreaPanel == null)
            {
                return;
            }

            dragAreaPanel.style.display = isEnable ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void SetUploadItemsButtonAsBeta(bool uploadAsBeta)
        {
            uploadItemsButton.text = uploadAsBeta
                ? TranslationTable.cck_upload_as_beta_feature_item
                : TranslationTable.cck_upload;
        }

        void SetItemList(ItemViewModel[] itemViewModels)
        {
            this.itemViewModels.Clear();
            this.itemViewModels.AddRange(itemViewModels);
            var itemCount = itemViewModels.Length;

            itemList?.Rebuild();
            if (itemContainer != null)
            {
                itemContainer.style.height = ItemViewHeight * itemCount;
            }
            if (itemCountLabel != null)
            {
                itemCountLabel.text = TranslationUtility.GetMessage(TranslationTable.cck_item_count, itemCount);
            }

            var hasItem = itemCount > 0;
            var hasInvalidItem = itemViewModels.Any(itemView => !itemView.IsValid);

            clearItemViewsButton?.SetEnabled(hasItem);
            uploadItemsButton?.SetEnabled(hasItem && !hasInvalidItem);

            if (helpViewContainer != null)
            {
                helpViewContainer.style.display = hasItem ? DisplayStyle.None : DisplayStyle.Flex;
            }
        }
    }
}
