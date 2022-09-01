using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ProductDisplayItem)), CanEditMultipleObjects]
    public sealed class ProductDisplayItemEditor : VisualElementEditor
    {
        void OnSceneGUI()
        {
            if (target is not ProductDisplayItem productDisplayItem)
            {
                return;
            }
            MoveAndRotateHandle.Draw(productDisplayItem.ProductDisplayRoot, "ProductDisplayRoot");
        }
    }
}
