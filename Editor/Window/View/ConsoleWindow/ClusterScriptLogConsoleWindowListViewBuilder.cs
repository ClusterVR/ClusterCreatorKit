using System;
using System.Collections;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public static class ClusterScriptLogConsoleWindowListViewBuilder
    {
        public static ListView BuildListView(IList<OutputScriptableItemLog> matchedItems)
        {
            Func<VisualElement> makeItem = ClusterScriptLogConsoleWindowListViewEntryBuilder.Make;

            Action<VisualElement, int> bindItem = (e, i) => ClusterScriptLogConsoleWindowListViewEntryBuilder.Bind(e, matchedItems[i]);

            const int itemHeight = 32;
            var listView = new ListView((IList) matchedItems, itemHeight, makeItem, bindItem)
            {
                selectionType = SelectionType.Single,
                showAlternatingRowBackgrounds = AlternatingRowBackground.All,
                showBorder = true,
                style =
                {
                    flexGrow = 1.0f
                }
            };

            listView.RegisterCallback<MouseDownEvent>((evt) =>
            {
                if (listView.selectedIndex == -1)
                {
                    return;
                }

                if (Event.current.button == 1)
                {
                    var item = listView.GetRootElementForIndex(listView.selectedIndex);
                    var label = (Label) item.ElementAt(1);

                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent(TranslationTable.cck_copy_to_clipboard), false, () => GUIUtility.systemCopyBuffer = label.text);
                    menu.ShowAsContext();
                }
            });

            listView.RegisterCallback<KeyDownEvent>((evt) =>
            {
                if (listView.selectedIndex == -1)
                {
                    return;
                }
#if UNITY_EDITOR_OSX
                if (Event.current.command && Event.current.keyCode == KeyCode.C)
#else
                if (Event.current.control && Event.current.keyCode == KeyCode.C)
#endif
                {
                    var item = listView.GetRootElementForIndex(listView.selectedIndex);
                    var label = (Label) item.ElementAt(1);
                    GUIUtility.systemCopyBuffer = label.text;
                }
            });

            return listView;
        }
    }
}
