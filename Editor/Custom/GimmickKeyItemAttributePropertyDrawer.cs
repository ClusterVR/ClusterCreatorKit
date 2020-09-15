using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GimmickKeyItemAttribute), true)]
    public class GimmickKeyItemAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var helpBox = new IMGUIContainer(() => EditorGUILayout.HelpBox($"{nameof(GimmickTarget)} を {nameof(GimmickTarget.Item)} にするには {nameof(Item)} を指定する必要があります。", MessageType.Warning));
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
