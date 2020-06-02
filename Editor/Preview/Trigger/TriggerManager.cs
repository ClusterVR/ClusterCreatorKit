using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Gimmick;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class TriggerManager
    {
        readonly RoomStateRepository roomStateRepository;
        readonly GimmickManager gimmickManager;

        public TriggerManager(RoomStateRepository roomStateRepository, ItemCreator itemCreator, GimmickManager gimmickManager)
        {
            this.roomStateRepository = roomStateRepository;
            this.gimmickManager = gimmickManager;

            itemCreator.OnCreate += OnCreateItem;
        }

        void OnCreateItem(IItem item)
        {
            Add(item.gameObject.GetComponents<IItemTrigger>());
        }

        public void Add(IEnumerable<IItemTrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.TriggerEvent += OnTriggered;
            }
        }

        void OnTriggered(object sender, TriggerEventArgs args)
        {
            var senderTrigger = (IItemTrigger) sender;
            var senderItem = senderTrigger.Item;
            if (!TryGetKey(args.Target, senderItem, args.SpecifiedTargetItem, args.CollidedObject, args.Key, out var key)) return;
            var gimmickValue = GetGimmickValue(args.Type, args.Value);
            roomStateRepository.Update(key, gimmickValue);
            gimmickManager.Invoke(key, gimmickValue);
        }

        bool TryGetKey(ItemTriggerTarget target, IItem senderItem, IItem specifiedTarget, GameObject collidedObject, string triggerKey, out string key)
        {
            key = default;
            switch (target)
            {
                case ItemTriggerTarget.This:
                    key = RoomStateKey.GetItemKey(senderItem.Id.Value, triggerKey);
                    return true;
                case ItemTriggerTarget.SpecifiedItem:
                    if (specifiedTarget == null) return false;
                    if (specifiedTarget.gameObject == null) return false;
                    key = RoomStateKey.GetItemKey(specifiedTarget.Id.Value, triggerKey);
                    return true;
                case ItemTriggerTarget.Owner:
                    key = RoomStateKey.GetPlayerKey(triggerKey);
                    return true;
                case ItemTriggerTarget.CollidedItemOrPlayer:
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
                case ItemTriggerTarget.Global:
                    key = RoomStateKey.GetGlobalKey(triggerKey);
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        GimmickValue GetGimmickValue(ParameterType type, TriggerValue triggerValue)
        {
            switch (type)
            {
                case ParameterType.Signal:
                    return new GimmickValue(DateTime.Now);
                case ParameterType.Bool:
                    return new GimmickValue(triggerValue.BoolValue);
                case ParameterType.Integer:
                    return new GimmickValue(triggerValue.IntegerValue);
                case ParameterType.Float:
                    return new GimmickValue(triggerValue.FloatValue);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
