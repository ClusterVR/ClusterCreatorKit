using System;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public static class PersistedRoomStateRepository
    {
        const string PersistedKeyPrefix = "PersistedRoomState/";
        const string PersistedSceneGuidsKey = "PersistedRoomStateSceneGuids";

        public static void Update(string sceneGuid, PersistedRoomStateData persistedRoomStateData)
        {
            SetPersistedRoomStateData(GetPersistedDataKey(sceneGuid), persistedRoomStateData);
            EnforceSceneGuidSaved(sceneGuid);
            PlayerPrefs.Save();
        }

        public static bool IsSaved(string sceneGuid)
        {
            var key = GetPersistedDataKey(sceneGuid);
            return PlayerPrefs.HasKey(key);
        }

        public static void Clear(string sceneGuid)
        {
            PlayerPrefs.DeleteKey(GetPersistedDataKey(sceneGuid));
            EnforceSceneGuidRemoved(sceneGuid);
            PlayerPrefs.Save();
        }

        public static void ClearAll()
        {
            foreach (var path in GetPersistedSceneGuids().SceneGuids)
            {
                PlayerPrefs.DeleteKey(GetPersistedDataKey(path));
            }

            PlayerPrefs.DeleteKey(PersistedSceneGuidsKey);
            PlayerPrefs.Save();
        }

        public static bool TryGetPersistedRoomStateData(string sceneGuid,
            out PersistedRoomStateData persistedRoomStateData)
        {
            var key = GetPersistedDataKey(sceneGuid);
            if (!PlayerPrefs.HasKey(key))
            {
                persistedRoomStateData = default;
                return false;
            }

            var json = PlayerPrefs.GetString(key);
            persistedRoomStateData = JsonUtility.FromJson<PersistedRoomStateData>(json);
            return true;
        }

        static string GetPersistedDataKey(string sceneGuid)
        {
            return $"{PersistedKeyPrefix}{sceneGuid}";
        }

        static void SetPersistedRoomStateData(string key, PersistedRoomStateData persistedRoomStateData)
        {
            var json = JsonUtility.ToJson(persistedRoomStateData);
            PlayerPrefs.SetString(key, json);
        }

        static void EnforceSceneGuidSaved(string sceneGuid)
        {
            var paths = GetPersistedSceneGuids();
            if (paths.TryAdd(sceneGuid))
            {
                SetPersistedSceneGuids(paths);
            }
        }

        static void EnforceSceneGuidRemoved(string sceneGuid)
        {
            var guids = GetPersistedSceneGuids();
            if (guids.TryRemove(sceneGuid))
            {
                SetPersistedSceneGuids(guids);
            }
        }

        static PersistedRoomStateSceneGuids GetPersistedSceneGuids()
        {
            var json = PlayerPrefs.GetString(PersistedSceneGuidsKey);
            if (string.IsNullOrEmpty(json))
            {
                return new PersistedRoomStateSceneGuids();
            }
            return JsonUtility.FromJson<PersistedRoomStateSceneGuids>(json);
        }

        static void SetPersistedSceneGuids(PersistedRoomStateSceneGuids persistedRoomStateSceneGuids)
        {
            var json = JsonUtility.ToJson(persistedRoomStateSceneGuids);
            PlayerPrefs.SetString(PersistedSceneGuidsKey, json);
        }
    }

    [Serializable]
    sealed class PersistedRoomStateSceneGuids
    {
        [SerializeField] string[] sceneGuids;
        public string[] SceneGuids => sceneGuids;

        public PersistedRoomStateSceneGuids()
        {
            sceneGuids = new string[] { };
        }

        public bool TryAdd(string guid)
        {
            if (sceneGuids.Contains(guid))
            {
                return false;
            }
            sceneGuids = sceneGuids.Append(guid).ToArray();
            return true;
        }

        public bool TryRemove(string guid)
        {
            if (!sceneGuids.Contains(guid))
            {
                return false;
            }
            sceneGuids = sceneGuids.Where(g => g != guid).ToArray();
            return true;
        }
    }
}
