using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemMaterialSetList), isFallback = true), CanEditMultipleObjects]
    public class ItemMaterialSetListEditor : VisualElementEditor
    {
    }
}
