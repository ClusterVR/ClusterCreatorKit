using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public static class UiUtils
    {
        public static VisualElement Separator()
        {
            return new VisualElement
            {
                style =
                {
                    borderBottomWidth = 2,
#if UNITY_2019_3_OR_NEWER
                    borderLeftColor = new StyleColor(Color.gray),
                    borderRightColor = new StyleColor(Color.gray),
                    borderTopColor = new StyleColor(Color.gray),
                    borderBottomColor = new StyleColor(Color.gray),
#else
                    borderColor = new StyleColor(Color.gray),
#endif
                    marginTop = 8,
                    marginBottom = 8
                }
            };
        }
    }
}
