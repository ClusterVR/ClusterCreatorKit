using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(TriggerParamAttribute), true)]
    public class TriggerParamAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var triggerAttr = (TriggerParamAttribute) attribute;
            return CreatePropertyGUI(property, triggerAttr.TargetSelectables.ToList(), triggerAttr.FormatTarget,
                triggerAttr.ValueLabelText);
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, List<TriggerTarget> targetChoices,
            Func<TriggerTarget, string> formatTarget, string valueLabelText)
        {
            var container = new VisualElement
            {
                style =
                {
                    marginTop = new StyleLength(1),
                    marginBottom = new StyleLength(1),
                    marginLeft = new StyleLength(3)
                }
            };

            container.Add(CreateTargetPropertyGUI(property, targetChoices, formatTarget));
            container.Add(CreateValuePropertyGUI(property, valueLabelText));

            return container;
        }

        static VisualElement CreateTargetPropertyGUI(SerializedProperty property, List<TriggerTarget> targetChoices,
            Func<TriggerTarget, string> formatTarget)
        {
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                }
            };

            container.Add(new Label("Target")
            {
                style =
                {
                    paddingTop = new StyleLength(2),
                    paddingRight = new StyleLength(2),
                    paddingLeft = new StyleLength(0f),
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft)
                }
            });

            var specifiedTargetItemProperty = property.FindPropertyRelative("specifiedTargetItem");
            var specifiedTargetItemField = new ObjectField
            {
                objectType = typeof(Item.Implements.Item),
                bindingPath = specifiedTargetItemProperty.propertyPath
            };
            specifiedTargetItemField.Bind(specifiedTargetItemProperty.serializedObject);

            void SwitchSpecifiedTargetItemField(TriggerTarget itemTriggerTarget)
            {
                specifiedTargetItemField.SetVisibility(itemTriggerTarget == TriggerTarget.SpecifiedItem);
            }

            var targetProperty = property.FindPropertyRelative("target");
            var currentTarget = (TriggerTarget) targetProperty.enumValueIndex;
            var selectingTarget = targetChoices.Contains(currentTarget) ? currentTarget : targetChoices[0];
            var targetField = EnumField.Create(targetProperty, targetChoices, selectingTarget, formatTarget,
                SwitchSpecifiedTargetItemField);
            targetField.SetEnabled(targetChoices.Count > 1);

            SwitchSpecifiedTargetItemField((TriggerTarget) targetProperty.enumValueIndex);

            var keyProperty = property.FindPropertyRelative("key");
            Assert.AreEqual(keyProperty.propertyType, SerializedPropertyType.String);
            var keyField = StateKeyStringAttributePropertyDrawer.CreateStateKeyPropertyGUI(keyProperty);
            keyField.style.flexGrow = new StyleFloat(9);

            var vertical = new VisualElement
            {
                style =
                {
                    flexGrow = new StyleFloat(1)
                }
            };
            container.Add(vertical);
            var horizontal = new VisualElement
            {
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                }
            };
            vertical.Add(horizontal);
            horizontal.Add(targetField);
            horizontal.Add(keyField);
            vertical.Add(specifiedTargetItemField);

            return container;
        }

        static VisualElement CreateValuePropertyGUI(SerializedProperty property, string valueLabelText)
        {
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                }
            };

            container.Add(new Label(string.IsNullOrEmpty(valueLabelText) ? "Value" : valueLabelText)
            {
                style =
                {
                    paddingTop = new StyleLength(2),
                    paddingRight = new StyleLength(2),
                    paddingLeft = new StyleLength(0f),
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft)
                }
            });

            var boolValueProperty = property.FindPropertyRelative("value.boolValue");
            Assert.AreEqual(boolValueProperty.propertyType, SerializedPropertyType.Boolean);
            var boolValueField = new Toggle
            {
                bindingPath = boolValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            boolValueField.Bind(boolValueProperty.serializedObject);
            container.Add(boolValueField);

            var floatValueProperty = property.FindPropertyRelative("value.floatValue");
            Assert.AreEqual(floatValueProperty.propertyType, SerializedPropertyType.Float);
            var floatValueField = new FloatField
            {
                bindingPath = floatValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            floatValueField.Bind(floatValueProperty.serializedObject);
            container.Add(floatValueField);

            var integerValueProperty = property.FindPropertyRelative("value.integerValue");
            Assert.AreEqual(integerValueProperty.propertyType, SerializedPropertyType.Integer);
            var integerValueField = new IntegerField
            {
                bindingPath = integerValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            integerValueField.Bind(integerValueProperty.serializedObject);
            container.Add(integerValueField);

            var typeProperty = property.FindPropertyRelative("type");
            var typeField = EnumField.Create<ParameterType>(typeProperty, SwitchTriggerValueField);
            container.Insert(1, typeField);

            void SwitchTriggerValueField(ParameterType type)
            {
                boolValueField.SetVisibility(type == ParameterType.Bool);
                floatValueField.SetVisibility(type == ParameterType.Float);
                integerValueField.SetVisibility(type == ParameterType.Integer);
            }

            SwitchTriggerValueField((ParameterType) typeProperty.enumValueIndex);

            return container;
        }
    }
}
