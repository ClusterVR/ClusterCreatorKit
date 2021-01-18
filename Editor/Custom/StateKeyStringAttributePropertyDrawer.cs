using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(StateKeyStringAttribute), true)]
    public sealed class StateKeyStringAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            
            var keyLengthErrorBox = new IMGUIContainer(() => EditorGUILayout.HelpBox($"{property.displayName} は {Constants.TriggerGimmick.MaxKeyLength}文字以下である必要があります。", MessageType.Error));

            void SetKeyLengthErrorBoxVisibility(string key)
            {
                keyLengthErrorBox.SetVisibility(key.Length > Constants.TriggerGimmick.MaxKeyLength);
            }

            SetKeyLengthErrorBoxVisibility(property.stringValue);
            var keyField = new TextField(property.displayName)
            {
                bindingPath = property.propertyPath,
            };
            keyField.Bind(property.serializedObject);
            keyField.RegisterCallback<ChangeEvent<string>>(e => SetKeyLengthErrorBoxVisibility(e.newValue));

            container.Add(keyLengthErrorBox);
            container.Add(keyField);
            return container;
        }
    }
}
