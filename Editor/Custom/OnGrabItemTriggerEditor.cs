using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnGrabItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnGrabItemTriggerEditor : TriggerEditor
    {
    }
}
