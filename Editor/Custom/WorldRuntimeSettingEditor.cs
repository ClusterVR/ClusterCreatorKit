using ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WorldRuntimeSetting)), CanEditMultipleObjects]
    public sealed class WorldRuntimeSettingEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();

            var useMovingPlatformField =
                FindPropertyField(container, "useMovingPlatform");
            var movingPlatformHorizontalInertiaField =
                FindPropertyField(container, "movingPlatformHorizontalInertia");
            var movingPlatformVerticalInertiaField =
                FindPropertyField(container, "movingPlatformVerticalInertia");

            movingPlatformHorizontalInertiaField.style.marginLeft = 10;
            movingPlatformVerticalInertiaField.style.marginLeft = 10;

            void UpdateMovingPlatformDetailSettingVisibility(bool useMovingPlatformEnabled)
            {
                movingPlatformHorizontalInertiaField.SetVisibility(useMovingPlatformEnabled);
                movingPlatformVerticalInertiaField.SetVisibility(useMovingPlatformEnabled);
            }

            UpdateMovingPlatformDetailSettingVisibility(
                serializedObject.FindProperty("useMovingPlatform").boolValue
                );
            useMovingPlatformField.RegisterValueChangeCallback(ev =>
                UpdateMovingPlatformDetailSettingVisibility(ev.changedProperty.boolValue)
                );

            var useMantlingField = FindPropertyField(container, "useMantling");
            useMantlingField.label = "Use Clambering";

            var uploadNoticeText = new TextElement()
            {
                text = "*このコンポーネントの指定値はclusterアプリでのみ反映されます。\nワールドをアップロードして挙動を確認してください。",
                style =
                {
                    marginTop = 10,
                    unityFontStyleAndWeight = FontStyle.Bold,
                }
            };
            container.Add(uploadNoticeText);

            return container;
        }
    }
}
