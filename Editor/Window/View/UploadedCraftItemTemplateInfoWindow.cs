using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoWindow : EditorWindow
    {
        readonly UploadedCraftItemTemplateInfoViewModel infoViewModel = new();
        Disposable disposables;

        [MenuItem(TranslationTable.cck_cluster_craftitem_info_fetch, priority = 304)]
        public static void Open()
        {
            var window = GetWindow<UploadedCraftItemTemplateInfoWindow>();
            window.minSize = new Vector2(640, 530);
            window.titleContent = new GUIContent(TranslationTable.cck_craftitem_info_fetch);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_FetchCraftItemInfo);
        }

        void OnEnable()
        {
            CreateView();
        }

        void OnDisable()
        {
            rootVisualElement.Clear();
            infoViewModel.Dispose();
            disposables?.Dispose();
        }

        void CreateView()
        {
            var tokenAuth = new TokenAuthFrameViewModel(infoViewModel);
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
