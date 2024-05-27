using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Enquete;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Window.Translation;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class SettingsWindow : EditorWindow
    {
        [MenuItem(TranslationTable.cck_cluster_settings_menu, priority = 305)]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_settings);
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
            container.Add(CreateLanguageSettings());
            container.Add(UiUtils.Separator());
            container.Add(CreateBetaSettings());
            container.Add(UiUtils.Separator());
            container.Add(CreateEnqueteRequestView());

            return container;
        }

        static VisualElement CreatePrivacySettings()
        {
            var container = new VisualElement();

            var heading = new Label(TranslationTable.cck_privacy_settings);
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var sendingAnalyticsDataToggle = new Toggle(TranslationTable.cck_send_statistics)
            {
                value = EditorPrefsUtils.EnableSendingAnalyticsData
            };
            sendingAnalyticsDataToggle.EnableInClassList("h2", true);
            sendingAnalyticsDataToggle.RegisterValueChangedCallback(ev =>
                EditorPrefsUtils.EnableSendingAnalyticsData = ev.newValue);
            container.Add(sendingAnalyticsDataToggle);

            return container;
        }

        static VisualElement CreateLanguageSettings()
        {
            var container = new VisualElement();
            var heading = new Label("Language Settings");
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var languageSettingKey = EditorPrefsUtils.LanguageSetting;
            if (string.IsNullOrEmpty(languageSettingKey))
            {
                TranslationSettings.SetLanguageSettingBySystemLanguage();
                languageSettingKey = EditorPrefsUtils.LanguageSetting;
            }
            var languageSelectionDropdown = new PopupField<string>("Selected Language", ServerLang.LangCodes.ToList(),
                languageSettingKey.Replace("cck_", ""));
            languageSelectionDropdown.RegisterValueChangedCallback(ev =>
            {
                EditorPrefsUtils.LanguageSetting = TranslationSettings.GetLanguageSettingKey(ev.newValue);
                EditorApplication.delayCall += TranslationSettings.ApplySymbolsForTarget;
            });
            container.Add(languageSelectionDropdown);
            var languageChangeInfo = new Label("NOTE: After changing language settings, the editor will be reload.");
            container.Add(languageChangeInfo);
            return container;
        }

        static VisualElement CreateBetaSettings()
        {
            var container = new VisualElement();
            var heading = new Label(TranslationTable.cck_beta_features_settings);
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var useBetaToggle = new Toggle(TranslationTable.cck_enable_beta_features)
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

            var betaDescriptionLabel = new Label(TranslationTable.cck_beta_features_description);
            container.Add(betaDescriptionLabel);

            return container;
        }

        static VisualElement CreateEnqueteRequestView()
        {
            var container = new VisualElement();
            var heading = new Label(TranslationTable.cck_survey);
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var betaDescriptionLabel = new Label(TranslationTable.cck_survey_invitation);
            container.Add(betaDescriptionLabel);

            container.Add(
                new Button(EnqueteService.OpenEnqueteLink)
                {
                    text = TranslationTable.cck_answer
                }
            );

            return container;
        }
    }
}
