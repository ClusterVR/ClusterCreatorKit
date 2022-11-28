using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.ItemExporter;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class CraftItemUploadWindow : EditorWindow
    {
        readonly ItemUploadView itemUploadView = new ItemUploadView(
            new CraftItemExporter(),
            new CraftItemComponentValidator(),
            new CraftItemValidator(),
            new CraftItemTemplateBuilder(),
            new UploadCraftItemTemplateService(),
            "アイテム");
        readonly List<IDisposable> disposables = new List<IDisposable>();

        [MenuItem("Cluster/クラフトアイテムアップロード", priority = 302)]
        public static void Open()
        {
            var window = GetWindow<CraftItemUploadWindow>();
            window.minSize = new Vector2(640, 480);
            window.titleContent = new GUIContent("クラフトアイテムアップロード");
        }

        void OnEnable()
        {
            var view = CreateView();
            rootVisualElement.Add(view);
        }

        void OnDisable()
        {
            itemUploadView.Dispose();
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
        }

        void OnGUI()
        {
            itemUploadView.AddObjectPickerItem();
        }

        VisualElement CreateView()
        {
            var tokenAuth = new RequireTokenAuthView(itemUploadView);
            var tokenAuthView = tokenAuth.CreateView();

            var disposable = ReactiveBinder.Bind(itemUploadView.ReactiveItemUploadStatus(), (status) =>
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
            });

            disposables.Add(tokenAuth);
            disposables.Add(disposable);

            return tokenAuthView;
        }
    }
}
