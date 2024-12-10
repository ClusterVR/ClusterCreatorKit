using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View.ConsoleWindow
{
    public sealed class ClusterScriptLogConsoleWindowModel
    {
        const string ScriptLogFileName = "ClusterScriptLog.log";
        static readonly string[] WhiteSpaces = { " ", "　" };

        readonly List<OutputScriptableItemLog> consoleItems = new();
        public readonly List<OutputScriptableItemLog> MatchedItems = new();
        public event Action RefreshLogCount;

        public MatchType ListViewMatchType { get; private set; } = MatchType.All;
        string listViewMatchString = "";

        public string ScriptLogFilePath { get; private set; }
        FileSystemWatcher logFileWatcher;
        long logFilePosition;
        SynchronizationContext mainThread;

        string GetDefaultScriptLogFilePath()
        {
#if UNITY_EDITOR_OSX
            return new FileInfo(Path.Combine(Application.persistentDataPath + @"/../../" + @"mu.cluster", ScriptLogFileName)).FullName;
#else
            return new FileInfo(Path.Combine(Application.persistentDataPath + @"\..\..\" + @"Cluster, Inc_\cluster", ScriptLogFileName)).FullName;
#endif
        }

        public void SetMainThreadContext(SynchronizationContext context)
        {
            mainThread = context;
        }

        public void SetListViewMatchType(MatchType matchType, ListView listView)
        {
            if (ListViewMatchType == matchType)
            {
                return;
            }
            ListViewMatchType = matchType;
            OnFilterUpdated(listView);
        }

        public void SetListViewMatchString(string matchString, ListView listView)
        {
            listViewMatchString = matchString;
            OnFilterUpdated(listView);
        }

        void OnFilterUpdated(ListView listView)
        {
            MatchedItems.Clear();
            MatchedItems.AddRange(GetFilterItems());
            if (listView != null)
            {
                listView.ClearSelection();
                listView.RefreshItems();
            }
            RefreshLogCount?.Invoke();
        }

        IEnumerable<OutputScriptableItemLog> GetFilterItems() => consoleItems.Where(MatchFilter);

        bool MatchFilter(OutputScriptableItemLog item)
            => MatchFilter(item, ListViewMatchType, listViewMatchString);

        static bool MatchFilter(OutputScriptableItemLog item, MatchType matchType, string matchString)
        {
            var matchStrings = matchString.Split(WhiteSpaces, StringSplitOptions.RemoveEmptyEntries);
            if (matchStrings.Length == 0)
            {
                return true;
            }

            var targets = matchType switch
            {
                MatchType.All => new[]
                {
                    item.BuildTimestampDeviceString(),
                    item.dvid,
                    item.type,
                    item.message,
                    item.origin.id.ToString(),
                    item.origin.name,
                    item.player.id,
                    item.player.userName,
                },
                MatchType.Message => new[]
                {
                    item.message,
                },
                MatchType.Item => new[]
                {
                    item.origin.id.ToString(),
                    item.origin.name,
                },
                _ => throw new ArgumentOutOfRangeException(),
            };

            return matchStrings.All(match => targets.Any(t => t?.Contains(match, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        void ReadLogFile(string filePath, ListView listView)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Seek(logFilePosition, SeekOrigin.Begin);
                    using (var stream = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                    {
                        while (!stream.EndOfStream)
                        {
                            var text = stream.ReadLine();
                            logFilePosition = fs.Position;
                            if (string.IsNullOrEmpty(text))
                            {
                                continue;
                            }

                            try
                            {
                                var item = JsonUtility.FromJson<OutputScriptableItemLog>(text);
                                InsertWithTsdvOrder(item, consoleItems);
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
                        }
                    }
                }
            }
            catch (Exception err)
            {
                UnityEngine.Debug.LogError(err);
            }

            MatchedItems.Clear();
            MatchedItems.AddRange(GetFilterItems());
            if (listView != null)
            {
                listView.RefreshItems();
                listView.ScrollToItem(-1);
            }
            RefreshLogCount?.Invoke();
        }

        void InsertWithTsdvOrder(OutputScriptableItemLog item, List<OutputScriptableItemLog> target)
        {
            var lastIndex = GetLastIndexExceptNewer(target, item.tsdv);
            target.Insert(lastIndex + 1, item);
        }

        int GetLastIndexExceptNewer(List<OutputScriptableItemLog> list, double tsdv)
        {
            for (var i = list.Count - 1; i >= 0; --i)
            {
                var target = list[i];
                if (!target.HasTimestamp)
                {
                    return i;
                }
                if (target.tsdv <= tsdv)
                {
                    return i;
                }
            }
            return -1;
        }

        void StartLogFileWatcher(string filePath, ListView listView)
        {
#if UNITY_EDITOR_OSX
            Environment.SetEnvironmentVariable("MONO_MANAGED_WATCHER", "enabled");
#endif
            var fileInfo = new FileInfo(filePath);
            logFileWatcher = new FileSystemWatcher();
            logFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            logFileWatcher.Path = fileInfo.DirectoryName;
            logFileWatcher.Filter = fileInfo.Name;
            logFileWatcher.Changed += (_, _) =>
            {
                mainThread.Post(_ =>
                {
                    ReadLogFile(filePath, listView);
                }, null);
            };
            logFileWatcher.Created += (_, _) =>
            {
                mainThread.Post(_ =>
                {
                    ClearLogs(listView);
                    logFilePosition = 0;
                    ReadLogFile(filePath, listView);
                }, null);
            };

            logFileWatcher.EnableRaisingEvents = true;
        }

        public void StopLogFileWatcher()
        {
            if (logFileWatcher != null)
            {
                logFileWatcher.EnableRaisingEvents = false;
                logFileWatcher.Dispose();
                logFileWatcher = null;
            }
        }

        public void SetPaused(bool paused)
        {
            if (logFileWatcher != null)
            {
                logFileWatcher.EnableRaisingEvents = !paused;
            }
        }

        public void ClearLogs(ListView listView)
        {
            consoleItems.Clear();
            MatchedItems.Clear();

            if (listView != null)
            {
                listView.ClearSelection();
                listView.RefreshItems();
            }
            RefreshLogCount?.Invoke();
        }

        public void LoadAndWatchNewFile(string path, ListView listView)
        {
            StopLogFileWatcher();
            ClearLogs(listView);
            logFilePosition = 0;
            ScriptLogFilePath = path;
            ReadLogFile(path, listView);
            StartLogFileWatcher(path, listView);
        }

        public void LoadAndWatchDefaultLogFile(ListView listView) => LoadAndWatchNewFile(GetDefaultScriptLogFilePath(), listView);

        public void Dispose()
        {
            StopLogFileWatcher();
        }
    }

    public enum MatchType
    {
        All,
        Message,
        Item,
    }

    [Serializable]
    public struct OutputScriptableItemLog
    {
        [SerializeField] public double tsdv;
        [SerializeField] public string dvid;
        [SerializeField] public ItemInfo origin;
        [SerializeField] public PlayerInfo player;
        [SerializeField] public string type;
        [SerializeField] public string message;

        public bool HasTimestamp => tsdv > 0;
        DateTimeOffset TimestampDevice => DateTimeOffset.FromUnixTimeSeconds((long) tsdv);
        public string BuildTimestampDeviceString() => TimestampDevice.LocalDateTime.ToString(CultureInfo.CurrentCulture);

        public string BuildLabelString() =>
            type switch
            {
                "ScriptLog_Dropped" => $"{type,-32} some logs have been dropped",
                "ScriptLog_Information" or "ScriptLog_Warning" or "ScriptLog_Error" => $"[{TimestampDevice.LocalDateTime}] devid:{dvid} item:{origin.id} [{origin.name}] player:{player.id} [{player.userName}]\n{type,-32} {message}",
                "RoomProfileLog_Information" or "RoomProfileLog_Warning" or "RoomProfileLog_Error" => $"[{TimestampDevice.LocalDateTime}]\\n{type,-32} {message}",
                "Error" => $"{type,-32} {message}",
                _ => $"{type,-32} unknown type",
            };
    }

    [Serializable]
    public struct ItemInfo
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
    public struct PlayerInfo
    {
        [SerializeField] public string id;
        [SerializeField] public string userName;

        public PlayerInfo(string entryId, string userName)
        {
            this.id = entryId;
            this.userName = userName;
        }
    }
}
