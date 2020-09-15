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
    public sealed class GimmickManager
    {
        readonly RoomStateRepository roomStateRepository;
        readonly Dictionary<string, HashSet<IGimmick>> gimmicks = new Dictionary<string, HashSet<IGimmick>>();
        readonly Dictionary<ulong, (IGimmick, string)[]> gimmicksInItems = new Dictionary<ulong, (IGimmick, string)[]>();

        public GimmickManager(RoomStateRepository roomStateRepository, ItemCreator itemCreator, ItemDestroyer itemDestroyer)
        {
            this.roomStateRepository = roomStateRepository;
            itemCreator.OnCreate += OnCreateItem;
            itemCreator.OnCreateCompleted += OnCreateItemCompleted;
            itemDestroyer.OnDestroy += OnDestroyItem;
        }

        public void AddGimmicksInScene(IEnumerable<IGimmick> gimmicks)
        {
            foreach (var gimmick in gimmicks)
            {
                AddGimmick(GetGimmickKey(gimmick), gimmick);
            }
        }

        public void AddGimmicksInItem(IGimmick[] gimmicks, ulong itemId)
        {
            if (gimmicks.Length == 0) return;
            var gimmickAndKeys = gimmicks.Select(gimmick => (gimmick, key: GetGimmickKey(gimmick))).ToArray();
            if (itemId != 0L) gimmicksInItems[itemId] = gimmickAndKeys;
            foreach (var gimmickAndKey in gimmickAndKeys) AddGimmick(gimmickAndKey.key, gimmickAndKey.gimmick);
        }

        void OnCreateItem(IItem item)
        {
            AddGimmicksInItem(item.gameObject.GetComponentsInChildren<IGimmick>(true), item.Id.Value);
        }

        void OnCreateItemCompleted(IItem item)
        {
            var now = DateTime.UtcNow;
            foreach (var gimmick in item.gameObject.GetComponentsInChildren<IGimmick>(true))
            {
                var key = GetGimmickKey(gimmick);
                if (!roomStateRepository.TryGetValue(key, out var value)) continue;
                Run(gimmick, value, now);
            }
        }

        void AddGimmick(string key, IGimmick gimmick)
        {
            if (gimmicks.TryGetValue(key, out var gimmickSet))
            {
                gimmickSet.Add(gimmick);
            }
            else
            {
                gimmicks.Add(key, new HashSet<IGimmick> {gimmick});
            }
        }

        void OnDestroyItem(IItem item)
        {
            var itemId = item.Id.Value;
            if (gimmicksInItems.TryGetValue(itemId, out var gimmickAndKeys))
            {
                foreach (var gimmickAndKey in gimmickAndKeys) RemoveGimmick(gimmickAndKey.Item2, gimmickAndKey.Item1);
                gimmicksInItems.Remove(itemId);
            }
        }

        void RemoveGimmick(string key, IGimmick gimmick)
        {
            var hasGimmick = gimmicks.TryGetValue(key, out var gimmickSet);
            Assert.IsTrue(hasGimmick);
            gimmickSet.Remove(gimmick);
            if (gimmickSet.Count == 0) gimmicks.Remove(key);
        }

        public void OnStateUpdated(IEnumerable<string> keys)
        {
            var now = DateTime.UtcNow;
            foreach (var key in keys)
            {
                if (this.gimmicks.TryGetValue(key, out var gimmicks) && roomStateRepository.TryGetValue(key, out var value))
                {
                    foreach (var gimmick in gimmicks.ToArray())
                    {
                        Run(gimmick, value, now);
                    }
                }
            }
        }

        void Run(IGimmick gimmick, StateValue value, DateTime now)
            => gimmick.Run(GetGimmickValue(gimmick.ParameterType, value), now);

        string GetGimmickKey(IGimmick gimmick)
        {
            switch (gimmick.Target)
            {
                case GimmickTarget.Global:
                    return RoomStateKey.GetGlobalKey(gimmick.Key);
                case GimmickTarget.Player:
                    return RoomStateKey.GetPlayerKey(gimmick.Key);
                case GimmickTarget.Item:
                    return RoomStateKey.GetItemKey(gimmick.ItemId.Value, gimmick.Key);
                default:
                    throw new NotImplementedException();
            }
        }

        GimmickValue GetGimmickValue(ParameterType type, StateValue value)
        {
            switch (type)
            {
                case ParameterType.Signal:
                    return new GimmickValue(value.ToDateTime());
                case ParameterType.Bool:
                    return new GimmickValue(value.ToBool());
                case ParameterType.Integer:
                    return new GimmickValue(value.ToInt());
                case ParameterType.Float:
                    return new GimmickValue(value.ToFloat());
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
