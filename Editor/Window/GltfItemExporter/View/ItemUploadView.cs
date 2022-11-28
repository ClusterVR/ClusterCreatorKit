using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.ItemExporter;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using UserInfo = ClusterVR.CreatorKit.Editor.Api.User.UserInfo;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemUploadView : IRequireTokenAuthMainView, IDisposable
    {
        const string MainTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ItemUploaderWindow.uxml";
        const string ItemTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ItemView.uxml";
        const string MainStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ItemUploaderWindow.uss";
        const string ItemViewStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ItemView.uss";

        const int ItemViewHeight = 95;

        readonly List<ItemView> itemViews = new List<ItemView>();

        readonly Reactive<ItemUploadProgressWindow.ItemUploadStatus> reactiveItemUploadStatus =
            new Reactive<ItemUploadProgressWindow.ItemUploadStatus>();

        UserInfo? loginUserInfo;

        Label itemCountLabel;
        ListView itemList;
        VisualElement itemContainer;
        VisualElement helpViewContainer;
        VisualElement dragAreaPanel;
        IDisposable disposable;

        Button clearItemViewsButton;
        Button uploadItemsButton;

        ItemUploadProgressWindow progressWindow;

        CancellationTokenSource cancellationTokenSource;
        readonly IItemExporter itemExporter;
        readonly IComponentValidator componentValidator;
        readonly IGltfValidator gltfValidator;
        readonly IItemTemplateBuilder itemTemplateBuilder;
        readonly IItemUploadService uploadService;
        readonly string editorTypeName;

        public Reactive<ItemUploadProgressWindow.ItemUploadStatus> ReactiveItemUploadStatus() =>
            reactiveItemUploadStatus;

        public static event Action<IReadOnlyList<(GameObject item, string itemTemplateId)>> OnItemUploaded;

        public ItemUploadView(IItemExporter itemExporter,
            IComponentValidator componentValidator,
            IGltfValidator gltfValidator,
            IItemTemplateBuilder itemTemplateBuilder,
            IItemUploadService uploadService,
            string editorTypeName)
        {
            this.itemExporter = itemExporter;
            this.componentValidator = componentValidator;
            this.gltfValidator = gltfValidator;
            this.itemTemplateBuilder = itemTemplateBuilder;
            this.uploadService = uploadService;
            this.editorTypeName = editorTypeName;
        }

        public VisualElement LoginAndCreateView(UserInfo userInfo)
        {
            loginUserInfo = userInfo;

            var mainTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MainTemplatePath);
            VisualElement view = mainTemplate.CloneTree();
            var mainStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(MainStyleSheetPath);
            var itemViewStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ItemViewStyleSheetPath);
            view.styleSheets.Add(mainStyleSheet);
            view.styleSheets.Add(itemViewStyleSheet);

            var itemTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ItemTemplatePath);

            itemCountLabel = view.Q<Label>("item-count-label");

            var addItemButton = view.Q<Button>("add-item-button");
            addItemButton.text = $"{editorTypeName}の追加";
            addItemButton.clicked += () =>
            {
                EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "", 0);
            };

            clearItemViewsButton = view.Q<Button>("clear-item-list-button");
            clearItemViewsButton.clicked += () =>
            {
                var result = EditorUtility.DisplayDialog("複数アイテムの削除", "アイテムをすべて削除しますか？",
                    "はい", "いいえ");
                if (result)
                {
                    ClearItemViews();
                }
            };

            uploadItemsButton = view.Q<Button>("upload-button");
            uploadItemsButton.clicked += UploadItems;

            Func<VisualElement> makeItemView = () =>
            {
                var template = itemTemplate.CloneTree();
                return template;
            };

            Action<VisualElement, int> bindItemView = (itemElement, itemIdx) =>
            {
                var itemView = itemViews[itemIdx];
                itemView.BindItemView(itemElement);
            };

            itemList = view.Q<ListView>("item-list-view");
            itemList.makeItem = makeItemView;
            itemList.bindItem = bindItemView;
            itemList.itemsSource = itemViews;
            itemList.itemHeight = ItemViewHeight;
            itemList.selectionType = SelectionType.None;

            itemContainer = view.Q<VisualElement>("item-list-view-container");
            helpViewContainer = view.Q<VisualElement>("help-view-container");
            dragAreaPanel = view.Q<VisualElement>("drag-area-panel");

            var itemListPanel = view.Q<VisualElement>("item-list-panel");

            itemListPanel.RegisterCallback<DragEnterEvent>(OnDragEnter);
            itemListPanel.RegisterCallback<DragLeaveEvent>(OnDragLeave);
            itemListPanel.RegisterCallback<DragUpdatedEvent>(OnDragUpdate);
            itemListPanel.RegisterCallback<DragExitedEvent>(OnDragExited);

            EnableDragAreaPanel(false);
            RefreshItemList();

            disposable = ReactiveBinder.Bind(reactiveItemUploadStatus, (status) =>
            {
                switch (status)
                {
                    case ItemUploadProgressWindow.ItemUploadStatus.Standby:
                        progressWindow?.Close();
                        progressWindow = null;
                        break;
                    case ItemUploadProgressWindow.ItemUploadStatus.Uploading:
                        var viewRect = GUIUtility.GUIToScreenRect(view.worldBound);

                        progressWindow = ItemUploadProgressWindow.CreateWindow(viewRect, editorTypeName);
                        progressWindow.OnClose += OnProgressWindowClosed;
                        progressWindow.SetStatus(ItemUploadProgressWindow.ItemUploadStatus.Uploading);
                        break;
                    case ItemUploadProgressWindow.ItemUploadStatus.Finish:
                        ClearItemViews();
                        progressWindow.SetStatus(ItemUploadProgressWindow.ItemUploadStatus.Finish);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });

            return view;
        }

        public void Logout()
        {
            Clear();
        }

        void Clear()
        {
            ClearItemViews();

            loginUserInfo = null;

            itemCountLabel = null;
            itemList = null;
            itemContainer = null;
            helpViewContainer = null;
            dragAreaPanel = null;

            clearItemViewsButton = null;
            uploadItemsButton = null;

            progressWindow?.Close();
            progressWindow = null;
        }

        public void AddObjectPickerItem()
        {
            if (loginUserInfo == null)
            {
                return;
            }
            if (Event.current.type == EventType.ExecuteCommand)
            {
                if (Event.current.commandName == "ObjectSelectorClosed")
                {
                    var obj = EditorGUIUtility.GetObjectPickerObject();
                    AddItems(new[] { obj });
                    Event.current.Use();
                }
            }
        }

        void UploadItems()
        {
            _ = UploadAsync();
        }

        void OnDragEnter(DragEnterEvent arg)
        {
            EnableDragAreaPanel(CanDrop());
        }

        void OnDragLeave(DragLeaveEvent arg)
        {
            EnableDragAreaPanel(false);
        }

        void OnDragExited(DragExitedEvent arg)
        {
            EnableDragAreaPanel(false);
            AddItems(DragAndDrop.objectReferences);
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

        void AddItems(Object[] objects)
        {
            foreach (var obj in objects)
            {
                if (!(obj is GameObject gameObject) || !IsPrefabAsset(gameObject))
                {
                    continue;
                }

                var targetItemView =
                    itemViews.FirstOrDefault(item =>
                        item.Item != null && item.Item.GetInstanceID() == gameObject.GetInstanceID());
                if (targetItemView != null)
                {
                    targetItemView.SetItem(gameObject);
                }
                else
                {
                    AddNewItem(gameObject);
                }
            }
            RefreshItemList();
        }

        void AddNewItem(GameObject gameObject)
        {
            var itemView = new ItemView(itemExporter, componentValidator, gltfValidator, itemTemplateBuilder);
            itemView.SetItem(gameObject);
            itemView.OnRemoveButtonClicked += RemoveItem;

            itemViews.Insert(0, itemView);
        }

        void RemoveItem(ItemView itemView)
        {
            itemViews.Remove(itemView);
            itemView.Dispose();

            RefreshItemList();
        }

        void ClearItemViews()
        {
            foreach (var itemView in itemViews)
            {
                itemView.Dispose();
            }
            itemViews.Clear();

            RefreshItemList();
        }

        void EnableDragAreaPanel(bool isEnable)
        {
            if (dragAreaPanel == null)
            {
                return;
            }

            dragAreaPanel.style.display = isEnable ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void RefreshItemList()
        {
            itemList?.Rebuild();
            if (itemContainer != null)
            {
                itemContainer.style.height = ItemViewHeight * itemViews.Count;
            }
            if (itemCountLabel != null)
            {
                itemCountLabel.text = $"アイテム数： {itemViews.Count}";
            }

            var hasItem = itemViews.Count > 0;
            var hasInvalidItem = itemViews.Any(itemView => !itemView.IsValid);

            clearItemViewsButton?.SetEnabled(hasItem);
            uploadItemsButton?.SetEnabled(hasItem && !hasInvalidItem);

            if (helpViewContainer != null)
            {
                helpViewContainer.style.display = hasItem ? DisplayStyle.None : DisplayStyle.Flex;
            }
        }

        void OnProgressWindowClosed()
        {
            UploadCancelRequest();
            progressWindow = null;
            reactiveItemUploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Standby;
        }

        async Task UploadAsync()
        {
            if (!loginUserInfo.HasValue ||
                reactiveItemUploadStatus.Val == ItemUploadProgressWindow.ItemUploadStatus.Uploading)
            {
                return;
            }
            try
            {
                reactiveItemUploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Uploading;
                UploadCancelRequest();
                cancellationTokenSource = new CancellationTokenSource();

                await UploadItemAsync(loginUserInfo.Value.VerifiedToken, cancellationTokenSource.Token);

                reactiveItemUploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Finish;
                Application.OpenURL(uploadService.UploadedItemsManagementUrl);
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("アップロード処理が中断されました");
            }
            catch (Exception e)
            {
                Debug.LogError($"アイテムのアップロードに失敗しました: {e.Message}");
                reactiveItemUploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Standby;
                throw;
            }
        }

        async Task UploadItemAsync(string verifiedToken, CancellationToken cancellationToken)
        {
            uploadService.SetAccessToken(verifiedToken);

            var validItemViews = itemViews.Where(item => item.IsValid).ToArray();
            var length = validItemViews.Length;
            var itemList = new List<(GameObject item, string itemTemplateId)>(validItemViews.Length);
            foreach (var (itemView, i) in validItemViews.Select((item, i) => (item, i)))
            {
                cancellationToken.ThrowIfCancellationRequested();

                progressWindow?.SetProgressRate(i * 100f / length);
                var zipBinary = await itemView.BuildZippedItemBinary();
                var itemTemplateId = await uploadService.UploadItemAsync(zipBinary, cancellationToken);
                itemList.Add((itemView.Item, itemTemplateId));
            }
            OnItemUploaded?.Invoke(itemList);
        }

        void UploadCancelRequest()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        public void Dispose()
        {
            UploadCancelRequest();
            Clear();
            disposable?.Dispose();
        }
    }
}
