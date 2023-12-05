using ClusterVR.CreatorKit.World.Implements.TextView;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(TextView)), CanEditMultipleObjects]
    public sealed class TextViewEditor : UnityEditor.Editor
    {
        void OnDestroy()
        {
            foreach (var target in targets)
            {
                if (target is not null && target == null)
                {
                    ((TextView) target).DestroyRenderers();
                }
            }
        }
    }
}
