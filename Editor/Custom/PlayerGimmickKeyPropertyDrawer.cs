using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(PlayerGimmickKey), true)]
    public sealed class PlayerGimmickKeyPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var keyProperty = property.FindPropertyRelative("key");
            var targetProperty = property.FindPropertyRelative("target");
            var itemProperty = property.FindPropertyRelative("item");

            var itemContainer = new PropertyField(itemProperty);

            void SwitchDisplayItem(GimmickTarget target)
            {
                itemContainer.SetVisibility(target == GimmickTarget.Item);
            }

            SwitchDisplayItem((GimmickTarget) targetProperty.enumValueIndex);

            var targetField =
                EnumField.Create<GimmickTarget>(targetProperty.displayName, targetProperty, onValueChanged: SwitchDisplayItem);

            var keyField = new PropertyField(keyProperty);

            container.Add(targetField);
            container.Add(keyField);
            container.Add(itemContainer);

            return container;
        }
    }
}
