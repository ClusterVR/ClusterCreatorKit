using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SteerItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class SteerItemTriggerEditor : TriggerEditor
    {
    }
}
