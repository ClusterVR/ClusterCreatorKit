using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class QuaternionField
    {
        public static VisualElement Create(SerializedProperty property, Action<Quaternion> onValueChanged = null)
        {
            Assert.AreEqual(SerializedPropertyType.Quaternion, property.propertyType);
            return Create(null, property, onValueChanged: onValueChanged);
        }

        public static VisualElement Create(string label, SerializedProperty property, Action<Quaternion> onValueChanged = null)
        {
            Assert.AreEqual(SerializedPropertyType.Quaternion, property.propertyType);
            var defaultValue = property.quaternionValue;

            return Create(label, property, defaultValue, onValueChanged);
        }

        public static VisualElement Create(SerializedProperty property, Quaternion defaultValue, Action<Quaternion> onValueChanged = null)
        {
            Assert.AreEqual(SerializedPropertyType.Quaternion, property.propertyType);
            return Create(null, property, defaultValue, onValueChanged);
        }

        public static VisualElement Create(string label, SerializedProperty property, Quaternion defaultValue, Action<Quaternion> onValueChanged = null)
        {
            Assert.AreEqual(SerializedPropertyType.Quaternion, property.propertyType);

            var container = new VisualElement
            {
                style = { flexGrow = new StyleFloat(1) }
            };

            void UpdateProperty(Quaternion newValue)
            {
                if (property.quaternionValue.Equals(newValue))
                {
                    return;
                }
                property.quaternionValue = newValue;
                property.serializedObject.ApplyModifiedProperties();
            }

            UpdateProperty(defaultValue);

            var visibleField = new Vector3Field(label)
            {
                style = { flexGrow = new StyleFloat(1) },
                value = defaultValue.eulerAngles,
            };

            visibleField.AddToClassList(Vector3Field.alignedFieldUssClassName);
            visibleField.RegisterValueChangedCallback(e =>
            {
                var newValue = Quaternion.Euler(e.newValue);

                UpdateProperty(newValue);
#if !UNITY_2019_3_OR_NEWER
                onValueChanged?.Invoke(newValue);
#endif
                visibleField.SetValueWithoutNotify(newValue.eulerAngles);
            });
            container.Add(visibleField);

            var internalField = new PropertyField(property);
            internalField.Bind(property.serializedObject);
            internalField.RegisterValueChangeCallback(ev =>
            {
                var newValue = ev.changedProperty.quaternionValue;
                visibleField.SetValueWithoutNotify(newValue.eulerAngles);
                onValueChanged?.Invoke(newValue);
            });
            internalField.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            container.Add(internalField);

            return container;
        }
    }
}
