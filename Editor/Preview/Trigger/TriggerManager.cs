using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Preview.Gimmick;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class TriggerManager
    {
        readonly RoomStateRepository roomStateRepository;
        readonly GimmickManager gimmickManager;
        readonly SignalGenerator signalGenerator;

        public TriggerManager(
            RoomStateRepository roomStateRepository,
            ItemCreator itemCreator,
            GimmickManager gimmickManager,
            SignalGenerator signalGenerator)
        {
            this.roomStateRepository = roomStateRepository;
            this.gimmickManager = gimmickManager;
            this.signalGenerator = signalGenerator;

            itemCreator.OnCreate += OnCreateItem;
        }

        void OnCreateItem(IItem item)
        {
            Add(item.gameObject.GetComponents<IItemTrigger>());
            Add(item.gameObject.GetComponentsInChildren<IPlayerTrigger>(true));
            Add(item.gameObject.GetComponentsInChildren<IGlobalTrigger>(true));
        }

        public void Add(IEnumerable<IItemTrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.TriggerEvent += OnTriggered;
            }
        }

        public void Add(IEnumerable<IPlayerTrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.TriggerEvent += OnTriggered;
            }
        }

        public void Add(IEnumerable<IGlobalTrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.TriggerEvent += OnTriggered;
            }
        }

        void OnTriggered(IItemTrigger sender, TriggerEventArgs args)
        {
            UpdateState(GetStateChange(args, sender.Item.Id).ToArray());
        }

        void OnTriggered(IPlayerTrigger _, TriggerEventArgs args)
        {
            OnTriggered(args);
        }

        void OnTriggered(TriggerEventArgs args)
        {
            UpdateState(GetStateChange(args).ToArray());
        }

        void UpdateState(IReadOnlyCollection<KeyValuePair<string, StateValue>> stateChange)
        {
            if (stateChange.Count == 0)
            {
                return;
            }
            foreach (var state in stateChange)
            {
                roomStateRepository.Update(state.Key, state.Value);
            }

            gimmickManager.OnStateUpdated(stateChange.Select(s => s.Key));
        }

        IEnumerable<KeyValuePair<string, StateValue>> GetStateChange(TriggerEventArgs args,
            ItemId senderItemId = default)
        {
            if (!signalGenerator.TryGet(out var signal))
            {
                yield break;
            }

            foreach (var trigger in args.TriggerParams)
            {
                if (!TryGetKeyPrefix(trigger.Target, senderItemId, trigger.SpecifiedTargetItem, args.CollidedObject, out var keyPrefix))
                {
                    continue;
                }
                foreach (var value in trigger.ToTriggerStates(keyPrefix, signal))
                {
                    if (args.DontOverride && roomStateRepository.TryGetValue(value.Key, out _))
                    {
                        continue;
                    }
                    yield return value;
                }
            }
        }

        static bool TryGetKeyPrefix(TriggerTarget target, ItemId senderItemId, IItem specifiedTarget,
            GameObject collidedObject, out string key)
        {
            key = default;
            switch (target)
            {
                case TriggerTarget.Item:
                    key = RoomStateKey.GetItemKeyPrefix(senderItemId.Value);
                    return true;
                case TriggerTarget.SpecifiedItem:
                    if (specifiedTarget == null)
                    {
                        return false;
                    }
                    if (specifiedTarget.gameObject == null)
                    {
                        return false;
                    }
                    key = RoomStateKey.GetItemKeyPrefix(specifiedTarget.Id.Value);
                    return true;
                case TriggerTarget.Player:
                    key = RoomStateKey.GetPlayerKeyPrefix();
                    return true;
                case TriggerTarget.CollidedItemOrPlayer:
                    if (collidedObject.CompareTag("Player"))
                    {
                        key = RoomStateKey.GetPlayerKeyPrefix();
                        return true;
                    }

                    var collidedItem = collidedObject.GetComponentInParent<IItem>();
                    if (collidedItem != null)
                    {
                        key = RoomStateKey.GetItemKeyPrefix(collidedItem.Id.Value);
                        return true;
                    }

                    return false;
                case TriggerTarget.Global:
                    key = RoomStateKey.GetGlobalKeyPrefix();
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
