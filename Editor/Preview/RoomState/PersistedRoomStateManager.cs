using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public sealed class PersistedRoomStateManager
    {
        public static PersistedRoomStateManager CreateFromActiveScene()
        {
            var scene = SceneManager.GetActiveScene();
            var persistedPlayerStateKeys = PersistedPlayerStateKeysGatherer.Gather(scene);
            if (!persistedPlayerStateKeys.Any())
            {
                return null;
            }

            var sceneGuid = AssetDatabase.AssetPathToGUID(scene.path);

            if (string.IsNullOrEmpty(sceneGuid))
            {
                Debug.LogWarning("プレビューでセーブ機能を利用するにはシーンを保存する必要があります。今回のプレビューではセーブ機能は利用されません。");
                return null;
            }

            return new PersistedRoomStateManager(sceneGuid, persistedPlayerStateKeys);
        }

        readonly string sceneGuid;
        readonly IReadOnlyCollection<string> persistedPlayerStateKeys;

        PersistedRoomStateManager(string sceneGuid, IEnumerable<string> persistedPlayerStateKeys)
        {
            this.sceneGuid = sceneGuid;
            this.persistedPlayerStateKeys = persistedPlayerStateKeys.Select(RoomStateKey.GetPlayerKey).ToArray();
        }

        public IEnumerable<string> Load(RoomStateRepository roomStateRepository)
        {
            if (!PersistedRoomStateRepository.TryGetPersistedRoomStateData(sceneGuid, out var saveData))
            {
                return Enumerable.Empty<string>();
            }
            var updatedKeys = new List<string>();
            foreach (var state in saveData.Player.State)
            {
                if (persistedPlayerStateKeys.Contains(state.Key))
                {
                    roomStateRepository.Update(state.Key, state.Value);
                    updatedKeys.Add(state.Key);
                }
            }

            return updatedKeys;
        }

        public void Save(RoomStateRepository roomStateRepository)
        {
            var states = new List<State>();
            foreach (var key in persistedPlayerStateKeys)
            {
                if (roomStateRepository.TryGetValue(key, out var value))
                {
                    states.Add(new State(key, value));
                }
            }

            if (!states.Any())
            {
                return;
            }
            var saveData = new PersistedRoomStateData(new RoomStateSegment(states.ToArray()));
            PersistedRoomStateRepository.Update(sceneGuid, saveData);
        }
    }
}
