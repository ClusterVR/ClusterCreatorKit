using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GrabbableItem), isFallback = true), CanEditMultipleObjects]
    public class GrabbableItemEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            if (!(target is GrabbableItem grabbableItem))
            {
                return;
            }
            MoveAndRotateHandle.Draw(grabbableItem.Grip, "Grip");
        }
    }
}
