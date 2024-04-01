using System.Linq;
using ClusterVR.CreatorKit.Validator;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public abstract class VisualElementEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var iterator = serializedObject.GetIterator();
            if (!iterator.NextVisible(true))
            {
                return container;
            }

            while (iterator.NextVisible(false))
            {
                var propertyField = CreateField(iterator.Copy());

                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                {
                    propertyField.SetEnabled(false);
                }

                container.Add(propertyField);
            }

            container.Bind(serializedObject);

            ComponentValidationMessage(container);

            return container;
        }

        protected virtual VisualElement CreateField(SerializedProperty serializedProperty)
        {
            return serializedProperty.propertyType switch
            {
                SerializedPropertyType.Quaternion => QuaternionField.Create(serializedProperty.displayName, serializedProperty),
                _ => new PropertyField(serializedProperty) { name = "PropertyField:" + serializedProperty.propertyPath },
            };
        }

        protected PropertyField FindPropertyField(VisualElement container, string propertyName)
        {
            return container.Children()
                .OfType<PropertyField>()
                .First(c => c.bindingPath == propertyName);
        }

        void ComponentValidationMessage(VisualElement container)
        {
            var warningContainer = new IMGUIContainer(() =>
            {
                foreach (var obj in targets)
                {
                    if (obj is MonoBehaviour component)
                    {
                        var attr = (ComponentValidatorAttribute[]) component.GetType().GetCustomAttributes(typeof(ComponentValidatorAttribute), true);
                        foreach (var validator in attr)
                        {
                            if (!validator.Validate(component, out UnityEditor.MessageType type, out string message))
                            {
                                EditorGUILayout.HelpBox(message, type);
                                return;
                            }
                        }
                    }
                }
            });
            container.Add(warningContainer);
        }
    }
}
