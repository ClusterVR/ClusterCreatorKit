using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(StopTimelineGimmick), isFallback = true), CanEditMultipleObjects]
    public class StopTimelineGimmickEditor : VisualElementEditor
    {
    }
}
