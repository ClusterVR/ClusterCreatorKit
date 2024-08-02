using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnGetOffItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnGetOffItemTriggerEditor : TriggerEditor
    {
    }
}
