﻿using System;
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
                if (!TryGetKey(trigger.Target, senderItemId, trigger.SpecifiedTargetItem, args.CollidedObject,
                    trigger.Key, out var key))
                {
                    continue;
                }
                if (args.DontOverride && roomStateRepository.TryGetValue(key, out _))
                {
                    continue;
                }
                var value = GetStateValue(trigger.Type, trigger.Value, signal);
                yield return new KeyValuePair<string, StateValue>(key, value);
            }
        }

        static bool TryGetKey(TriggerTarget target, ItemId senderItemId, IItem specifiedTarget,
            GameObject collidedObject, string triggerKey, out string key)
        {
            key = default;
            switch (target)
            {
                case TriggerTarget.Item:
                    key = RoomStateKey.GetItemKey(senderItemId.Value, triggerKey);
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
                    key = RoomStateKey.GetItemKey(specifiedTarget.Id.Value, triggerKey);
                    return true;
                case TriggerTarget.Player:
                    key = RoomStateKey.GetPlayerKey(triggerKey);
                    return true;
                case TriggerTarget.CollidedItemOrPlayer:
                    if (collidedObject.CompareTag("Player"))
                    {
                        key = RoomStateKey.GetPlayerKey(triggerKey);
                        return true;
                    }

                    var collidedItem = collidedObject.GetComponentInParent<IItem>();
                    if (collidedItem != null)
                    {
                        key = RoomStateKey.GetItemKey(collidedItem.Id.Value, triggerKey);
                        return true;
                    }

                    return false;
                case TriggerTarget.Global:
                    key = RoomStateKey.GetGlobalKey(triggerKey);
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        static StateValue GetStateValue(ParameterType type, TriggerValue triggerValue, StateValue signal)
        {
            switch (type)
            {
                case ParameterType.Signal:
                    return signal;
                case ParameterType.Bool:
                    return new StateValue(triggerValue.BoolValue);
                case ParameterType.Integer:
                    return new StateValue(triggerValue.IntegerValue);
                case ParameterType.Float:
                    return new StateValue(triggerValue.FloatValue);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
