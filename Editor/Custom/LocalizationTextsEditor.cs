using ClusterVR.CreatorKit.World.Implements.Localization;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(LocalizationTexts), isFallback = true), CanEditMultipleObjects]
    public class LocalizationTextsEditor : VisualElementEditor
    {
    }
}
