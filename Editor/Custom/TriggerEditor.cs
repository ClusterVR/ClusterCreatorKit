using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public class TriggerEditor : VisualElementEditor
    {
        protected override VisualElement CreateField(SerializedProperty serializedProperty)
        {
            if (serializedProperty.isArray && serializedProperty.arrayElementType == nameof(TriggerParam))
            {
                return TriggerParamArrayField.CreateTriggerParamArrayField(serializedProperty);
            }
            return base.CreateField(serializedProperty);
        }
    }
}
