using System;
using ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public static class ClusterScriptLogConsoleWindowListViewEntryBuilder
    {
        public static VisualElement Make()
        {
            var item = new VisualElement();
            var style = item.style;
            style.flexDirection = FlexDirection.Row;

            item.Add(new Image());

            var label = new Label();
            label.style.overflow = new StyleEnum<Overflow>(Overflow.Hidden);
            item.Add(label);

            return item;
        }

        public static void Bind(VisualElement element, OutputScriptableItemLog item)
        {
            var label = (Label) element.ElementAt(1);

            label.text = item.BuildLabelString();

            var image = (Image) element.ElementAt(0);
            image.style.flexShrink = 0;

            if (item.type.Contains("Error"))
            {
                label.style.color = new StyleColor(Color.red);
                image.image = EditorGUIUtility.IconContent("icons/d_console.erroricon.png").image;
            }
            else if (item.type.Contains("Warn"))
            {
                label.style.color = new StyleColor(Color.yellow);
                image.image = EditorGUIUtility.IconContent("icons/console.warnicon.png").image;
            }
            else if (item.type.Contains("Dropped"))
            {
                label.style.color = new StyleColor(Color.gray);
                image.image = EditorGUIUtility.IconContent("icons/console.infoicon.png").image;
            }
            else
            {
                label.style.color = new StyleColor(Color.white);
                image.image = EditorGUIUtility.IconContent("icons/console.infoicon.png").image;
            }
        }
    }
}
