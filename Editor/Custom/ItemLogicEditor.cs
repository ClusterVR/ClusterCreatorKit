using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemLogic), isFallback = true), CanEditMultipleObjects]
    public class ItemLogicEditor : LogicEditor
    {
    }
}
