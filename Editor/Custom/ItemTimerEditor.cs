using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemTimer), isFallback = true), CanEditMultipleObjects]
    public class ItemTimerEditor : TriggerEditor
    {
    }
}
