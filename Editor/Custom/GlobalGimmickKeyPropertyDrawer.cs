using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GlobalGimmickKey))]
    public class GlobalGimmickKeyPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var keyProperty = property.FindPropertyRelative("key");
            var targetProperty = keyProperty.FindPropertyRelative("target");
            var targetChoices = new List<GimmickTarget> { GimmickTarget.Global, GimmickTarget.Item };
            var targetField = new PopupField<GimmickTarget>("Target", targetChoices, (GimmickTarget)targetProperty.enumValueIndex);
            var keyField = new PropertyField(keyProperty.FindPropertyRelative("key"));

            var itemContainer = CreateItemContainer(property.FindPropertyRelative("item"));

            void SwitchDisplayItem(GimmickTarget target)
            {
                itemContainer.SetVisibility(target == GimmickTarget.Item);
            }

            targetField.RegisterValueChangedCallback(e =>
            {
                targetProperty.enumValueIndex = (int) e.newValue;
                property.serializedObject.ApplyModifiedProperties();
                SwitchDisplayItem(e.newValue);
            });

            SwitchDisplayItem((GimmickTarget) targetProperty.enumValueIndex);

            container.Add(targetField);
            container.Add(keyField);
            container.Add(itemContainer);

            return container;
        }

        static VisualElement CreateItemContainer(SerializedProperty property)
        {
            var container = new VisualElement();

            var helpBox = new IMGUIContainer(() => EditorGUILayout.HelpBox($"{nameof(GimmickTarget)} を {nameof(GimmickTarget.Item)} にするには {nameof(Item)} を指定する必要があります。", MessageType.Warning));
            var itemField = new PropertyField(property);

            itemField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
            {
                SwitchDisplayHelp(e.newValue == null);
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
