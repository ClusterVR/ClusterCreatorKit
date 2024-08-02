using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(UseItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class UseItemTriggerEditor : TriggerEditor
    {
    }
}
