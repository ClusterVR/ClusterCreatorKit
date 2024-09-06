using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerLocalObjectReferenceList), isFallback = true), CanEditMultipleObjects]
    public class PlayerLocalObjectReferenceListEditor : VisualElementEditor
    {
    }
}
