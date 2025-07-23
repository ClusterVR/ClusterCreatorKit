using System;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class VenueUploadWindow : EditorWindow
    {
        readonly VenueUploadViewModel venueUploadViewModel = new VenueUploadViewModel();
        Disposable disposables;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        [MenuItem(TranslationTable.cck_cluster_world_upload, priority = 301)]
        public static void Open()
        {
            var window = GetWindow<VenueUploadWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_world_upload);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_UploadWorld);
        }

        void OnEnable()
        {
            Input.imeCompositionMode = IMECompositionMode.On;
            AwaitRefreshingAndCreateView();
        }

        void OnDisable()
        {
            Input.imeCompositionMode = IMECompositionMode.Auto;
            venueUploadViewModel?.Dispose();
            disposables?.Dispose();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        void AwaitRefreshingAndCreateView()
        {
            EditorApplication.update -= AwaitRefreshingAndCreateView;

            if (EditorApplication.isUpdating)
            {
                EditorApplication.update += AwaitRefreshingAndCreateView;
                return;
            }

            CreateView();
        }

        void CreateView()
        {
            rootVisualElement.styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/ClusterStyle.uss"));

            var tokenAuth = new TokenAuthFrameViewModel(venueUploadViewModel);
            var tokenAuthView = new TokenAuthFrameView();
            var tokenAuthViewDisposable = tokenAuthView.Bind(tokenAuth);
            tokenAuthView.SetEnabled(true);

            rootVisualElement.Add(tokenAuthView);

            disposables = Disposable.Create(
                tokenAuth,
                tokenAuthViewDisposable);
        }
    }
}
