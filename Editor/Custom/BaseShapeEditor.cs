using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(BaseShape), true, isFallback = true), CanEditMultipleObjects]
    public class BaseShapeEditor : VisualElementEditor
    {
    }
}
