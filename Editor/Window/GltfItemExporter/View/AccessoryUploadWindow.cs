using System;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class AccessoryUploadWindow : EditorWindow
    {
        readonly ItemUploadViewModel itemUploadViewModel = new ItemUploadViewModel(new AccessoryItemBuilderDependencies());
        Disposable disposables;

        [MenuItem(TranslationTable.cck_cluster_accessory_upload, priority = 303)]
        public static void Open()
        {
            var window = GetWindow<AccessoryUploadWindow>();
            window.minSize = new Vector2(640, 480);
            window.titleContent = new GUIContent(TranslationTable.cck_accessory_upload);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_UploadAccessory);
        }

        void OnEnable()
        {
            CreateView();
        }

        void OnDisable()
        {
            itemUploadViewModel?.Dispose();
            disposables?.Dispose();
        }

        void OnGUI()
        {
            itemUploadViewModel.AddObjectPickerItem();
        }

        void CreateView()
        {
            var tokenAuth = new TokenAuthFrameViewModel(itemUploadViewModel);
            var tokenAuthView = new TokenAuthFrameView();
            var tokenAuthViewDisposable = tokenAuthView.Bind(tokenAuth);

            disposables = Disposable.Create(
                tokenAuth,
                tokenAuthViewDisposable,
                ReactiveBinder.Bind(itemUploadViewModel.UploadStatus, status =>
                {
                    switch (status)
                    {
                        case ItemUploadProgressWindow.ItemUploadStatus.Standby:
                            tokenAuthView.SetEnabled(true);
                            break;
                        case ItemUploadProgressWindow.ItemUploadStatus.Uploading:
                        case ItemUploadProgressWindow.ItemUploadStatus.Finish:
                            tokenAuthView.SetEnabled(false);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                })
            );

            rootVisualElement.Add(tokenAuthView);
        }
    }
}
