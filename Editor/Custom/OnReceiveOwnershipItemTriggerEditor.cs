using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnReceiveOwnershipItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnReceiveOwnershipItemTriggerEditor : TriggerEditor
    {
        protected override VisualElement CreateField(SerializedProperty serializedProperty)
        {
            if (serializedProperty.propertyPath == "eventType")
            {
                if (!serializedProperty.hasMultipleDifferentValues)
                {
                    NormalizeEventType(serializedProperty);
                }

                return new UnityEditor.UIElements.EnumField(
                    serializedProperty.displayName)
                {
                    bindingPath = serializedProperty.propertyPath
                };
            }

            return base.CreateField(serializedProperty);
        }

        void NormalizeEventType(SerializedProperty serializedProperty)
        {
            var value = (OnReceiveOwnershipItemTrigger.EventType) serializedProperty.enumValueFlag;
            if (value.HasFlag(OnReceiveOwnershipItemTrigger.EventType.Voluntary))
            {
                if (value.HasFlag(OnReceiveOwnershipItemTrigger.EventType.Involuntary))
                {
                    serializedProperty.enumValueFlag = (int) OnReceiveOwnershipItemTrigger.EventType.Always;
                }
                else
                {
                    serializedProperty.enumValueFlag = (int) OnReceiveOwnershipItemTrigger.EventType.Voluntary;
                }
            }
            else if (value.HasFlag(OnReceiveOwnershipItemTrigger.EventType.Involuntary))
            {
                serializedProperty.enumValueFlag = (int) OnReceiveOwnershipItemTrigger.EventType.Involuntary;
            }
        }
    }
}
