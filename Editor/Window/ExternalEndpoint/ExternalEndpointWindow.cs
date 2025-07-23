using System;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    public sealed class ExternalEndpointWindow : EditorWindow
    {
        readonly ExternalEndpointViewModel externalEndpointViewModel = new();
        Disposable disposables;

        [MenuItem(TranslationTable.cck_cluster_external_communication_url, priority = 321)]
        public static void Open()
        {
            var window = GetWindow<ExternalEndpointWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_external_communication_url);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_ExternalEndpoint);
        }

        void OnEnable()
        {
            CreateView();
        }

        void OnDisable()
        {
            rootVisualElement.Clear();
            externalEndpointViewModel?.Dispose();
            disposables?.Dispose();
        }

        void CreateView()
        {
            var tokenAuth = new TokenAuthFrameViewModel(externalEndpointViewModel);
            var tokenAuthView = new TokenAuthFrameView();
            var tokenAuthViewDisposable = tokenAuthView.Bind(tokenAuth);
            tokenAuthView.SetEnabled(true);
            disposables = Disposable.Create(
                tokenAuth,
                tokenAuthViewDisposable);
            rootVisualElement.Add(tokenAuthView);
        }
    }
}
