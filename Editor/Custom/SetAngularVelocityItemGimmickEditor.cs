using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetAngularVelocityItemGimmick)), CanEditMultipleObjects]
    public sealed class SetAngularVelocityItemGimmickEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            var parameterTypeProperty = serializedObject.FindProperty("parameterType");
            var spaceField = new PropertyField(serializedObject.FindProperty("space"));
            var angularVelocityField = new PropertyField(serializedObject.FindProperty("angularVelocity"));
            var scaleFactorField = new PropertyField(serializedObject.FindProperty("scaleFactor"));

            void SwitchField(ParameterType parameterType)
            {
                angularVelocityField.SetVisibility(parameterType == ParameterType.Signal);
                scaleFactorField.SetVisibility(parameterType == ParameterType.Vector3);
            }

            var currentParameterType = (ParameterType) parameterTypeProperty.enumValueIndex;
            if (!SetAngularVelocityItemGimmick.SelectableTypes.Contains(currentParameterType))
            {
                currentParameterType = SetAngularVelocityItemGimmick.SelectableTypes[0];
            }

            SwitchField(currentParameterType);
            var parameterTypeField = EnumField.Create(parameterTypeProperty.displayName, parameterTypeProperty,
                SetAngularVelocityItemGimmick.SelectableTypes, currentParameterType, SwitchField);

            container.Add(keyField);
            container.Add(parameterTypeField);
            container.Add(spaceField);
            container.Add(angularVelocityField);
            container.Add(scaleFactorField);

            container.Bind(serializedObject);
            return container;
        }
    }
}
