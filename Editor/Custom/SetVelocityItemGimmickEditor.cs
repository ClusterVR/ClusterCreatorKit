using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetVelocityItemGimmick)), CanEditMultipleObjects]
    public sealed class SetVelocityItemGimmickEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            var parameterTypeProperty = serializedObject.FindProperty("parameterType");
            var spaceField = new PropertyField(serializedObject.FindProperty("space"));
            var velocityField = new PropertyField(serializedObject.FindProperty("velocity"));
            var scaleFactorField = new PropertyField(serializedObject.FindProperty("scaleFactor"));

            void SwitchField(ParameterType parameterType)
            {
                velocityField.SetVisibility(parameterType == ParameterType.Signal);
                scaleFactorField.SetVisibility(parameterType == ParameterType.Vector3);
            }

            var currentParameterType = (ParameterType) parameterTypeProperty.enumValueIndex;
            if (!SetVelocityItemGimmick.SelectableTypes.Contains(currentParameterType))
            {
                currentParameterType = SetVelocityItemGimmick.SelectableTypes[0];
            }

            SwitchField(currentParameterType);
            var parameterTypeField = EnumField.Create(parameterTypeProperty.displayName, parameterTypeProperty,
                SetVelocityItemGimmick.SelectableTypes, currentParameterType, SwitchField);

            container.Add(keyField);
            container.Add(parameterTypeField);
            container.Add(spaceField);
            container.Add(velocityField);
            container.Add(scaleFactorField);

            container.Bind(serializedObject);
            return container;
        }
    }
}
