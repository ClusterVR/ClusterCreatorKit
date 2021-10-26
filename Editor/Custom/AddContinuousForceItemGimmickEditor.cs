using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(AddContinuousForceItemGimmick)), CanEditMultipleObjects]
    public sealed class AddContinuousForceItemGimmickEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            var parameterTypeProperty = serializedObject.FindProperty("parameterType");
            var spaceField = new PropertyField(serializedObject.FindProperty("space"));
            var forceField = new PropertyField(serializedObject.FindProperty("force"));
            var scaleFactorField = new PropertyField(serializedObject.FindProperty("scaleFactor"));
            var ignoreMassField = new PropertyField(serializedObject.FindProperty("ignoreMass"));

            void SwitchField(ParameterType parameterType)
            {
                var isVector = parameterType == ParameterType.Vector3;
                forceField.SetVisibility(!isVector);
                scaleFactorField.SetVisibility(isVector);
            }

            var currentParameterType = (ParameterType) parameterTypeProperty.enumValueIndex;
            if (!AddContinuousForceItemGimmick.SelectableTypes.Contains(currentParameterType))
            {
                currentParameterType = AddContinuousForceItemGimmick.SelectableTypes[0];
            }

            SwitchField(currentParameterType);
            var parameterTypeField = EnumField.Create(parameterTypeProperty.displayName, parameterTypeProperty,
                AddContinuousForceItemGimmick.SelectableTypes, currentParameterType, SwitchField);

            container.Add(keyField);
            container.Add(parameterTypeField);
            container.Add(spaceField);
            container.Add(forceField);
            container.Add(scaleFactorField);
            container.Add(ignoreMassField);

            container.Bind(serializedObject);
            return container;
        }
    }
}
