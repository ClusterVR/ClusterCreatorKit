using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(AddContinuousTorqueItemGimmick)), CanEditMultipleObjects]
    public sealed class AddContinuousTorqueItemGimmickEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            var parameterTypeProperty = serializedObject.FindProperty("parameterType");
            var spaceField = new PropertyField(serializedObject.FindProperty("space"));
            var torqueField = new PropertyField(serializedObject.FindProperty("torque"));
            var scaleFactorField = new PropertyField(serializedObject.FindProperty("scaleFactor"));
            var ignoreMassField = new PropertyField(serializedObject.FindProperty("ignoreMass"));

            void SwitchField(ParameterType parameterType)
            {
                var isVector = parameterType == ParameterType.Vector3;
                torqueField.SetVisibility(!isVector);
                scaleFactorField.SetVisibility(isVector);
            }

            var currentParameterType = (ParameterType) parameterTypeProperty.enumValueIndex;
            if (!AddContinuousTorqueItemGimmick.SelectableTypes.Contains(currentParameterType))
            {
                currentParameterType = AddContinuousTorqueItemGimmick.SelectableTypes[0];
            }

            SwitchField(currentParameterType);
            var parameterTypeField = EnumField.Create(parameterTypeProperty.displayName, parameterTypeProperty,
                AddContinuousTorqueItemGimmick.SelectableTypes, currentParameterType, SwitchField);

            container.Add(keyField);
            container.Add(parameterTypeField);
            container.Add(spaceField);
            container.Add(torqueField);
            container.Add(scaleFactorField);
            container.Add(ignoreMassField);

            container.Bind(serializedObject);
            return container;
        }
    }
}
