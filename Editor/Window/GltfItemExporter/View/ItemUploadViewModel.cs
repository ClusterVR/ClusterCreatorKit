using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using UserInfo = ClusterVR.CreatorKit.Editor.Api.User.UserInfo;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemUploadViewModel : IRequireTokenAuthMainView, IDisposable
    {
        readonly Reactive<ItemViewModel[]> itemViewModels = new(new ItemViewModel[] { });
        readonly Reactive<bool> uploadAsBeta = new();
        readonly Reactive<float> uploadProgress = new();
        readonly Reactive<ItemUploadProgressWindow.ItemUploadStatus> uploadStatus = new();

        UserInfo? loginUserInfo;

        CancellationTokenSource uploadCancellationTokenSource;
        readonly IItemBuilderDependencies dependencies;
        readonly UploadingItemRepository itemRepository;
        readonly ItemBuilder itemBuilder;
        readonly ItemUploader itemUploader;
        readonly string editorTypeName;

        public IReadOnlyReactive<ItemViewModel[]> ItemViewModels => itemViewModels;
        public IReadOnlyReactive<bool> UploadAsBeta => uploadAsBeta;
        public IReadOnlyReactive<float> UploadProgress => uploadProgress;
        public IReadOnlyReactive<ItemUploadProgressWindow.ItemUploadStatus> UploadStatus =>
            uploadStatus;

        readonly IDisposable disposable;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;

        public ItemUploadViewModel(IItemBuilderDependencies dependencies)
        {
            this.dependencies = dependencies;
            itemRepository = dependencies.ItemRepository;
            itemBuilder = new ItemBuilder(dependencies);
            itemUploader = new ItemUploader(dependencies, itemBuilder);
            editorTypeName = dependencies.EditorTypeName;

            uploadAsBeta.Val = dependencies.IsBetaAllowedItemType && ClusterCreatorKitSettings.instance.IsBeta;

            disposable = Disposable.Create(
                ReactiveBinder.Bind(itemRepository.Items, RefreshItemViewModels),
                ReactiveBinder.Bind(uploadStatus, status =>
                {
                    switch (status)
                    {
                        case ItemUploadProgressWindow.ItemUploadStatus.Standby:
                        case ItemUploadProgressWindow.ItemUploadStatus.Uploading:
                            break;
                        case ItemUploadProgressWindow.ItemUploadStatus.Finish:
                            itemRepository.Clear();
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }));
        }

        void RefreshItemViewModels(IReadOnlyList<UploadingItem> items)
        {
            itemViewModels.Val = items.Select(item =>
            {
                var itemViewModel = new ItemViewModel();
                itemViewModel.OnRemoveButtonClicked += RemoveItemViewModel;
                itemViewModel.SetUploadingItem(item);
                return itemViewModel;

            }).ToArray();
        }

        public (VisualElement, IDisposable) LoginAndCreateView()
        {
            loginUserInfo = TokenAuthRepository.GetLoggedIn();
            var view = new ItemUploadView(editorTypeName);
            return (view, view.Bind(this));
        }

        public void OnDropItems(Object[] items)
        {
            AddItems(items);
        }

        public void OnAddItemButtonClicked()
        {
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "", 0);
        }

        public void OnClearItemViewsButtonClicked()
        {
            var result = EditorUtility.DisplayDialog(TranslationTable.cck_delete_multiple_items, TranslationTable.cck_confirm_delete_all_items, TranslationTable.cck_yes, TranslationTable.cck_no);
            if (result)
            {
                itemRepository.Clear();
            }
        }

        public void OnUploadItemsButtonClicked()
        {
            UploadItems(ClusterCreatorKitSettings.instance.IsBeta);
        }

        public void Logout()
        {
            Clear();
        }

        void Clear()
        {
            itemRepository.Clear();

            loginUserInfo = null;
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

        void UploadItems(bool isBeta)
        {
            UploadAsync(isBeta).Forget();
        }

        bool IsPrefabAsset(Object obj)
        {
            return (obj is GameObject gameObject) && EditorUtility.IsPersistent(gameObject);
        }

        void AddItems(Object[] objects)
        {
            var uploadingItems = objects
                .OfType<GameObject>()
                .Where(IsPrefabAsset)
                .Select(gameObject => itemBuilder.BuildItem(gameObject, ClusterCreatorKitSettings.instance.IsBeta));
            itemRepository.AddOrUpdateItems(uploadingItems);
        }

        void RemoveItemViewModel(ItemViewModel itemView)
        {
            itemRepository.Remove(itemView.UploadingItem);
        }

        public void OnProgressWindowClosed()
        {
            CancelUpload();
            uploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Standby;
        }

        async Task UploadAsync(bool isBeta)
        {
            if (!loginUserInfo.HasValue ||
                uploadStatus.Val == ItemUploadProgressWindow.ItemUploadStatus.Uploading)
            {
                return;
            }
            try
            {
                uploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Uploading;
                CancelUpload();
                uploadCancellationTokenSource = new CancellationTokenSource();

                await itemUploader.UploadItemAsync(
                    rate => uploadProgress.Val = rate,
                    itemViewModels.Val,
                    loginUserInfo.Value.VerifiedToken,
                    isBeta,
                    uploadCancellationTokenSource.Token);

                uploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Finish;
                var uploadedItemsManagementUrl = dependencies.GetUploadedItemsManagementUrl();
                Application.OpenURL(uploadedItemsManagementUrl);
                PanamaLogger.LogCckOpenLink(uploadedItemsManagementUrl, "ItemUploadView_UploadComplete");
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning(TranslationTable.cck_upload_interrupted);
                throw;
            }
            catch (Exception e)
            {
                Debug.LogError(TranslationUtility.GetMessage(TranslationTable.cck_item_upload_failed, e.Message));
                uploadStatus.Val = ItemUploadProgressWindow.ItemUploadStatus.Standby;
            }
        }

        void CancelUpload()
        {
            uploadCancellationTokenSource?.Cancel();
            uploadCancellationTokenSource?.Dispose();
            uploadCancellationTokenSource = null;
        }

        public void Dispose()
        {
            CancelUpload();
            Clear();
            disposable.Dispose();
        }
    }
}
