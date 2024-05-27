using System.Linq;
using ClusterVR.CreatorKit.Editor.Custom;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Preview.PlayerController;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public sealed class SettingsWindow : EditorWindow
    {
        [MenuItem("Cluster/Preview/Settings", priority = 114)]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_preview_settings);
        }

        public void OnEnable()
        {
            var root = rootVisualElement;
            root.Add(GenerateCameraControlSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateSavedStateSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateLangCodeSection());
        }

        static VisualElement GenerateCameraControlSection()
        {
            var cameraControlSection = EditorUIGenerator.GenerateSection();
            cameraControlSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_view_options));

            var sensitivitySlider = EditorUIGenerator.GenerateSlider(LabelType.h2, TranslationTable.cck_sensitivity);
            sensitivitySlider.lowValue = 0f;
            sensitivitySlider.highValue = 1f;
            sensitivitySlider.value = CameraControlSettings.Sensitivity;
            sensitivitySlider.RegisterValueChangedCallback(ev => CameraControlSettings.Sensitivity = ev.newValue);
            cameraControlSection.Add(sensitivitySlider);

            var invertVerticalToggle = EditorUIGenerator.GenerateToggle(LabelType.h2, TranslationTable.cck_invert_vertical);
            invertVerticalToggle.value = CameraControlSettings.InvertVertical;
            invertVerticalToggle.RegisterValueChangedCallback(ev => CameraControlSettings.InvertVertical = ev.newValue);
            cameraControlSection.Add(invertVerticalToggle);

            var invertHorizontalToggle = EditorUIGenerator.GenerateToggle(LabelType.h2, TranslationTable.cck_invert_horizontal);
            invertHorizontalToggle.value = CameraControlSettings.InvertHorizontal;
            invertHorizontalToggle.RegisterValueChangedCallback(ev =>
                CameraControlSettings.InvertHorizontal = ev.newValue);
            cameraControlSection.Add(invertHorizontalToggle);

            var standingEyeHeightField = EditorUIGenerator.GenerateFloatField(LabelType.h2, TranslationTable.cck_view_height_standing);
            var sittingEyeHeightField = EditorUIGenerator.GenerateFloatField(LabelType.h2, TranslationTable.cck_view_height_sitting);
            standingEyeHeightField.value = CameraControlSettings.StandingEyeHeight;
            standingEyeHeightField.RegisterValueChangedCallback(ev =>
            {
                CameraControlSettings.StandingEyeHeight = ev.newValue;
                standingEyeHeightField.SetValueWithoutNotify(CameraControlSettings.StandingEyeHeight);
                sittingEyeHeightField.SetValueWithoutNotify(CameraControlSettings.SittingEyeHeight);
            });
            cameraControlSection.Add(standingEyeHeightField);

            sittingEyeHeightField.value = CameraControlSettings.SittingEyeHeight;
            sittingEyeHeightField.RegisterValueChangedCallback(ev =>
            {
                CameraControlSettings.SittingEyeHeight = ev.newValue;
                sittingEyeHeightField.SetValueWithoutNotify(CameraControlSettings.SittingEyeHeight);
            });
            cameraControlSection.Add(sittingEyeHeightField);

            return cameraControlSection;
        }

        static VisualElement GenerateSavedStateSection()
        {
            var section = EditorUIGenerator.GenerateSection();
            section.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_save_feature));

            var informationBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationTable.cck_save_data_preview_warning, MessageType.Info));
            section.Add(informationBox);

            var playingHelpBox =
                new IMGUIContainer(() => EditorGUILayout.HelpBox(TranslationTable.cck_save_data_operation_during_playback, MessageType.Warning));
            section.Add(playingHelpBox);

            var staticContents = new VisualElement();
            section.Add(staticContents);

            var clearThisButton =
                EditorUIGenerator.GenerateButton(LabelType.h2, TranslationTable.cck_delete_current_scene_save_data, AskAndClearActiveScene);
            staticContents.Add(clearThisButton);

            var clearAllButton = EditorUIGenerator.GenerateButton(LabelType.h2, TranslationTable.cck_delete_all_save_data, AskAndClearAllSave);
            staticContents.Add(clearAllButton);

            void UpdatePlayingMode(bool isPlaying)
            {
                playingHelpBox.SetVisibility(isPlaying);
                staticContents.SetEnabled(!isPlaying);
            }

            UpdatePlayingMode(Application.isPlaying);
            EditorApplication.playModeStateChanged += state =>
            {
                switch (state)
                {
                    case PlayModeStateChange.ExitingPlayMode:
                        UpdatePlayingMode(false);
                        break;
                    case PlayModeStateChange.EnteredPlayMode:
                        UpdatePlayingMode(true);
                        break;
                }
            };

            return section;
        }

        static void AskAndClearActiveScene()
        {
            const string title = TranslationTable.cck_save_data_deletion;
            var activeScene = SceneManager.GetActiveScene();
            var activeSceneGuid = AssetDatabase.AssetPathToGUID(activeScene.path);
            if (!PersistedRoomStateRepository.IsSaved(activeSceneGuid))
            {
                EditorUtility.DisplayDialog(title, TranslationTable.cck_no_save_data_current_scene, TranslationTable.cck_ok);
                return;
            }

            if (EditorUtility.DisplayDialog(title, TranslationTable.cck_confirm_delete_current_scene_save_data, TranslationTable.cck_delete_action, TranslationTable.cck_cancel))
            {
                PersistedRoomStateRepository.Clear(activeSceneGuid);
                EditorUtility.DisplayDialog(title, TranslationTable.cck_deleted_current_scene_save_data, TranslationTable.cck_ok);
            }
        }

        static void AskAndClearAllSave()
        {
            const string title = TranslationTable.cck_delete_all_save_data_action;
            if (EditorUtility.DisplayDialog(title, TranslationTable.cck_confirm_delete_all_save_data, TranslationTable.cck_delete_action, TranslationTable.cck_cancel))
            {
                PersistedRoomStateRepository.ClearAll();
                EditorUtility.DisplayDialog(title, TranslationTable.cck_deleted_all_save_data, TranslationTable.cck_ok);
            }
        }

        static VisualElement GenerateLangCodeSection()
        {
            var langSection = EditorUIGenerator.GenerateSection();
            langSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, TranslationTable.cck_server_language_settings));
            var popup = new PopupField<string>(ServerLang.LangCodes.ToList(), ServerLangCodeManager.GetLangCode());
            popup.RegisterValueChangedCallback(ev => ServerLangCodeManager.SetLangCode(ev.newValue));
            langSection.Add(popup);
            return langSection;
        }
    }
}
