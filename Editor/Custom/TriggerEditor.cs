using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public abstract class TriggerEditor : VisualElementEditor
    {
        protected override VisualElement CreateField(SerializedProperty serializedProperty)
        {
            if (serializedProperty.isArray &&
                (serializedProperty.arrayElementType == nameof(ConstantTriggerParam) || serializedProperty.arrayElementType == nameof(VariableTriggerParam)))
            {
                return ReorderableArrayField.CreateReorderableArrayField(serializedProperty);
            }

            return base.CreateField(serializedProperty);
        }
    }
}
