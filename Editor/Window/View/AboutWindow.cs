using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class AboutWindow : EditorWindow
    {
        [InitializeOnLoadMethod]
        static void ShowWindowOnlyOnce()
        {
            if (EditorPrefsUtils.HasAlreadyShownAboutWindow)
            {
                return;
            }
            ShowWindow();
            EditorPrefsUtils.HasAlreadyShownAboutWindow = true;
        }

        [MenuItem(TranslationTable.cck_cluster_about_menu, priority = 310)]
        public static void ShowWindow()
        {
            var wnd = GetWindow<AboutWindow>();
            wnd.titleContent = new GUIContent(TranslationTable.cck_about);
        }

        void OnEnable()
        {
            minSize = new Vector2(200, 350);
            AwaitRefreshingAndCreateView();
        }

        void AwaitRefreshingAndCreateView()
        {
            EditorApplication.update -= AwaitRefreshingAndCreateView;

            if (EditorApplication.isUpdating)
            {
                EditorApplication.update += AwaitRefreshingAndCreateView;
                return;
            }

            rootVisualElement.Clear();
            rootVisualElement.Add(CreateView());
        }

        static VisualElement CreateView()
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uxml/AboutWindow.uxml");
            VisualElement view = template.CloneTree();
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/AboutWindow.uss");
            view.styleSheets.Add(styleSheet);

            view.Query<Button>("open-document").ForEach(b =>
                b.clickable.clicked += () => Application.OpenURL("https://docs.cluster.mu/creatorkit/"));
            view.Query<Button>("open-creators-guide").ForEach(b =>
                b.clickable.clicked += () => Application.OpenURL("https://creator.cluster.mu/"));
            var statisticalInfoLabel = view.Q<Label>("send_statistical_info");
            statisticalInfoLabel.text = TranslationTable.cck_send_statistical_info;
            var settingButton = view.Query<Button>("open-settings-window");
            settingButton.ForEach(b =>
            {
                b.text = TranslationTable.cck_open_settings;
                b.clickable.clicked += SettingsWindow.ShowWindow;
            });

            return view;
        }
    }
}
