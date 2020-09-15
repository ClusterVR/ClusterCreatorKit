using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class EnumField
    {
        public static VisualElement Create<TEnum>(SerializedProperty property, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            return Create(null, property, onValueChanged);
        }

        public static VisualElement Create<TEnum>(string label, SerializedProperty property, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            var defaultValue = (TEnum) (object) property.intValue;
            var choices = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

            return Create(label, property, choices, defaultValue, null, onValueChanged);
        }

        public static VisualElement Create<TEnum>(SerializedProperty property, List<TEnum> choices, TEnum defaultValue, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            return Create(null, property, choices, defaultValue, onValueChanged);
        }

        public static VisualElement Create<TEnum>(string label, SerializedProperty property, List<TEnum> choices, TEnum defaultValue, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            string Format(TEnum e)
            {
                return e.ToString();
            }

            return Create(label, property, choices, defaultValue, Format, onValueChanged);
        }

        public static VisualElement Create<TEnum>(SerializedProperty property, List<TEnum> choices, TEnum defaultValue,
            Func<TEnum, string> format, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            return Create(null, property, choices, defaultValue, format, onValueChanged);
        }

        public static VisualElement Create<TEnum>(string label, SerializedProperty property, List<TEnum> choices, TEnum defaultValue, Func<TEnum, string> format, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            // Enum の SerializedProperty type が int になることがある(深さによって発生する)
            //Assert.AreEqual(property.propertyType, SerializedPropertyType.Enum);
            var container = new VisualElement
            {
                style = { flexGrow = new StyleFloat(1) }
            };

            void UpdateProperty(TEnum value)
            {
                var newValue = (int)(object) value;
                if (property.intValue == newValue) return;
                property.intValue = newValue;
                property.serializedObject.ApplyModifiedProperties();
            }
            UpdateProperty(defaultValue);

            var popupField = new PopupField<TEnum>(label, choices, defaultValue, format, format)
            {
                style = { flexGrow = new StyleFloat(1) }
            };
            popupField.RegisterValueChangedCallback(e =>
            {
                UpdateProperty(e.newValue);
#if !UNITY_2019_3_OR_NEWER
                onValueChanged?.Invoke(e.newValue);
#endif
            });
            popupField.SetEnabled(choices.Count > 1);
            container.Add(popupField);

            VisualElement CreateFieldAsEnum()
            {
                Assert.AreEqual(property.propertyType, SerializedPropertyType.Enum);
                var enumField = CreateAsStringPopupField<TEnum>(property, newValue =>
                {
                    popupField.SetValueWithoutNotify(newValue);
                    onValueChanged?.Invoke(newValue);
                });
                return enumField;
            }

            VisualElement CreateFieldAsInt()
            {
                Assert.AreEqual(property.propertyType, SerializedPropertyType.Integer);
                var intField = new IntegerField
                {
                    bindingPath = property.propertyPath
                };
                intField.Bind(property.serializedObject);
                intField.RegisterValueChangedCallback(e =>
                {
                    var newValue = (TEnum)(object) e.newValue;
                    popupField.SetValueWithoutNotify(newValue);
                    onValueChanged?.Invoke(newValue);
                });
                return intField;
            }

            VisualElement CreateFieldByType()
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Enum:
                    {
                        return CreateFieldAsEnum();
                    }
                    // HACK: Enum の SerializedProperty type が int になることがある(深さによって発生する)
                    case SerializedPropertyType.Integer:
                    {
                        return CreateFieldAsInt();
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var field = CreateFieldByType();
            field.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            container.Add(field);

            return container;
        }

        public static VisualElement CreateAsStringPopupField<TEnum>(SerializedProperty property, Action<TEnum> onValueChanged = null)
            where TEnum : struct, Enum
        {
            Assert.AreEqual(property.propertyType, SerializedPropertyType.Enum);
#if UNITY_2019_3_OR_NEWER
            var enumField = new UnityEditor.UIElements.EnumField
            {
                bindingPath = property.propertyPath
            };
            enumField.RegisterValueChangedCallback(e =>
            {
                if (onValueChanged != null && e.newValue is TEnum newValue)
                {
                    onValueChanged.Invoke(newValue);
                }
            });
            enumField.Bind(property.serializedObject);
            return enumField;
#else
            // HACK: `Field type UnityEditor.UIElements.EnumField is not compatible with Enum property "myEnum"` in 2019.2
            // ref: https://forum.unity.com/threads/cant-create-bindings-for-an-enum-not-compatible.728111/#post-4873661
            var enumField = new PopupField<string>
            {
                bindingPath = property.propertyPath
            };
            // HACK: `NullReferenceException: Object reference not set to an instance of an object` in 2019.2
            enumField.RegisterValueChangedCallback(_ =>
            {
                onValueChanged?.Invoke((TEnum)(object) enumField.index);
            });
            return enumField;
#endif
        }
    }
}