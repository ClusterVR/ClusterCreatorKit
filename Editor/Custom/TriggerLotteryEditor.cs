using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public class TriggerLotteryEditor : VisualElementEditor
    {
        protected override VisualElement CreateField(SerializedProperty serializedProperty)
        {
            if (serializedProperty.isArray && serializedProperty.arrayElementType == "Choice")
            {
                return ReorderableArrayField.CreateReorderableArrayField(serializedProperty);
            }

            return base.CreateField(serializedProperty);
        }
    }
}
