using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.AccessoryExporter;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.Editor.Window.View;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class AccessoryUploadWindow : EditorWindow
    {
        readonly ItemUploadView itemUploadView = new ItemUploadView(
            new CreatorKit.AccessoryExporter.AccessoryExporter(),
            new AccessoryComponentValidator(),
            new AccessoryValidator(),
            new AccessoryTemplateBuilder(),
            new UploadAccessoryTemplateService(),
            "アクセサリー");
        readonly List<IDisposable> disposables = new List<IDisposable>();

        [MenuItem("Cluster/アクセサリーアップロード", priority = 303)]
        public static void Open()
        {
            var window = GetWindow<AccessoryUploadWindow>();
            window.minSize = new Vector2(640, 480);
            window.titleContent = new GUIContent("アクセサリーアップロード");
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
