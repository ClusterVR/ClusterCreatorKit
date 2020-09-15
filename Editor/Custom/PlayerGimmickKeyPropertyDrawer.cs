using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(PlayerGimmickKey), true)]
    public class PlayerGimmickKeyPropertyDrawer : PropertyDrawer
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
            var targetField = EnumField.Create(targetProperty.displayName, targetProperty, new List<GimmickTarget> {GimmickTarget.Player, GimmickTarget.Global}, (GimmickTarget)targetProperty.enumValueIndex, SwitchDisplayItem);

            var keyField = new PropertyField(keyProperty);

            container.Add(targetField);
            container.Add(keyField);
            container.Add(itemContainer);

            return container;
        }
    }
}
