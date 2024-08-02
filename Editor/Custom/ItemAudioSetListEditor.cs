using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemAudioSetList), isFallback = true), CanEditMultipleObjects]
    public class ItemAudioSetListEditor : VisualElementEditor
    {
    }
}
