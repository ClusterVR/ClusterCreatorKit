using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnVelocityChangedItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnVelocityChangedItemTriggerEditor : TriggerEditor
    {
    }
}
