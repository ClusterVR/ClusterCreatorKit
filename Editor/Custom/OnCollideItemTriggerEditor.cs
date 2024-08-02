using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnCollideItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnCollideItemTriggerEditor : TriggerEditor
    {
    }
}
