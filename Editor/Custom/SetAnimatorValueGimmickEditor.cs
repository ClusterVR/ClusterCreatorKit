using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetAnimatorValueGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetAnimatorValueGimmickEditor : VisualElementEditor
    {
    }
}
