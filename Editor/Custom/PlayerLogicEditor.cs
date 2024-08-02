using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerLogic), isFallback = true), CanEditMultipleObjects]
    public class PlayerLogicEditor : LogicEditor
    {
    }
}
