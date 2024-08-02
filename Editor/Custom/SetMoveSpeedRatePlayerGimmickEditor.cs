using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetMoveSpeedRatePlayerGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetMoveSpeedRatePlayerGimmickEditor : VisualElementEditor
    {
    }
}
