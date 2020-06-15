using ClusterVR.CreatorKit.Preview.PlayerController;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public class SettingsWindow : EditorWindow
    {
        [MenuItem("Cluster/Preview/Settings")]
        public static void ShowWindow()
        {
            var window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent("SettingsWindow");
        }

        public void OnEnable()
        {
            var root = rootVisualElement;
            root.Add(GenerateCameraControlSection());
            root.Add(UiUtils.Separator());
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
            invertHorizontalToggle.RegisterValueChangedCallback(ev => CameraControlSettings.InvertHorizontal = ev.newValue);
            cameraControlSection.Add(invertHorizontalToggle);

            return cameraControlSection;
        }
    }
}