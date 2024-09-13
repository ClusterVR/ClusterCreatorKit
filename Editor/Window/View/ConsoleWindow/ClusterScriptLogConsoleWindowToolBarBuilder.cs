using System;
using System.Diagnostics;
using System.IO;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow
{
    public static class ClusterScriptLogConsoleWindowToolBarBuilder
    {
        public static VisualElement BuildToolBar(ListView listView, ClusterScriptLogConsoleWindowModel model)
        {
            var toolBar = new Toolbar();
            var style = toolBar.style;
            style.flexDirection = FlexDirection.Row;
            style.height = 26;

            var searchField = ClusterScriptLogConsoleWindowSearchFieldBuilder.BuildSearchField(listView, model);

            var logCount = new Label($"Log Count {model.MatchedItems.Count}")
            {
                style =
                {
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter)
                }
            };
            model.RefreshLogCount += () => logCount.text = $"Log Count {model.MatchedItems.Count}";


            toolBar.Add(new Button(() =>
            {
                model.ClearLogs(listView);
                searchField.value = "";
            })
            {
                text = "Clear"
            });

            var pauseToggle = new Toggle
            {
                text = "Pause Log"
            };
            pauseToggle.RegisterValueChangedCallback(evt => model.SetPaused(evt.newValue));

            toolBar.Add(new Button(() =>
            {
                var dataPath = new FileInfo(model.ScriptLogFilePath);
                var path = EditorUtility.OpenFilePanel("Load", dataPath.DirectoryName, "log");
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                model.LoadAndWatchNewFile(path, listView);
                searchField.value = "";
                pauseToggle.value = false;
            })
            {
                text = "Load"
            });

            toolBar.Add(searchField);

            toolBar.Add(pauseToggle);

            toolBar.Add(new Button(() =>
            {
                try
                {
                    if (!File.Exists(model.ScriptLogFilePath))
                    {
                        EditorUtility.DisplayDialog("error", "Log file is not found.", TranslationTable.cck_ok);
                        return;
                    }

                    var dummyFilePath = model.ScriptLogFilePath.Replace(".log", "~.log");
                    File.Copy(model.ScriptLogFilePath, dummyFilePath, true);
                    if (File.Exists(dummyFilePath))
                    {
                        Process.Start(dummyFilePath);
                    }
                }
                catch (Exception err)
                {
                    UnityEngine.Debug.LogError(err);
                }
            })
            {
                text = "Open Log"
            });

            toolBar.Add(logCount);

            return toolBar;
        }
    }
}
