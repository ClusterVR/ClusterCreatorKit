using ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WorldRuntimeSetting))]
    public sealed class WorldRuntimeSettingEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var hudcheckbox = false;
            var container = base.CreateInspectorGUI();

            var useMovingPlatformField =
                FindPropertyField(container, "useMovingPlatform");
            var movingPlatformHorizontalInertiaField =
                FindPropertyField(container, "movingPlatformHorizontalInertia");
            var movingPlatformVerticalInertiaField =
                FindPropertyField(container, "movingPlatformVerticalInertia");
            var hudTypeField =
                FindPropertyField(container, "useHUDType");

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
            hudTypeField.SetVisibility(false);
            var wrts = target as WorldRuntimeSetting;
            if (wrts == null)
            {
                return container;
            }
            var hudTypeCheckbox = new Toggle("Use Cluster HUD v2");
            hudTypeCheckbox.AddToClassList(Toggle.alignedFieldUssClassName);
            hudTypeCheckbox.value = WorldRuntimeSetting.HUDTypeToBool(wrts.UseHUDType);
            container.Add(hudTypeCheckbox);

            void UpdateHudTypeCheckbox(bool useClusterHudV2)
            {
                wrts.UseHUDType = WorldRuntimeSetting.BoolToHUDType(useClusterHudV2);
            }

            UpdateHudTypeCheckbox(hudTypeCheckbox.value);
            hudTypeCheckbox.RegisterValueChangedCallback(ev =>
                UpdateHudTypeCheckbox(ev.newValue)
                );


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
