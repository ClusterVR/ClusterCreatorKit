using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(IsGroundedCharacterItemTrigger), isFallback = true), CanEditMultipleObjects]
    public class IsGroundedCharacterItemTriggerEditor : TriggerEditor
    {
    }
}
