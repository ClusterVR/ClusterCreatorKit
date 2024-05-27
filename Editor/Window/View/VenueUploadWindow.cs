using System;
using System.Collections.Generic;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class VenueUploadWindow : EditorWindow
    {
        readonly VenueUploadView venueUploadView = new VenueUploadView();
        readonly List<IDisposable> disposables = new List<IDisposable>();
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        TokenAuthWidget tokenAuthWidget;

        [MenuItem(TranslationTable.cck_cluster_world_upload, priority = 301)]
        public static void Open()
        {
            var window = GetWindow<VenueUploadWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_world_upload);
        }

        void OnEnable()
        {
            Input.imeCompositionMode = IMECompositionMode.On;
            AwaitRefreshingAndCreateView();
        }

        void OnDisable()
        {
            Input.imeCompositionMode = IMECompositionMode.Auto;
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
            tokenAuthWidget?.Dispose();
            venueUploadView?.Dispose();
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

            var tokenAuth = new RequireTokenAuthView(venueUploadView);
            var tokenAuthView = tokenAuth.CreateView();

            rootVisualElement.Add(tokenAuthView);

            disposables.Add(tokenAuth);
        }
    }
}
