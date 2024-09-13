using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow
{
    public static class ClusterScriptLogConsoleWindowSearchFieldBuilder
    {
        public static ToolbarPopupSearchField BuildSearchField(ListView listView, ClusterScriptLogConsoleWindowModel model)
        {
            var searchField = new ToolbarPopupSearchField
            {
                style =
                {
                    width = 340,
                    backgroundImage = new StyleBackground()
                },
                focusable = true
            };

            searchField.menu.AppendAction("All",
                _ => model.SetListViewMatchType(MatchType.All, listView),
                _ => model.ListViewMatchType == MatchType.All ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);

            searchField.menu.AppendAction("Message",
                _ => model.SetListViewMatchType(MatchType.Message, listView),
                _ => model.ListViewMatchType == MatchType.Message ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);

            searchField.menu.AppendAction("Item",
                _ => model.SetListViewMatchType(MatchType.Item, listView),
                _ => model.ListViewMatchType == MatchType.Item ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal);

            searchField.RegisterValueChangedCallback(evt => model.SetListViewMatchString(evt.newValue, listView));

            return searchField;
        }
    }
}
