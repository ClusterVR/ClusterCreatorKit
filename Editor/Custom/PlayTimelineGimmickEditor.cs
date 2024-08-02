using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayTimelineGimmick), isFallback = true), CanEditMultipleObjects]
    public class PlayTimelineGimmickEditor : VisualElementEditor
    {
    }
}
