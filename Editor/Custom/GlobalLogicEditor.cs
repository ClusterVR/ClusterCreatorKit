using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GlobalLogic), isFallback = true), CanEditMultipleObjects]
    public class GlobalLogicEditor : LogicEditor
    {
    }
}
