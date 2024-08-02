using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WorldItemTemplateList), isFallback = true), CanEditMultipleObjects]
    public class WorldItemTemplateListEditor : VisualElementEditor
    {
    }
}
