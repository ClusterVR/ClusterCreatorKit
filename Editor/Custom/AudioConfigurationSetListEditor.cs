using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(AudioConfigurationSetList), isFallback = true), CanEditMultipleObjects]
    public class AudioConfigurationSetListEditor : VisualElementEditor
    {
    }
}
