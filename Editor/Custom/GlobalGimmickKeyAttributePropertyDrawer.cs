using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GlobalGimmickKeyAttribute), true)]
    public class GlobalGimmickKeyAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var globalGimmickKeyAttr = (GlobalGimmickKeyAttribute) attribute;
            return CreatePropertyGUI(property, globalGimmickKeyAttr);
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, GlobalGimmickKeyAttribute attribute)
        {
            var container = new VisualElement();

            var keyProperty = property.FindPropertyRelative("key");
            var targetProperty = keyProperty.FindPropertyRelative("target");

            var itemContainer = new PropertyField(property.FindPropertyRelative("item"));
            void SwitchDisplayItem(GimmickTarget target)
            {
                itemContainer.SetVisibility(target == GimmickTarget.Item);
            }
            SwitchDisplayItem((GimmickTarget) targetProperty.enumValueIndex);

            var targetField = EnumField.Create(targetProperty.displayName, targetProperty, attribute.TargetSelectables, (GimmickTarget) targetProperty.enumValueIndex,
                attribute.FormatTarget, SwitchDisplayItem);

            var keyField = new PropertyField(keyProperty.FindPropertyRelative("key"));

            container.Add(targetField);
            container.Add(keyField);
            container.Add(itemContainer);

            return container;
        }
    }
}
