using ClusterVR.CreatorKit.Translation;
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
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_invalid_characters, propertyDisplayName, propertyDisplayName),
                    MessageType.Error));

            var idLengthErrorBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_id_too_long, propertyDisplayName, Constants.Component.MaxIdLength),
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
