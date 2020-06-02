using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(ItemTriggerAttribute), true)]
    public class ItemTriggerAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var targetChoices = attribute is ItemTriggerAttribute triggerAttr ?
                triggerAttr.TargetSelectables :
                Enum.GetValues(typeof(ItemTriggerTarget)).Cast<ItemTriggerTarget>();
            return CreatePropertyGUI(property, targetChoices.ToList());
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, List<ItemTriggerTarget> targetChoices)
        {
            var container = new VisualElement();

            var targetProperty = property.FindPropertyRelative("target");
            var targetField = new PopupField<ItemTriggerTarget>("Target", targetChoices, (ItemTriggerTarget)targetProperty.enumValueIndex);

            var specifiedTargetItemField = new PropertyField(property.FindPropertyRelative("specifiedTargetItem"));
            void SwitchSpecifiedTargetItemField(ItemTriggerTarget itemTriggerTarget)
            {
                specifiedTargetItemField.SetVisibility(itemTriggerTarget == ItemTriggerTarget.SpecifiedItem);
            }
            SwitchSpecifiedTargetItemField((ItemTriggerTarget) targetProperty.enumValueIndex);

            targetField.RegisterValueChangedCallback(e =>
            {
                targetProperty.enumValueIndex = (int) e.newValue;
                property.serializedObject.ApplyModifiedProperties();
                SwitchSpecifiedTargetItemField(e.newValue);
            });

            var keyField = new PropertyField(property.FindPropertyRelative("key"));
            var typeProperty = property.FindPropertyRelative("type");
            var typeField = new PopupField<string>("Parameter Type") { bindingPath = "type" };

            var valueProperty = property.FindPropertyRelative("value");
            var valueField = new VisualElement();
            var boolValueField = new PropertyField(valueProperty.FindPropertyRelative("boolValue"));
            var floatValueField = new PropertyField(valueProperty.FindPropertyRelative("floatValue"));
            var integerValueField = new PropertyField(valueProperty.FindPropertyRelative("integerValue"));
            valueField.Add(boolValueField);
            valueField.Add(floatValueField);
            valueField.Add(integerValueField);

            void SwitchTriggerValueField(ParameterType parameterType)
            {
                boolValueField.SetVisibility(parameterType == ParameterType.Bool);
                floatValueField.SetVisibility(parameterType == ParameterType.Float);
                integerValueField.SetVisibility(parameterType == ParameterType.Integer);
            }
            typeField.RegisterValueChangedCallback(e =>
            {
                SwitchTriggerValueField((ParameterType) Enum.Parse(typeof(ParameterType), e.newValue));
            });
            SwitchTriggerValueField((ParameterType) typeProperty.enumValueIndex);

            container.Add(targetField);
            container.Add(specifiedTargetItemField);
            container.Add(keyField);
            container.Add(typeField);
            container.Add(valueField);

            return container;
        }
    }
}