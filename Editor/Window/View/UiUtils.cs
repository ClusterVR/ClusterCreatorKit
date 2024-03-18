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

        public static void IMGUISeparator()
        {
            var style = new GUIStyle
            {
                normal =
                {
                    background = Texture2D.grayTexture
                },
                margin =
                {
                    top = 8,
                    bottom = 8
                },
                fixedHeight = 2
            };
            GUILayout.Box(GUIContent.none, style);
        }
    }
}
