using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using ValueType = ClusterVR.CreatorKit.Operation.ValueType;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(LogicAttribute), true)]
    public sealed class LogicAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var logicAttr = (LogicAttribute) attribute;
            return CreatePropertyGUI(property, logicAttr);
        }

        static VisualElement CreatePropertyGUI(SerializedProperty property, LogicAttribute attribute)
        {
            Assert.AreEqual(property.type, nameof(Logic));
            var container = new VisualElement();

            var statementsProperty = property.FindPropertyRelative("statements");
            var statementsField = CreateStatementsPropertyGUI(statementsProperty, attribute);
            container.Add(statementsField);

            return container;
        }

        static VisualElement CreateStatementsPropertyGUI(SerializedProperty property, LogicAttribute attribute)
        {
            Assert.IsTrue(property.isArray);
            Assert.AreEqual(property.arrayElementType, nameof(Statement));
            var container = new VisualElement();
            var list = new VisualElement
            {
                style = { marginTop = new StyleLength(5) }
            };
            container.Add(list);

            var arraySizeProperty = property.FindPropertyRelative("Array.size");
            var arraySizeField = new IntegerField
            {
                bindingPath = arraySizeProperty.propertyPath,
                style = { display = new StyleEnum<DisplayStyle>(DisplayStyle.None) }
            };
            arraySizeField.RegisterValueChangedCallback(e =>
            {
                Refresh();
            });
            container.Add(arraySizeField);

            void AddArrayElement()
            {
                property.arraySize += 1;
                property.serializedObject.ApplyModifiedProperties();
            }

            void DeleteArrayElementAt(int i)
            {
                if (property.GetArrayElementAtIndex(i) != null)
                    property.DeleteArrayElementAtIndex(i);
                property.serializedObject.ApplyModifiedProperties();
            }

            void MoveArrayElement(int srcIndex, int dstIndex)
            {
                if (property.GetArrayElementAtIndex(srcIndex) != null)
                    property.MoveArrayElement(srcIndex, dstIndex);
                property.serializedObject.ApplyModifiedProperties();
            }

            VisualElement CreateCell(int i)
            {
                var listItem = new VisualElement
                {
                    style =
                    {
                        flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                        borderTopWidth = new StyleFloat(1),
#if UNITY_2019_3_OR_NEWER
                        borderTopColor = new StyleColor(Color.gray),
#else
                        borderColor = new StyleColor(Color.gray),
#endif
                        paddingTop = new StyleLength(5),
                        paddingBottom = new StyleLength(5)
                    }
                };

                var statementProperty = property.GetArrayElementAtIndex(i);
                var field = CreateStatementPropertyGUI(statementProperty, attribute);
                listItem.Add(field);

                var listItemMenu = new VisualElement
                {
                    style =
                    {
                        borderLeftWidth = new StyleFloat(1),
#if UNITY_2019_3_OR_NEWER
                        borderLeftColor = new StyleColor(Color.gray),
#else
                        borderColor = new StyleColor(Color.gray),
#endif
                    }
                };
                listItem.Add(listItemMenu);
                var moveUpButton = new Button(() => MoveArrayElement(i, i - 1))
                {
                    text = "▲",
                    tooltip = "Move Up"
                };
                moveUpButton.SetEnabled(i > 0);
                listItemMenu.Add(moveUpButton);

                var removeButton = new Button(() => DeleteArrayElementAt(i))
                {
                    text = "-",
                    tooltip = "Delete"
                };
                listItemMenu.Add(removeButton);

                var moveDownButton = new Button(() => MoveArrayElement(i, i + 1))
                {
                    text = "▼",
                    tooltip = "Move Down"
                };
                moveDownButton.SetEnabled(i < property.arraySize - 1);
                listItemMenu.Add(moveDownButton);

                return listItem;
            }

            void Refresh()
            {
                list.Unbind();
                list.Clear();
                for (var i = 0; i < property.arraySize; i++)
                {
                    list.Add(CreateCell(i));
                }
                var addButton = new Button(AddArrayElement)
                {
                    text = "+",
                    tooltip = "Add"
                };
                list.Add(addButton);
            }
            Refresh();

            return container;
        }

        static VisualElement CreateStatementPropertyGUI(SerializedProperty property, LogicAttribute attribute)
        {
            Assert.AreEqual(property.type, nameof(Statement));
            var container = new VisualElement
            {
                style = { flexGrow = new StyleFloat(1) }
            };

            var targetStateProperty = property.FindPropertyRelative("singleStatement.targetState");
            var statementsField = CreateTargetStatePropertyGUI(targetStateProperty, attribute.TargetStateTargetSelectables, attribute.FormatTargetStateTarget);
            container.Add(statementsField);

            var expressionProperty = property.FindPropertyRelative("singleStatement.expression");
            var expressionField = CreateExpressionPropertyGUI(expressionProperty, attribute.SourceStateTargetSelectables, attribute.FormatSourceTarget);
            container.Add(expressionField);

            return container;
        }

        static VisualElement CreateTargetStatePropertyGUI(SerializedProperty property, List<TargetStateTarget> targetChoices, Func<TargetStateTarget, string> formatTarget)
        {
            Assert.AreEqual(property.type, nameof(TargetState));
            Assert.IsTrue(targetChoices.Count > 0);
            var container = new VisualElement
            {
                style = { flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row) }
            };

            var targetProperty = property.FindPropertyRelative("target");
            var currentTarget = (TargetStateTarget) targetProperty.intValue;
            var selectingTarget = targetChoices.Contains(currentTarget) ? currentTarget : targetChoices[0];
            var targetField = EnumField.Create(targetProperty, targetChoices, selectingTarget, formatTarget);
            container.Add(targetField);

            var keyProperty = property.FindPropertyRelative("key");
            Assert.AreEqual(keyProperty.propertyType, SerializedPropertyType.String);
            var keyField = new TextField
            {
                bindingPath = keyProperty.propertyPath,
                style = { flexGrow = new StyleFloat(4) }
            };
            keyField.Bind(keyProperty.serializedObject);
            container.Add(keyField);

            var parameterTypeProperty = property.FindPropertyRelative("parameterType");
            var parameterTypeField = EnumField.Create<ParameterType>(parameterTypeProperty);
            container.Add(parameterTypeField);

            return container;
        }

        static VisualElement CreateExpressionPropertyGUI(SerializedProperty property,
            List<GimmickTarget> targetChoices,
            Func<GimmickTarget, string> formatTarget,
            int depth = 0)
        {
            Assert.AreEqual(property.type, nameof(Expression));
            var container = new VisualElement
            {
                style = { marginLeft = new StyleLength(5) }
            };

            var valueProperty = property.FindPropertyRelative("value");
            var valueField = CreateValuePropertyGUI(valueProperty, targetChoices, formatTarget);
            container.Add(valueField);

            if (depth > 0)
            {
                return container;
            }

            valueField.style.marginLeft = new StyleLength(5);

            var typeProperty = property.FindPropertyRelative("type");
            var currentType = (ExpressionType) typeProperty.intValue;
            var operatorExpressionProperty = property.FindPropertyRelative("operatorExpression");
            var operatorProperty = operatorExpressionProperty.FindPropertyRelative("operator");
            var currentOperator = (Operator) operatorProperty.intValue;
            var operandsProperty = operatorExpressionProperty.FindPropertyRelative("operands");

            var operandsContainer = new VisualElement();
            container.Add(operandsContainer);

            const int ExpressionTypeValue = -1;
            var typeOperatorChoices = Enum.GetValues(typeof(Operator)).Cast<int>().Prepend(ExpressionTypeValue).ToList();
            var typeOperatorDefaultIndex = typeOperatorChoices.IndexOf(currentType == ExpressionType.Value ? ExpressionTypeValue : (int) currentOperator);
            var typeOperatorField = new PopupField<int>(typeOperatorChoices, typeOperatorDefaultIndex, TypeOperatorFormat, TypeOperatorFormat);
            string TypeOperatorFormat(int value)
            {
                return value == ExpressionTypeValue ? "=" : $"= {(Operator) value}";
            }
            typeOperatorField.RegisterValueChangedCallback(e =>
            {
                var type = e.newValue == ExpressionTypeValue ? ExpressionType.Value : ExpressionType.OperatorExpression;

                if (typeProperty.intValue != (int) type)
                {
                    typeProperty.intValue = (int) type;
                    typeProperty.serializedObject.ApplyModifiedProperties();

                    var operandsFirstProperty = operandsProperty.GetArrayElementAtIndex(0);
                    switch (type)
                    {
                        case ExpressionType.Value:
                            if (operandsFirstProperty != null)
                            {
                                CopyValueProperty(operandsFirstProperty.FindPropertyRelative("value"), valueProperty);
                            }
                            break;
                        case ExpressionType.OperatorExpression:
                            if (operandsFirstProperty == null)
                            {
                                operandsProperty.InsertArrayElementAtIndex(0);
                            }
                            CopyValueProperty(valueProperty, operandsFirstProperty.FindPropertyRelative("value"));
                            break;
                    }
#if !UNITY_2019_3_OR_NEWER
                    OnTypeChanged(type);
#endif
                }

                if (type == ExpressionType.OperatorExpression && operatorProperty.intValue != e.newValue)
                {
                    operatorProperty.intValue = e.newValue;
                    operatorProperty.serializedObject.ApplyModifiedProperties();
#if !UNITY_2019_3_OR_NEWER
                    OnOperatorChanged((Operator)e.newValue);
#endif
                }
            });
            container.Insert(0, typeOperatorField);

            var typeField = EnumField.CreateAsStringPopupField<ExpressionType>(typeProperty, newValue =>
            {
                typeOperatorField.SetValueWithoutNotify(newValue == ExpressionType.Value ? ExpressionTypeValue : operatorProperty.intValue);
                OnTypeChanged(newValue);
            });
            typeField.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            container.Add(typeField);

            var operatorField = EnumField.CreateAsStringPopupField<Operator>(operatorProperty, newValue =>
            {
                typeOperatorField.SetValueWithoutNotify(typeProperty.intValue == (int) ExpressionType.Value ? ExpressionTypeValue : (int) newValue);
                OnOperatorChanged(newValue);
            });
            operatorField.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            container.Add(operatorField);

            void OnOperatorChanged(Operator @operator)
            {
                operandsContainer.Unbind();
                operandsContainer.Clear();

                var requiredLength = @operator.GetRequiredLength();
                if (requiredLength != operandsProperty.arraySize)
                {
                    operandsProperty.arraySize = requiredLength;
                    operandsProperty.serializedObject.ApplyModifiedProperties();
                }

                var operandsField = CreateOperandsPropertyGUI(operandsProperty, targetChoices, formatTarget, depth);
                operandsContainer.Add(operandsField);
            }
            OnOperatorChanged(currentOperator);

            void OnTypeChanged(ExpressionType type)
            {
                valueField.SetVisibility(type == ExpressionType.Value);
                operandsContainer.SetVisibility(type == ExpressionType.OperatorExpression);
            }
            OnTypeChanged(currentType);

            return container;
        }

        static void CopyValueProperty(SerializedProperty source, SerializedProperty target)
        {
            Assert.AreEqual(source.type, nameof(Value));
            Assert.AreEqual(target.type, nameof(Value));
            target.FindPropertyRelative("type").intValue = source.FindPropertyRelative("type").intValue;
            target.FindPropertyRelative("constant.type").intValue = source.FindPropertyRelative("constant.type").intValue;
            target.FindPropertyRelative("constant.boolValue").boolValue = source.FindPropertyRelative("constant.boolValue").boolValue;
            target.FindPropertyRelative("constant.floatValue").floatValue = source.FindPropertyRelative("constant.floatValue").floatValue;
            target.FindPropertyRelative("constant.integerValue").intValue = source.FindPropertyRelative("constant.integerValue").intValue;
            target.FindPropertyRelative("sourceState.target").intValue = source.FindPropertyRelative("sourceState.target").intValue;
            target.FindPropertyRelative("sourceState.key").stringValue = source.FindPropertyRelative("sourceState.key").stringValue;
            target.serializedObject.ApplyModifiedProperties();
        }

        static VisualElement CreateValuePropertyGUI(SerializedProperty property,
            List<GimmickTarget> targetChoices,
            Func<GimmickTarget, string> formatTarget)
        {
            Assert.AreEqual(property.type, nameof(Value));
            var container = new VisualElement
            {
                style = { flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row) }
            };

            var constantProperty = property.FindPropertyRelative("constant");
            var constantField = CreateConstantValuePropertyGUI(constantProperty);
            container.Add(constantField);

            var sourceStateProperty = property.FindPropertyRelative("sourceState");
            var sourceStateField = CreateSourceStatePropertyGUI(sourceStateProperty, targetChoices, formatTarget);
            container.Add(sourceStateField);

            var typeProperty = property.FindPropertyRelative("type");
            var typeField = EnumField.Create<ValueType>(typeProperty, OnTypeChanged);
            container.Insert(0, typeField);

            void OnTypeChanged(ValueType type)
            {
                constantField.SetVisibility(type == ValueType.Constant);
                sourceStateField.SetVisibility(type == ValueType.RoomState);
            }
            OnTypeChanged((ValueType) typeProperty.intValue);

            return container;
        }

        static VisualElement CreateConstantValuePropertyGUI(SerializedProperty property)
        {
            Assert.AreEqual(property.type, nameof(ConstantValue));
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    flexGrow = new StyleFloat(9)
                }
            };

            var boolValueProperty = property.FindPropertyRelative("boolValue");
            Assert.AreEqual(boolValueProperty.propertyType, SerializedPropertyType.Boolean);
            var boolValueField = new Toggle
            {
                bindingPath = boolValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            boolValueField.Bind(boolValueProperty.serializedObject);
            container.Add(boolValueField);

            var floatValueProperty = property.FindPropertyRelative("floatValue");
            Assert.AreEqual(floatValueProperty.propertyType, SerializedPropertyType.Float);
            var floatValueField = new FloatField
            {
                bindingPath = floatValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            floatValueField.Bind(floatValueProperty.serializedObject);
            container.Add(floatValueField);

            var integerValueProperty = property.FindPropertyRelative("integerValue");
            Assert.AreEqual(integerValueProperty.propertyType, SerializedPropertyType.Integer);
            var integerValueField = new IntegerField
            {
                bindingPath = integerValueProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            integerValueField.Bind(integerValueProperty.serializedObject);
            container.Add(integerValueField);

            var typeProperty = property.FindPropertyRelative("type");
            var typeChoices = new List<ParameterType> { ParameterType.Bool, ParameterType.Float, ParameterType.Integer };
            var currentType = (ParameterType) typeProperty.intValue;
            var selectingTarget = typeChoices.Contains(currentType) ? currentType : typeChoices[0];
            var typeField = EnumField.Create(typeProperty, typeChoices, selectingTarget, SwitchField);
            container.Insert(0, typeField);

            void SwitchField(ParameterType type)
            {
                boolValueField.SetVisibility(type == ParameterType.Bool);
                floatValueField.SetVisibility(type == ParameterType.Float);
                integerValueField.SetVisibility(type == ParameterType.Integer);
            }
            SwitchField(selectingTarget);

            return container;
        }

        static VisualElement CreateSourceStatePropertyGUI(SerializedProperty property,
            List<GimmickTarget> targetChoices,
            Func<GimmickTarget, string> formatTarget)
        {
            Assert.AreEqual(property.type, nameof(SourceState));
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    flexGrow = new StyleFloat(9)
                }
            };

            var targetProperty = property.FindPropertyRelative("target");
            var currentTarget = (GimmickTarget) targetProperty.intValue;
            var selectingTarget = targetChoices.Contains(currentTarget) ? currentTarget : targetChoices[0];
            var targetField = EnumField.Create(targetProperty, targetChoices, selectingTarget, formatTarget);
            container.Add(targetField);

            var keyProperty = property.FindPropertyRelative("key");
            Assert.AreEqual(keyProperty.propertyType, SerializedPropertyType.String);
            var keyField = new TextField
            {
                bindingPath = keyProperty.propertyPath,
                style = { flexGrow = new StyleFloat(9) }
            };
            keyField.Bind(keyProperty.serializedObject);
            container.Add(keyField);

            return container;
        }

        static VisualElement CreateOperandsPropertyGUI(SerializedProperty property,
            List<GimmickTarget> targetChoices,
            Func<GimmickTarget, string> formatTarget,
            int depth)
        {
            Assert.IsTrue(property.isArray);
            Assert.AreEqual(property.arrayElementType, nameof(Expression));
            var container = new VisualElement();
            for (var i = 0; i < property.arraySize; i++)
            {
                var field = CreateExpressionPropertyGUI(property.GetArrayElementAtIndex(i), targetChoices, formatTarget, depth + 1);
                container.Add(field);
            }
            return container;
        }
    }
}