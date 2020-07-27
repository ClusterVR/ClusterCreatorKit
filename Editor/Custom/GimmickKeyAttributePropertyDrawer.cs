using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GimmickKeyAttribute), true)]
    public class GimmickKeyAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var keyAttr = (GimmickKeyAttribute) attribute;
            return CreatePropertyGUI(property, keyAttr.TargetSelectables.ToList());
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, List<GimmickTarget> targetChoices)
        {
            var container = new VisualElement();

            var targetProperty = property.FindPropertyRelative("target");
            var targetField = new PopupField<GimmickTarget>("Target", targetChoices, (GimmickTarget)targetProperty.enumValueIndex, FormatListItem, FormatListItem);
            targetField.SetEnabled(targetChoices.Count > 1);
            targetField.RegisterValueChangedCallback(e =>
            {
                targetProperty.enumValueIndex = (int) e.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });

            var keyField = new PropertyField(property.FindPropertyRelative("key"));

            container.Add(targetField);
            container.Add(keyField);

            return container;
        }

        static string FormatListItem(GimmickTarget target)
        {
            switch (target)
            {
                case GimmickTarget.Item:
                    return "This";
                default:
                    return target.ToString();
            }
        }
    }
}
