using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class GimmickManager : IGimmickUpdater
    {
        readonly RoomStateRepository roomStateRepository;
        readonly Dictionary<string, HashSet<GimmickStateValueSet>> gimmicks = new();

        readonly Dictionary<ulong, IReadOnlyList<(GimmickStateValueSet, string)>> gimmicksInItems = new();
        readonly Dictionary<string, IReadOnlyList<(GimmickStateValueSet, string)>> gimmicksInSubScenes = new();

        public GimmickManager(RoomStateRepository roomStateRepository, ItemCreator itemCreator,
            ItemDestroyer itemDestroyer, SubSceneManager subSceneManager)
        {
            this.roomStateRepository = roomStateRepository;
            itemCreator.OnCreate += OnCreateItem;
            itemCreator.OnCreateCompleted += OnCreateItemCompleted;
            itemDestroyer.OnDestroy += OnDestroyItem;

            subSceneManager.OnSubSceneActiveChanged += OnSubSceneActiveChanged;
        }

        public void AddGimmicksInScene(IEnumerable<IGimmick> gimmicks)
        {
            foreach (var (gimmick, key) in gimmicks.SelectMany(GimmickStateValueSets))
            {
                AddGimmick(key, gimmick);
            }
        }

        public void AddGimmicksInItem(IGimmick[] gimmicks, ulong itemId)
        {
            if (gimmicks.Length == 0)
            {
                return;
            }
            var gimmickAndKeys = gimmicks.SelectMany(GimmickStateValueSets).ToArray();
            if (itemId != 0L)
            {
                gimmicksInItems[itemId] = gimmickAndKeys;
            }
            foreach (var (gimmick, key) in gimmickAndKeys)
            {
                AddGimmick(key, gimmick);
            }
        }

        void OnCreateItem(IItem item)
        {
            AddGimmicksInItem(item.gameObject.GetComponentsInChildren<IGimmick>(true), item.Id.Value);
        }

        void OnCreateItemCompleted(IItem item)
        {
            RunGimmicks(item.gameObject.GetComponentsInChildren<IGimmick>(true).SelectMany(GimmickStateValueSets));
        }

        void RunGimmicks(IEnumerable<(GimmickStateValueSet, string)> gimmickAndKeys)
        {
            var now = DateTime.UtcNow;
            foreach (var (gimmick, key) in gimmickAndKeys)
            {
                if (roomStateRepository.TryGetValue(key, out var value))
                {
                    gimmick.Run(key, value, now);
                }
            }
        }

        void AddGimmick(string key, GimmickStateValueSet gimmick)
        {
            if (gimmicks.TryGetValue(key, out var gimmickSet))
            {
                gimmickSet.Add(gimmick);
            }
            else
            {
                gimmicks.Add(key, new HashSet<GimmickStateValueSet> { gimmick });
            }
        }

        void OnDestroyItem(IItem item)
        {
            var itemId = item.Id.Value;
            if (gimmicksInItems.TryGetValue(itemId, out var gimmickAndKeys))
            {
                foreach (var gimmickAndKey in gimmickAndKeys)
                {
                    RemoveGimmick(gimmickAndKey.Item2, gimmickAndKey.Item1);
                }
                gimmicksInItems.Remove(itemId);
            }
        }

        void RemoveGimmick(string key, GimmickStateValueSet gimmick)
        {
            var hasGimmick = gimmicks.TryGetValue(key, out var gimmickSet);
            Assert.IsTrue(hasGimmick);
            gimmickSet.Remove(gimmick);
            if (gimmickSet.Count == 0)
            {
                gimmicks.Remove(key);
            }
        }

        public void OnStateUpdated(IEnumerable<string> keys)
        {
            var now = DateTime.UtcNow;
            foreach (var key in keys)
            {
                if (this.gimmicks.TryGetValue(key, out var gimmicks) &&
                    roomStateRepository.TryGetValue(key, out var value))
                {
                    foreach (var gimmick in gimmicks.ToArray())
                    {
                        gimmick.Run(key, value, now);
                    }
                }
            }
        }

        void OnSubSceneActiveChanged((string subSceneName, bool isActive) ev)
        {
            if (ev.isActive)
            {
                OnSubSceneLoaded(ev.subSceneName);
            }
            else
            {
                OnSubSceneUnloaded(ev.subSceneName);
            }
        }

        void OnSubSceneLoaded(string subSceneName)
        {
            var sceneRootObjects = SceneManager.GetSceneByName(subSceneName).GetRootGameObjects();
            var gimmicks = sceneRootObjects.SelectMany(g => g.GetComponentsInChildren<IGimmick>(true));
            var gimmickAndKeys = gimmicks.SelectMany(GimmickStateValueSets).ToArray();
            if (gimmickAndKeys.Length == 0)
            {
                return;
            }

            AddGimmicksInSubScene(gimmickAndKeys, subSceneName);
            RunGimmicks(gimmickAndKeys);
        }

        void AddGimmicksInSubScene(IReadOnlyList<(GimmickStateValueSet, string)> gimmickAndKeys, string subSceneName)
        {
            gimmicksInSubScenes[subSceneName] = gimmickAndKeys;
            foreach (var (gimmick, key) in gimmickAndKeys)
            {
                AddGimmick(key, gimmick);
            }
        }

        void OnSubSceneUnloaded(string subSceneName)
        {
            if (gimmicksInSubScenes.TryGetValue(subSceneName, out var gimmickAndKeys))
            {
                foreach (var gimmickAndKey in gimmickAndKeys)
                {
                    RemoveGimmick(gimmickAndKey.Item2, gimmickAndKey.Item1);
                }
                gimmicksInSubScenes.Remove(subSceneName);
            }
        }

        static IEnumerable<(GimmickStateValueSet gimmick, string key)> GimmickStateValueSets(IGimmick gimmick)
        {
            var set = new GimmickStateValueSet(gimmick);
            var keyPrefix = GetGimmickKeyPrefix(gimmick);
            return set.Keys().Select(k => (set, keyPrefix + k));
        }

        static string GetGimmickKeyPrefix(IGimmick gimmick)
        {
            switch (gimmick.Target)
            {
                case GimmickTarget.Global:
                    return RoomStateKey.GetGlobalKeyPrefix();
                case GimmickTarget.Player:
                    return RoomStateKey.GetPlayerKeyPrefix();
                case GimmickTarget.Item:
                    return RoomStateKey.GetItemKeyPrefix(gimmick.ItemId.Value);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
