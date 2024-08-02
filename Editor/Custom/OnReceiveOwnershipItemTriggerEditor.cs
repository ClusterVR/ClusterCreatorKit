using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnReceiveOwnershipItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnReceiveOwnershipItemTriggerEditor : TriggerEditor
    {
    }
}
