using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(InitializePlayerTrigger), isFallback = true), CanEditMultipleObjects]
    public class InitializePlayerTriggerEditor : TriggerEditor
    {
    }
}
