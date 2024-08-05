using ClusterVR.CreatorKit.Editor.Extensions;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ScriptableItem), isFallback = true), CanEditMultipleObjects]
    public class ScriptableItemEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var sourceCodeAssetProperty = serializedObject.FindProperty("sourceCodeAsset");
            var sourceCodeAssetField = new PropertyField(sourceCodeAssetProperty);
            container.Add(sourceCodeAssetField);

            var sourceCodeField = new PropertyField(serializedObject.FindProperty("sourceCode"));
            container.Add(sourceCodeField);

            void UpdateSourceCodeFieldVisibility(Object sourceCodeAssetObject)
            {
                sourceCodeField.SetVisibility(sourceCodeAssetObject == null);
            }
            UpdateSourceCodeFieldVisibility(sourceCodeAssetProperty.objectReferenceValue);
            sourceCodeAssetField.RegisterValueChangeCallback(e =>
                UpdateSourceCodeFieldVisibility(e.changedProperty.objectReferenceValue));

            container.Bind(serializedObject);
            return container;
        }
    }
}
