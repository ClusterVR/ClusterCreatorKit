using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World.Implements.WorldRuntimeSetting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WorldRuntimeSetting), isFallback = true)]
    public class WorldRuntimeSettingEditor : VisualElementEditor
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

            var enableCrouchWalkField = FindPropertyField(container, "enableCrouchWalk");
            enableCrouchWalkField.style.marginLeft = 10;
            container.Add(enableCrouchWalkField);

            void UpdateHudTypeCheckbox(bool useClusterHudV2)
            {
                wrts.UseHUDType = WorldRuntimeSetting.BoolToHUDType(useClusterHudV2);
                enableCrouchWalkField.SetEnabled(useClusterHudV2);
            }

            UpdateHudTypeCheckbox(hudTypeCheckbox.value);
            hudTypeCheckbox.RegisterValueChangedCallback(ev =>
                UpdateHudTypeCheckbox(ev.newValue)
                );

            var useCustomClippingPlanesField = FindPropertyField(container, "useCustomClippingPlanes");

            VisualElement ResettableClippingPlaneField(string propertyName, string label, float defaultValue)
            {
                var clippingPlaneField = new VisualElement();
                clippingPlaneField.style.flexDirection = FlexDirection.Row;
                clippingPlaneField.style.marginLeft = 10;

                var distanceField = FindPropertyField(container, propertyName);
                distanceField.style.flexGrow = 1;
                distanceField.label = label;

                var resetButton = new Button();
                resetButton.text = "Reset";
                resetButton.clicked += () =>
                {
                    serializedObject.FindProperty(propertyName).floatValue = defaultValue;
                    serializedObject.ApplyModifiedProperties();
                };

                clippingPlaneField.Add(distanceField);
                clippingPlaneField.Add(resetButton);
                return clippingPlaneField;
            }

            var nearPlaneLabel = $"Near Plane  ({CameraClippingPlanes.NearPlaneMin} - {CameraClippingPlanes.NearPlaneMax})";
            var farPlaneLabel = $"Far Plane  (>= {CameraClippingPlanes.FarPlaneMin})";

            var nearPlaneField = ResettableClippingPlaneField("nearPlane", nearPlaneLabel, WorldRuntimeSetting.DefaultValues.NearPlane);
            var farPlaneField = ResettableClippingPlaneField("farPlane", farPlaneLabel, WorldRuntimeSetting.DefaultValues.FarPlane);

            var initialCustomClippingPlanes = serializedObject.FindProperty("useCustomClippingPlanes").boolValue;
            nearPlaneField.SetVisibility(initialCustomClippingPlanes);
            farPlaneField.SetVisibility(initialCustomClippingPlanes);

            useCustomClippingPlanesField.RegisterValueChangeCallback(ev =>
            {
                var isEnabled = ev.changedProperty.boolValue;
                nearPlaneField.SetVisibility(isEnabled);
                farPlaneField.SetVisibility(isEnabled);
            });

            container.Add(useCustomClippingPlanesField);
            container.Add(nearPlaneField);
            container.Add(farPlaneField);

            var uploadNoticeText = new TextElement()
            {
                text = TranslationTable.cck_component_only_in_cluster_app,
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
