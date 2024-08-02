using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayAudioSourceGimmick), isFallback = true), CanEditMultipleObjects]
    public class PlayAudioSourceGimmickEditor : VisualElementEditor
    {
    }
}
