using ClusterVR.CreatorKit.Editor.Custom;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Preview.PlayerController;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public class SettingsWindow : EditorWindow
    {
        [MenuItem("Cluster/Preview/Settings", priority = 114)]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent("Preview Settings");
        }

        public void OnEnable()
        {
            var root = rootVisualElement;
            root.Add(GenerateCameraControlSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateSavedStateSection());
        }

        static VisualElement GenerateCameraControlSection()
        {
            var cameraControlSection = EditorUIGenerator.GenerateSection();
            cameraControlSection.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, "視点操作オプション"));

            var sensitivitySlider = EditorUIGenerator.GenerateSlider(LabelType.h2, "感度");
            sensitivitySlider.lowValue = 0f;
            sensitivitySlider.highValue = 1f;
            sensitivitySlider.value = CameraControlSettings.Sensitivity;
            sensitivitySlider.RegisterValueChangedCallback(ev => CameraControlSettings.Sensitivity = ev.newValue);
            cameraControlSection.Add(sensitivitySlider);

            var invertVerticalToggle = EditorUIGenerator.GenerateToggle(LabelType.h2, "上下反転");
            invertVerticalToggle.value = CameraControlSettings.InvertVertical;
            invertVerticalToggle.RegisterValueChangedCallback(ev => CameraControlSettings.InvertVertical = ev.newValue);
            cameraControlSection.Add(invertVerticalToggle);

            var invertHorizontalToggle = EditorUIGenerator.GenerateToggle(LabelType.h2, "左右反転");
            invertHorizontalToggle.value = CameraControlSettings.InvertHorizontal;
            invertHorizontalToggle.RegisterValueChangedCallback(ev =>
                CameraControlSettings.InvertHorizontal = ev.newValue);
            cameraControlSection.Add(invertHorizontalToggle);

            return cameraControlSection;
        }

        static VisualElement GenerateSavedStateSection()
        {
            var section = EditorUIGenerator.GenerateSection();
            section.Add(EditorUIGenerator.GenerateLabel(LabelType.h1, "セーブ機能"));

            var informationBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox($"プレビューでのセーブデータ及びその操作はプレビューのみに利用され、アップロードされたワールドに影響はありません。", MessageType.Info));
            section.Add(informationBox);

            var playingHelpBox =
                new IMGUIContainer(() => EditorGUILayout.HelpBox($"セーブデータの操作は再生中には使用できません", MessageType.Warning));
            section.Add(playingHelpBox);

            var staticContents = new VisualElement();
            section.Add(staticContents);

            var clearThisButton =
                EditorUIGenerator.GenerateButton(LabelType.h2, "現在のシーンのセーブデータを削除する", AskAndClearActiveScene);
            staticContents.Add(clearThisButton);

            var clearAllButton = EditorUIGenerator.GenerateButton(LabelType.h2, "全てのセーブデータを削除する", AskAndClearAllSave);
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
            const string title = "セーブデータの削除";
            var activeScene = SceneManager.GetActiveScene();
            var activeSceneGuid = AssetDatabase.AssetPathToGUID(activeScene.path);
            if (!PersistedRoomStateRepository.IsSaved(activeSceneGuid))
            {
                EditorUtility.DisplayDialog(title, "現在のシーンのセーブデータはありません", "OK");
                return;
            }

            if (EditorUtility.DisplayDialog(title, "現在のシーンのセーブデータを削除します。よろしいですか？", "削除する", "キャンセル"))
            {
                PersistedRoomStateRepository.Clear(activeSceneGuid);
                EditorUtility.DisplayDialog(title, "現在のシーンのセーブデータを削除しました。", "OK");
            }
        }

        static void AskAndClearAllSave()
        {
            const string title = "全てのセーブデータの削除";
            if (EditorUtility.DisplayDialog(title, "全てのセーブデータを削除します。よろしいですか？", "削除する", "キャンセル"))
            {
                PersistedRoomStateRepository.ClearAll();
                EditorUtility.DisplayDialog(title, "全てのセーブデータを削除しました。", "OK");
            }
        }
    }
}
