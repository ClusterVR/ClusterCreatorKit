using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(JavaScriptAsset)), CanEditMultipleObjects]
    public sealed class JavaScriptAssetEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var textProperty = serializedObject.FindProperty("text");
            var textField = new TextField
            {
                value = textProperty.stringValue,
                isReadOnly = true
            };
            container.Add(textField);
            return container;
        }
    }
}
