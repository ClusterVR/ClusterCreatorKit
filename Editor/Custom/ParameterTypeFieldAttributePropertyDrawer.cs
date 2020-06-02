using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(ParameterTypeFieldAttribute))]
    public class ParameterTypeFieldAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (!(attribute is ParameterTypeFieldAttribute attr))
            {
                return new PropertyField(property);
            }

            var field = new PopupField<ParameterType>(property.displayName, attr.Selectables.ToList(), (ParameterType) property.enumValueIndex);
            field.Bind(property.serializedObject);
            field.RegisterValueChangedCallback(e =>
            {
                property.enumValueIndex = (int) e.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });

            return field;
        }
    }
}