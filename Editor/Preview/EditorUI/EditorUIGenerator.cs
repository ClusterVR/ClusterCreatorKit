using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public class EditorUIGenerator : MonoBehaviour
    {
        static readonly Dictionary<LabelType, int> dictLabelAndFontSize = new Dictionary<LabelType, int>
        {
            {LabelType.h1, 18},
            {LabelType.h2, 14}
        };

        public static VisualElement GenerateSection()
        {
            return new VisualElement
            {
                style =
                {
                    paddingTop = 10,
                    paddingLeft = 10,
                    paddingRight = 10,
                    paddingBottom = 10,
                    flexShrink = 0
                }
            };
        }

        public static Label GenerateLabel(LabelType labelType, string content)
        {
            var label = new Label(content);
            label.style.fontSize = dictLabelAndFontSize[labelType];
            return label;
        }

        public static Slider GenerateSlider(LabelType labelType, string content)
        {
            var slider = new Slider(content);
            slider.style.fontSize = dictLabelAndFontSize[labelType];
            return slider;
        }

        public static Toggle GenerateToggle(LabelType labelType, string content)
        {
            var toggle = new Toggle(content);
            toggle.style.fontSize = dictLabelAndFontSize[labelType];
            return toggle;
        }
    }

    public enum LabelType
    {
        h1,
        h2
    }
}
