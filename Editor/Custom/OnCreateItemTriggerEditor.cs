using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnCreateItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnCreateItemTriggerEditor : TriggerEditor
    {
    }
}
