using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(OnAngularVelocityChangedItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class OnAngularVelocityChangedItemTriggerEditor : TriggerEditor
    {
    }
}
