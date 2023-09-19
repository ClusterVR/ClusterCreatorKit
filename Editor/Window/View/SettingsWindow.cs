using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class SettingsWindow : EditorWindow
    {
        [MenuItem("Cluster/Settings", priority = 305)]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent("Settings");
        }

        void OnEnable()
        {
            rootVisualElement.styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/ClusterStyle.uss"));
            rootVisualElement.Add(CreateView());
        }

        static VisualElement CreateView()
        {
            var container = new VisualElement();
            container.Add(CreatePrivacySettings());
            container.Add(UiUtils.Separator());
            container.Add(CreateBetaSettings());
            return container;
        }

        static VisualElement CreatePrivacySettings()
        {
            var container = new VisualElement();
            var heading = new Label("プライバシー設定");
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var sendingAnalyticsDataToggle = new Toggle("統計情報を送信する")
            {
                value = EditorPrefsUtils.EnableSendingAnalyticsData
            };
            sendingAnalyticsDataToggle.EnableInClassList("h2", true);
            sendingAnalyticsDataToggle.RegisterValueChangedCallback(ev =>
                EditorPrefsUtils.EnableSendingAnalyticsData = ev.newValue);
            container.Add(sendingAnalyticsDataToggle);

            return container;
        }

        static VisualElement CreateBetaSettings()
        {
            var container = new VisualElement();
            var heading = new Label("ベータ機能設定");
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var useBetaToggle = new Toggle("ベータ機能を有効にする")
            {
                value = ClusterCreatorKitSettings.instance.IsBeta
            };
            useBetaToggle.EnableInClassList("h2", true);
            useBetaToggle.RegisterValueChangedCallback(ev =>
            {
                ClusterCreatorKitSettings.instance.IsBeta = ev.newValue;
                CompilationPipeline.RequestScriptCompilation();
            });
            container.Add(useBetaToggle);

            var betaDescriptionLabel = new Label("有効にすると Creator Kit の実験的な機能が使えるようになります。\nアップロードされるワールドやアイテムはベータ版機能を利用しているとして個別にカテゴライズされます。");
            container.Add(betaDescriptionLabel);

            return container;
        }
    }
}
