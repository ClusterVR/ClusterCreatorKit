using ClusterVR.CreatorKit.World.Implements.Localization;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(LocalizationTextures), isFallback = true), CanEditMultipleObjects]
    public class LocalizationTexturesEditor : VisualElementEditor
    {
    }
}
