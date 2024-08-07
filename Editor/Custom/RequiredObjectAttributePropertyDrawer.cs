using ClusterVR.CreatorKit.Editor.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(RequiredObjectAttribute), true)]
    public class RequiredObjectAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var helpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_need_property_specification, property.displayName), MessageType.Warning));
            var objectField = new ObjectField(property.displayName)
            {
                objectType = ((RequiredObjectAttribute) attribute).PropertyType,
                value = property.objectReferenceValue
            };
            objectField.Bind(property.serializedObject);

            objectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
            {
                property.objectReferenceValue = e.newValue;
                SwitchDisplayHelp(e.newValue == null);
                property.serializedObject.ApplyModifiedProperties();
            });

            void SwitchDisplayHelp(bool show)
            {
                helpBox.SetVisibility(show);
            }

            SwitchDisplayHelp(property.objectReferenceValue == null);

            container.Add(helpBox);
            container.Add(objectField);

            return container;
        }
    }
}
