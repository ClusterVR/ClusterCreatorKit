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
    [CustomPropertyDrawer(typeof(TriggerParamAttribute), true)]
    public class TriggerParamAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var triggerAttr = (TriggerParamAttribute) attribute;
            return CreatePropertyGUI(property, triggerAttr.TargetSelectables.ToList(), triggerAttr.FormatTarget);
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, List<TriggerTarget> targetChoices, Func<TriggerTarget, string> formatTarget)
        {
            var container = new VisualElement();

            var targetProperty = property.FindPropertyRelative("target");

            var currentTarget = (TriggerTarget) targetProperty.enumValueIndex;
            var selectingTarget = targetChoices.Contains(currentTarget) ? currentTarget : targetChoices[0];
            var specifiedTargetItemField = new PropertyField(property.FindPropertyRelative("specifiedTargetItem"));
            void SwitchSpecifiedTargetItemField(TriggerTarget itemTriggerTarget)
            {
                specifiedTargetItemField.SetVisibility(itemTriggerTarget == TriggerTarget.SpecifiedItem);
            }

            var targetField = EnumField.Create(targetProperty.displayName, targetProperty, targetChoices, selectingTarget, formatTarget, SwitchSpecifiedTargetItemField);
            targetField.SetEnabled(targetChoices.Count > 1);

            SwitchSpecifiedTargetItemField((TriggerTarget) targetProperty.enumValueIndex);

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