using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetGameObjectActiveGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetGameObjectActiveGimmickEditor : VisualElementEditor
    {
    }
}
