using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WarpItemGimmick)), CanEditMultipleObjects]
    public sealed class WarpItemGimmickEditor : VisualElementEditor
    {
        void OnSceneGUI()
        {
            if (!(target is WarpItemGimmick warpItemGimmick)) return;
            MoveAndRotateHandle.Draw(warpItemGimmick.TargetTransform, "TargetTransform");
        }
    }
}