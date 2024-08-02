using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(RidableItem), isFallback = true), CanEditMultipleObjects]
    public class RidableItemEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            if (target is not RidableItem ridableItem)
            {
                return;
            }
            MoveAndRotateHandle.Draw(ridableItem.Seat, "Seat");
        }
    }
}
