using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class VisualElementExtensions
    {
        static readonly StyleEnum<DisplayStyle> styleFlex = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        static readonly StyleEnum<DisplayStyle> styleNone = new StyleEnum<DisplayStyle>(DisplayStyle.None);

        public static void SetVisibility(this VisualElement element, bool visible)
        {
            element.style.display = visible ? styleFlex : styleNone;
        }
    }
}
