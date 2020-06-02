using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GrabbableItem)), CanEditMultipleObjects]
    public sealed class GrabbableItemEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            if (!(target is GrabbableItem grabbableItem)) return;
            MoveAndRotateHandle.Draw(grabbableItem.Grip, "Grip");
        }
    }
}