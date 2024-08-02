using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnReleaseItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnReleaseItemTriggerEditor : TriggerEditor
    {
    }
}
