using System;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Custom;
using ClusterVR.CreatorKit.Editor.Enquete;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemUploadProgressWindow : EditorWindow
    {
        public enum ItemUploadStatus
        {
            Standby,
            Uploading,
            Finish,
        }

        static readonly Vector2 WindowSize = new Vector2(480, 160);

        public event Action OnClose;

        VisualElement progressContainer;
        VisualElement completeContainer;
        ProgressBar progressBar;
        Label uploadItemLabel;

        public static ItemUploadProgressWindow CreateWindow(Rect parentRect, string editorTypeName)
        {
            var window = CreateInstance<ItemUploadProgressWindow>();
            var position = new Vector2(
                parentRect.position.x + parentRect.width / 2 - WindowSize.x / 2,
                parentRect.position.y + parentRect.height / 2 - WindowSize.y / 2);
            window.position = new Rect(position.x, position.y, WindowSize.x, WindowSize.y);
            window.SetUploadLabelStr(editorTypeName);
            window.ShowPopup();

            return window;
        }

        void SetUploadLabelStr(string editorTypeName)
        {
            uploadItemLabel.text = $"{editorTypeName}のアップロード中";
        }

        public void SetStatus(ItemUploadStatus status)
        {
            progressContainer?.SetVisibility(status == ItemUploadStatus.Uploading);
            completeContainer?.SetVisibility(status == ItemUploadStatus.Finish);
        }

        public void SetProgressRate(float rate)
        {
            progressBar.value = rate;
        }

        void OnEnable()
        {
            var view = CreateView();
            rootVisualElement.Add(view);
        }

        void OnDisable()
        {
            OnClose?.Invoke();
        }

        VisualElement CreateView()
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ItemUploaderProgressWindow.uxml");

            VisualElement view = template.CloneTree();

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ItemUploaderProgressWindow.uss");
            view.styleSheets.Add(styleSheet);

            var closeButton = view.Q<Button>("close-button");
            closeButton.clicked += Close;

            var enqueteButton = view.Q<Button>("enquete-button");
            enqueteButton.clicked += EnqueteService.OpenEnqueteLink;
            enqueteButton.clicked += Close;

            var enqueteCloseButton = view.Q<Button>("enquete-close-button");
            enqueteCloseButton.clicked += Close;
            enqueteCloseButton.clicked += EnqueteService.CancelEnquete;

            progressContainer = view.Q("progress-container");
            progressBar = view.Q<ProgressBar>("upload-progress-bar");

            uploadItemLabel = view.Q<Label>("upload-item-label");

            var shouldShowEnquete = EnqueteService.ShouldShowEnqueteRequest();

            var normalButtonContainer = view.Q<VisualElement>("normal-complete-container");
            var enqueteButtonContainer = view.Q<VisualElement>("enquete-complete-container");

            normalButtonContainer.SetVisibility(!shouldShowEnquete);
            enqueteButtonContainer.SetVisibility(shouldShowEnquete);

            completeContainer = shouldShowEnquete
                ? enqueteButtonContainer
                : normalButtonContainer;

            return view;
        }
    }
}
