using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GlobalTimer), isFallback = true), CanEditMultipleObjects]
    public class GlobalTimerEditor : TriggerEditor
    {
    }
}
