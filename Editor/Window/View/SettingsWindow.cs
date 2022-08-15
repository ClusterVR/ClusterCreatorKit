using ClusterVR.CreatorKit.Editor.Builder;
using UnityEditor;
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

            var privacySettingsHeading = new Label("プライバシー設定");
            privacySettingsHeading.EnableInClassList("h1", true);
            container.Add(privacySettingsHeading);

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
    }
}
