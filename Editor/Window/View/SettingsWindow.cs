using System;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Enquete;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Window.Translation;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class SettingsWindow : EditorWindow
    {
#if cck_ja
        const string PrivacyPolicyUrl = "https://help.cluster.mu/hc/ja-jp/articles/20264222848153-Privacy-Policy";
#else
        const string PrivacyPolicyUrl = "https://help.cluster.mu/hc/en-us/articles/20264222848153-Privacy-Policy";
#endif

        static EditorPrefsRepository EditorPrefsRepository => EditorPrefsRepository.Instance;

        [MenuItem(TranslationTable.cck_cluster_settings_menu, priority = 320)]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_settings);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_Settings);
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
            container.Add(CreateLanguageSettings());
            container.Add(UiUtils.Separator());
            container.Add(CreateBetaSettings());
            container.Add(UiUtils.Separator());
            container.Add(CreateEnqueteRequestView());
            container.Add(UiUtils.Separator());
            container.Add(CreateDataCollectionPolicy());

            return container;
        }

        static VisualElement CreateLanguageSettings()
        {
            var container = new VisualElement();
            var heading = new Label("Language Settings");
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var languageSettingKey = EditorPrefsRepository.LanguageSettings.Val;
            if (string.IsNullOrEmpty(languageSettingKey))
            {
                TranslationSettings.SetLanguageSettingBySystemLanguage();
                languageSettingKey = EditorPrefsRepository.LanguageSettings.Val;
            }
            var languageSelectionDropdown = new PopupField<string>("Selected Language",
                TranslationSettings.EditorLanguages.ToList(),
                Array.IndexOf(TranslationSettings.EditorLanguages, languageSettingKey.Replace("cck_", "")));
            languageSelectionDropdown.RegisterValueChangedCallback(ev =>
            {
                EditorPrefsRepository.SetLanguageSetting(TranslationSettings.GetLanguageSettingKey(ev.newValue));
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

        static VisualElement CreateDataCollectionPolicy()
        {
            var container = new VisualElement();

            var heading = new Label(TranslationTable.cck_data_collection_policy);
            heading.EnableInClassList("h1", true);
            container.Add(heading);

            var dataCollectionPolicyLabel = new Label(TranslationUtility.GetMessage(TranslationTable.cck_data_collection_policy_notice, PrivacyPolicyUrl))
            {
                style =
                {
                    whiteSpace = WhiteSpace.Normal
                }
            };
            container.Add(dataCollectionPolicyLabel);

            container.Add(
                new Button(() =>
                {
                    Application.OpenURL(PrivacyPolicyUrl);
                    PanamaLogger.LogCckOpenLink(PrivacyPolicyUrl, "SettingsWindow_PrivacyPolicy");
                })
                {
                    text = TranslationTable.cck_open_privacy_policy
                }
            );

            return container;
        }
    }
}
