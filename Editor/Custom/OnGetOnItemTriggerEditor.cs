using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnGetOnItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnGetOnItemTriggerEditor : TriggerEditor
    {
    }
}
