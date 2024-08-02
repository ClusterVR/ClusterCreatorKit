using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetWheelColliderSteerAngleItemGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetWheelColliderSteerAngleItemGimmickEditor : VisualElementEditor
    {
    }
}
