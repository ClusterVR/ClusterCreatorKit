using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GetOffItemGimmick), isFallback = true), CanEditMultipleObjects]
    public class GetOffItemGimmickEditor : VisualElementEditor
    {
    }
}
