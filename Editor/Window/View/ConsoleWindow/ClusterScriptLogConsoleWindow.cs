using System.Threading;
using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow
{
    public sealed class ClusterScriptLogConsoleWindow : EditorWindow
    {
        [MenuItem("Cluster/ClusterScript Log Console", priority = 340)]
        static void ShowWindow()
        {
            GetWindow<ClusterScriptLogConsoleWindow>("ClusterScript Log Console").Show();
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_ScriptLogConsole);
        }

        readonly ClusterScriptLogConsoleWindowModel model = new();

        public void OnEnable()
        {
            model.SetMainThreadContext(SynchronizationContext.Current);

            var listView = ClusterScriptLogConsoleWindowListViewBuilder.BuildListView(model.MatchedItems);
            var toolbar = ClusterScriptLogConsoleWindowToolBarBuilder.BuildToolBar(listView, model);
            rootVisualElement.Add(toolbar);
            rootVisualElement.Add(listView);

            model.LoadAndWatchDefaultLogFile(listView);
        }

        public void OnDisable()
        {
            model.StopLogFileWatcher();
        }

        public void OnDestroy()
        {
            model.Dispose();
        }
    }
}
