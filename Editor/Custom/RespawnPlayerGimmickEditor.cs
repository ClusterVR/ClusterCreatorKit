using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(RespawnPlayerGimmick), isFallback = true), CanEditMultipleObjects]
    public class RespawnPlayerGimmickEditor : VisualElementEditor
    {
    }
}
