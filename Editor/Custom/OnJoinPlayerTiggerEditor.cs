using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnJoinPlayerTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnJoinPlayerTriggerEditor : TriggerEditor
    {
    }
}
