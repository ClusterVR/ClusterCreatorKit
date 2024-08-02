using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetJumpHeightRatePlayerGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetJumpHeightRatePlayerGimmickEditor : VisualElementEditor
    {
    }
}
