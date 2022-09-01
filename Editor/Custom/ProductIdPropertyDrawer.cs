using ClusterVR.CreatorKit.ProductUgc;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(ProductId)), CanEditMultipleObjects]
    public sealed class ProductIdPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var valueProperty = property.FindPropertyRelative("value");
            var valueField = new PropertyField(valueProperty, property.displayName);
            var valueHelpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox($"{valueField.label} に商品Idを指定する必要があります。", MessageType.Warning));

            void SwitchDisplayHelp(string value)
            {
                valueHelpBox.SetVisibility(!ProductId.IsValid(value));
            }

            SwitchDisplayHelp(valueProperty.stringValue);
            valueField.RegisterValueChangeCallback(e => SwitchDisplayHelp(e.changedProperty.stringValue));

            container.Add(valueHelpBox);
            container.Add(valueField);
            return container;
        }
    }
}
