using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(InteractItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class InteractItemTriggerEditor : TriggerEditor
    {
    }
}
