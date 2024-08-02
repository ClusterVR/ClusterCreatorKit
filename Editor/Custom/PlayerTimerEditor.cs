using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerTimer), isFallback = true), CanEditMultipleObjects]
    public class PlayerTimerEditor : TriggerEditor
    {
    }
}
