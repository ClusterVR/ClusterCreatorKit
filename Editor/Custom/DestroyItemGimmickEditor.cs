using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(DestroyItemGimmick), isFallback = true), CanEditMultipleObjects]
    public class DestroyItemGimmickEditor : VisualElementEditor
    {
    }
}
