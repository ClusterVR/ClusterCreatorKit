using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using UnityEngine.Assertions;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class GimmickManager : IGimmickUpdater
    {
        readonly RoomStateRepository roomStateRepository;
        readonly Dictionary<string, HashSet<GimmickStateValueSet>> gimmicks = new Dictionary<string, HashSet<GimmickStateValueSet>>();

        readonly Dictionary<ulong, (GimmickStateValueSet, string)[]>
            gimmicksInItems = new Dictionary<ulong, (GimmickStateValueSet, string)[]>();

        public GimmickManager(RoomStateRepository roomStateRepository, ItemCreator itemCreator,
            ItemDestroyer itemDestroyer)
        {
            this.roomStateRepository = roomStateRepository;
            itemCreator.OnCreate += OnCreateItem;
            itemCreator.OnCreateCompleted += OnCreateItemCompleted;
            itemDestroyer.OnDestroy += OnDestroyItem;
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
            var now = DateTime.UtcNow;
            foreach (var (gimmick, key) in item.gameObject.GetComponentsInChildren<IGimmick>(true).SelectMany(GimmickStateValueSets))
            {
                if (!roomStateRepository.TryGetValue(key, out var value))
                {
                    continue;
                }
                gimmick.Run(key, value, now);
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
