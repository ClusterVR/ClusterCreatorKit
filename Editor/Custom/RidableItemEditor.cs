using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(RidableItem)), CanEditMultipleObjects]
    public sealed class RidableItemEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            if (!(target is RidableItem ridableItem))
            {
                return;
            }
            MoveAndRotateHandle.Draw(ridableItem.Seat, "Seat");
        }
    }
}
