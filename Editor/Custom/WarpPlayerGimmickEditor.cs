using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WarpPlayerGimmick), isFallback = true), CanEditMultipleObjects]
    public class WarpPlayerGimmickEditor : VisualElementEditor
    {
    }
}
