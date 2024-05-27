using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class ClusterScriptLogConsoleWindow : EditorWindow
    {
        [MenuItem("Cluster/ClusterScript Log Console", priority = 306)]
        static void ShowWindow()
        {
            GetWindow<ClusterScriptLogConsoleWindow>("ClusterScript Log Console").Show();
        }

        const string ScriptLogFileName = "ClusterScriptLog.log";

        #region ログテキスト出力用定義

        [Serializable]
        struct ItemInfo
        {
            [SerializeField] public string name;
            [SerializeField] public ulong id;

            public ItemInfo(string name, ulong id)
            {
                this.name = name;
                this.id = id;
            }
        }

        [Serializable]
        struct OutputScriptableItemLog
        {
            [SerializeField] public long tsdv;
            [SerializeField] public string dvid;
            [SerializeField] public ItemInfo origin;
            [SerializeField] public string type;
            [SerializeField] public string message;

            public OutputScriptableItemLog(
                long tsdv,
                string dvid,
                ItemInfo origin,
                string type,
                string message
            )
            {
                this.tsdv = tsdv;
                this.dvid = dvid;
                this.origin = origin;
                this.type = type;
                this.message = message;
            }
        }

        #endregion

        readonly List<OutputScriptableItemLog> consoleItems = new();
        readonly List<OutputScriptableItemLog> matchedItems = new(); // フィルタリング後のログリスト
        Action refreshLogCount; // ログカウント更新

        enum MatchType
        {
            All,
            Message,
            Item,
        }

        MatchType listViewMatchType = MatchType.All;
        string listViewMatchString = "";

        FileSystemWatcher logFileWatcher;
        long logFilePosition; // ログファイルが更新された際の差分抽出用
        SynchronizationContext mainThread;
        string scriptLogFilePath;

        FileInfo GetScriptLogPath()
        {
#if UNITY_EDITOR_OSX
            return new FileInfo(Path.Combine(Application.persistentDataPath + @"/../../" + @"mu.cluster",ScriptLogFileName));
#else
            return new FileInfo(Path.Combine(Application.persistentDataPath + @"\..\..\" + @"Cluster, Inc_\cluster", ScriptLogFileName));
#endif
        }

        public void OnEnable()
        {
            mainThread = SynchronizationContext.Current;

            var dataPath = GetScriptLogPath();
            scriptLogFilePath = dataPath.FullName;
            logFilePosition = ReadLogFile(scriptLogFilePath, null);

            var listView = BuildListView();
            var toolbar = BuildToolBar(listView);
            rootVisualElement.Add(toolbar);
            rootVisualElement.Add(listView);

            StartLogFileWatcher(scriptLogFilePath, listView);
        }

        public void OnDisable()
        {
            if (logFileWatcher != null)
            {
                logFileWatcher.EnableRaisingEvents = false;
                logFileWatcher.Dispose();
                logFileWatcher = null;
            }
        }

        VisualElement BuildToolBar(ListView listView)
        {
            var toolBar = new Toolbar();
            var style = toolBar.style;
            style.flexDirection = FlexDirection.Row;
            style.height = 26;

            var searchField = BuildSearchField(listView);

            var logCount = new Label($"Log Count {matchedItems.Count}")
            {
                style =
                {
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter)
                }
            };
            refreshLogCount += () => logCount.text = $"Log Count {matchedItems.Count}";


            toolBar.Add(new Button(() =>
            {
                consoleItems.Clear();
                matchedItems.Clear();

                listView.ClearSelection();
                listView.itemsSource = matchedItems;
                listView.RefreshItems();

                searchField.value = "";

                refreshLogCount();
            })
            {
                text = "Clear"
            });

            var pauseToggle = new Toggle
            {
                text = "Pause Log"
            };
            pauseToggle.RegisterValueChangedCallback(evt =>
            {
                if (logFileWatcher != null)
                {
                    logFileWatcher.EnableRaisingEvents = !evt.newValue;
                }
            });

            toolBar.Add(new Button(() =>
            {
                var dataPath = new FileInfo(scriptLogFilePath);
                var path = EditorUtility.OpenFilePanel("Load", dataPath.DirectoryName, "log");
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                consoleItems.Clear();
                logFilePosition = ReadLogFile(path, listView);
                if (logFilePosition > 0)
                {
                    StartLogFileWatcher(path, listView);
                    scriptLogFilePath = path;
                    searchField.value = "";
                }

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
                    if (!File.Exists(scriptLogFilePath))
                    {
                        EditorUtility.DisplayDialog("error", "Log file is not found.", TranslationTable.cck_ok);
                        return;
                    }

                    var dummyFilePath = scriptLogFilePath.Replace(".log", "~.log");
                    File.Copy(scriptLogFilePath, dummyFilePath, true);
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

        List<OutputScriptableItemLog> GetFilterItems()
        {
            return consoleItems.FindAll(item =>
            {
                StringBuilder text = new();
                switch (listViewMatchType)
                {
                    case MatchType.All:
                        text.Append(DateTimeOffset.FromUnixTimeSeconds(item.tsdv).LocalDateTime);
                        text.Append(" ");
                        text.Append(item.dvid);
                        text.Append(" ");
                        text.Append(item.type);
                        text.Append(" ");
                        text.Append(item.message);
                        text.Append(" ");
                        text.Append(item.origin.id);
                        text.Append(" ");
                        text.Append(item.origin.name);
                        break;
                    case MatchType.Message:
                        text.Append(item.message);
                        break;
                    case MatchType.Item:
                        text.Append(item.origin.id);
                        text.Append(" ");
                        text.Append(item.origin.name);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var matchStrings = listViewMatchString.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries);

                var logText = text.ToString();
                foreach (var match in matchStrings)
                {
                    if (!logText.Contains(match, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
                return true;
            });
        }


        ToolbarPopupSearchField BuildSearchField(ListView listView)
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

            void RefreshSearchField()
            {
                var temp = listViewMatchString;
                searchField.value = "";
                searchField.value = temp;
            }

            searchField.menu.AppendAction("All", action =>
            {
                listViewMatchType = MatchType.All;
                RefreshSearchField();
            }, a =>
            {
                return listViewMatchType == MatchType.All ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal;
            });

            searchField.menu.AppendAction("Message", action =>
            {
                listViewMatchType = MatchType.Message;
                RefreshSearchField();
            }, a =>
            {
                return listViewMatchType == MatchType.Message ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal;
            });

            searchField.menu.AppendAction("Item", action =>
            {
                listViewMatchType = MatchType.Item;
                RefreshSearchField();
            }, a =>
            {
                return listViewMatchType == MatchType.Item ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal;
            });

            searchField.RegisterValueChangedCallback(evt =>
            {

                listViewMatchString = evt.newValue;

                if (evt.newValue.Length > 0)
                {
                    matchedItems.Clear();
                    matchedItems.AddRange(GetFilterItems());
                }
                else
                {
                    matchedItems.Clear();
                    matchedItems.AddRange(consoleItems);
                }
                listView.ClearSelection();
                listView.itemsSource = matchedItems;
                listView.RefreshItems();

                refreshLogCount();

            });

            return searchField;
        }

        ListView BuildListView()
        {
            Func<VisualElement> makeItem = () =>
            {
                var item = new VisualElement();
                var style = item.style;
                style.flexDirection = FlexDirection.Row;

                item.Add(new Image());

                var label = new Label();
                label.style.overflow = new StyleEnum<Overflow>(Overflow.Hidden);
                item.Add(label);

                return item;
            };

            Action<VisualElement, int> bindItem = (e, i) =>
            {
                var item = matchedItems[i];

                var label = (Label) e.ElementAt(1);

                var time = DateTimeOffset.FromUnixTimeSeconds(item.tsdv).LocalDateTime;
                label.text = $"[{time}] devid:{item.dvid} item:{item.origin.id} [{item.origin.name}]\n" +
                    $"{item.type,-32} {item.message}";

                var image = (Image) e.ElementAt(0);
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
                else
                {
                    label.style.color = new StyleColor(Color.white);
                    image.image = EditorGUIUtility.IconContent("icons/console.infoicon.png").image;
                }
            };

            const int itemHeight = 32;
            var listView = new ListView(matchedItems, itemHeight, makeItem, bindItem)
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
                    menu.AddItem(new GUIContent("クリップボードにコピー"), false, () => GUIUtility.systemCopyBuffer = label.text);
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

        long ReadLogFile(string filePath, ListView listView, long endPosition = 0L)
        {
            try
            {
                var newPosition = 0L;
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Seek(endPosition, SeekOrigin.Begin);
                    using (var stream = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                    {
                        while (stream.Peek() != -1)
                        {
                            var text = stream.ReadLine();
                            if (text == "")
                            {
                                continue;
                            }

                            try
                            {
                                var item = JsonUtility.FromJson<OutputScriptableItemLog>(text);
                                consoleItems.Add(item);
                            }
                            catch (ArgumentException err)
                            {
                                var item = new OutputScriptableItemLog
                                {
                                    dvid = "",
                                    tsdv = 0,
                                    message = err.Message,
                                    type = "Error"
                                };
                                consoleItems.Add(item);
                                UnityEngine.Debug.LogError(err);
                            }

                            newPosition = fs.Position;
                        }
                    }
                }
                return newPosition;
            }
            catch (Exception err)
            {
                UnityEngine.Debug.LogError(err);
                return endPosition;
            }
            finally
            {
                if (listView != null)
                {
                    matchedItems.Clear();
                    matchedItems.AddRange(GetFilterItems());
                    listView.itemsSource = matchedItems;
                    listView.RefreshItems();
                    listView.ScrollToItem(-1);
                }
                else
                {
                    matchedItems.Clear();
                    matchedItems.AddRange(consoleItems);
                }
                refreshLogCount?.Invoke();
            }
        }

        void StartLogFileWatcher(string filePath, ListView listView)
        {
#if UNITY_EDITOR_OSX
            Environment.SetEnvironmentVariable("MONO_MANAGED_WATCHER", "enabled");
#endif

            if (logFileWatcher != null)
            {
                logFileWatcher.EnableRaisingEvents = false;
                logFileWatcher.Dispose();
                logFileWatcher = null;
            }
            logFileWatcher = new FileSystemWatcher();
            logFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            logFileWatcher.Path = new FileInfo(filePath).DirectoryName;
            logFileWatcher.Filter = ScriptLogFileName;
            logFileWatcher.Changed += (_, _) =>
            {
                mainThread.Post(_ =>
                {
                    logFilePosition = ReadLogFile(filePath, listView, logFilePosition);
                }, null);
            };
            logFileWatcher.Created += (_, _) =>
            {
                mainThread.Post(_ =>
                {
                    consoleItems.Clear();
                    logFilePosition = ReadLogFile(filePath, listView);
                }, null);
            };

            logFileWatcher.EnableRaisingEvents = true;
        }
    }
}
