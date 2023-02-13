using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(IdStringAttribute), true)]
    public sealed class IdStringAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return CreateIdStringPropertyGUI(property, property.displayName);
        }

        static VisualElement CreateIdStringPropertyGUI(SerializedProperty property, string displayName = "")
        {
            Assert.AreEqual(property.propertyType, SerializedPropertyType.String);
            var container = new VisualElement();

            var propertyDisplayName = property.displayName;

            var characterTypeErrorBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox($"{propertyDisplayName} に使用できない文字が含まれています。{propertyDisplayName} には英数字とアポストロフィ・カンマ・ハイフン・ピリオド・アンダースコアのみが使用可能です。",
                    MessageType.Error));

            var idLengthErrorBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox($"{propertyDisplayName} が長すぎます。 最大値: {Constants.Component.MaxIdLength}",
                    MessageType.Error));
            void SetErrorBoxVisibility(string id)
            {
                characterTypeErrorBox.SetVisibility(!Constants.Component.ValidIdCharactersRegex.IsMatch(id));
                idLengthErrorBox.SetVisibility(id.Length > Constants.Component.MaxIdLength);
            }

            SetErrorBoxVisibility(property.stringValue);
            var idField = new TextField(displayName)
            {
                bindingPath = property.propertyPath
            };
            idField.Bind(property.serializedObject);
            idField.RegisterCallback<ChangeEvent<string>>(e => SetErrorBoxVisibility(e.newValue));

            container.Add(characterTypeErrorBox);
            container.Add(idLengthErrorBox);
            container.Add(idField);
            return container;
        }
    }
}
