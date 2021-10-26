using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(StateKeyStringAttribute), true)]
    public sealed class StateKeyStringAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return CreateStateKeyPropertyGUI(property, property.displayName);
        }

        public static VisualElement CreateStateKeyPropertyGUI(SerializedProperty property, string displayName = "")
        {
            Assert.AreEqual(property.propertyType, SerializedPropertyType.String);
            var container = new VisualElement();

            var propertyDisplayName = property.displayName;
            var keyLengthErrorBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox($"{propertyDisplayName} が長すぎます。",
                    MessageType.Error));

            void SetKeyLengthErrorBoxVisibility(string key)
            {
                keyLengthErrorBox.SetVisibility(key.Length > Constants.TriggerGimmick.MaxKeyLength);
            }

            SetKeyLengthErrorBoxVisibility(property.stringValue);
            var keyField = new TextField(displayName)
            {
                bindingPath = property.propertyPath
            };
            keyField.Bind(property.serializedObject);
            keyField.RegisterCallback<ChangeEvent<string>>(e => SetKeyLengthErrorBoxVisibility(e.newValue));

            container.Add(keyLengthErrorBox);
            container.Add(keyField);
            return container;
        }
    }
}
