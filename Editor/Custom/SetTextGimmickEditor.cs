using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetTextGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetTextGimmickEditor : VisualElementEditor
    {
    }
}
