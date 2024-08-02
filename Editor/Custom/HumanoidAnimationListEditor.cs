using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(HumanoidAnimationList), isFallback = true), CanEditMultipleObjects]
    public class HumanoidAnimationListEditor : VisualElementEditor
    {
    }
}
