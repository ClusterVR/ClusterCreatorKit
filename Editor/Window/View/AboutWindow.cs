using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class AboutWindow : EditorWindow
    {
        const string CreatorKitDocumentUrl = "https://docs.cluster.mu/creatorkit/";
        const string CreatorsGuideUrl = "https://creator.cluster.mu/";

#if cck_ja
        const string PrivacyPolicyUrl = "https://help.cluster.mu/hc/ja-jp/articles/20264222848153-Privacy-Policy";
#else
        const string PrivacyPolicyUrl = "https://help.cluster.mu/hc/en-us/articles/20264222848153-Privacy-Policy";
#endif

        static EditorPrefsRepository EditorPrefsRepository => EditorPrefsRepository.Instance;

        [InitializeOnLoadMethod]
        static void ShowWindowOnlyOnce()
        {
            if (EditorPrefsRepository.HasAlreadyShownAboutWindow.Val)
            {
                return;
            }
            ShowWindow();
            EditorPrefsRepository.SetHasAlreadyShownAboutWindow(true);
        }

        [MenuItem(TranslationTable.cck_cluster_about_menu, priority = 400)]
        public static void ShowWindow()
        {
            var wnd = GetWindow<AboutWindow>();
            wnd.titleContent = new GUIContent(TranslationTable.cck_about);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_About);
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
                b.clickable.clicked += () =>
                {
                    Application.OpenURL(CreatorKitDocumentUrl);
                    PanamaLogger.LogCckOpenLink(CreatorKitDocumentUrl, "AboutWindow_OpenDocument");
                });
            view.Query<Button>("open-creators-guide").ForEach(b =>
                b.clickable.clicked += () =>
                {
                    Application.OpenURL(CreatorsGuideUrl);
                    PanamaLogger.LogCckOpenLink(CreatorsGuideUrl, "AboutWindow_OpenCreatorsGuide");
                });
            var dataCollectionPolicyLabel = view.Q<Label>("data-collection-policy");
            dataCollectionPolicyLabel.text = TranslationTable.cck_data_collection_policy_notice_short;
            var openPrivacyPolicyButton = view.Query<Button>("open-privacy-policy");
            openPrivacyPolicyButton.ForEach(b =>
            {
                b.text = TranslationTable.cck_open_privacy_policy;
                b.clickable.clicked += () =>
                {
                    Application.OpenURL(PrivacyPolicyUrl);
                    PanamaLogger.LogCckOpenLink(PrivacyPolicyUrl, "SettingsWindow_PrivacyPolicy");
                };
            });

            return view;
        }
    }
}
