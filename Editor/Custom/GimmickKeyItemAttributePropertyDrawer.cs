using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GimmickKeyItemAttribute), true)]
    public sealed class GimmickKeyItemAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var helpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(
                    TranslationUtility.GetMessage(TranslationTable.cck_gimmick_target_item_requires_item, nameof(GimmickTarget), nameof(GimmickTarget.Item), nameof(Item)),
                    MessageType.Warning));
            var itemField = new ObjectField(property.displayName)
            {
                objectType = typeof(Item.Implements.Item),
                value = property.objectReferenceValue
            };
            itemField.Bind(property.serializedObject);

            itemField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
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
            container.Add(itemField);

            return container;
        }
    }
}
