using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Gimmick;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Preview.Trigger;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Operation;

namespace ClusterVR.CreatorKit.Editor.Preview.Operation
{
    public sealed class LogicManager
    {
        readonly LogicExecutor logicExecutor;

        public LogicManager(ItemCreator itemCreator,
            RoomStateRepository roomStateRepository,
            GimmickManager gimmickManager,
            IEnumerable<IItemLogic> itemLogics,
            IEnumerable<IPlayerLogic> playerLogics,
            IEnumerable<IGlobalLogic> globalLogics,
            SignalGenerator signalGenerator)
        {
            logicExecutor = new LogicExecutor(signalGenerator, new LogicStateRepository(roomStateRepository), gimmickManager);
            itemCreator.OnCreate += OnCreateItem;
            Register(itemLogics);
            Register(playerLogics);
            Register(globalLogics);
        }

        void OnCreateItem(IItem item)
        {
            Register(item.gameObject.GetComponents<IItemLogic>());
            Register(item.gameObject.GetComponentsInChildren<IPlayerLogic>(true));
            Register(item.gameObject.GetComponentsInChildren<IGlobalLogic>(true));
        }

        void Register(IEnumerable<IItemLogic> logics)
        {
            foreach (var logic in logics)
            {
                logic.OnRunItemLogic += OnRunItemLogic;
            }
        }

        void Register(IEnumerable<IPlayerLogic> logics)
        {
            foreach (var logic in logics)
            {
                logic.OnRunPlayerLogic += OnRunPlayerLogic;
            }
        }

        void Register(IEnumerable<IGlobalLogic> logics)
        {
            foreach (var logic in logics)
            {
                logic.OnRunGlobalLogic += OnRunGlobalLogic;
            }
        }

        void OnRunItemLogic(IItemLogic sender, RunItemLogicEventArgs args)
        {
            var itemId = sender.ItemId;
            if (itemId.Value == 0)
            {
                return;
            }
            Execute(args.Logic, itemId);
        }

        void OnRunPlayerLogic(RunPlayerLogicEventArgs args)
        {
            Execute(args.Logic);
        }

        void OnRunGlobalLogic(RunGlobalLogicEventArgs args)
        {
            Execute(args.Logic);
        }

        void Execute(Logic logic, ItemId itemId = default) => logicExecutor.Execute(logic, itemId);

        class LogicStateRepository : ILogicStateRepository
        {
            readonly RoomStateRepository roomStateRepository;

            internal LogicStateRepository(RoomStateRepository roomStateRepository)
            {
                this.roomStateRepository = roomStateRepository;
            }

            IStateValueSet ILogicStateRepository.GetRoomStateValueSet(SourceState state, ItemId itemId)
            {
                TryGetRoomStateValueSet(state.Target, state.Key, state.Type, itemId, out var stateValueSet);
                return stateValueSet;
            }

            bool TryGetRoomStateValueSet(GimmickTarget target, string key, ParameterType parameterType, ItemId itemId, out IStateValueSet stateValueSet)
            {
                stateValueSet = StateValueSet.Create(parameterType);
                var hasValue = false;
                foreach (var fieldName in stateValueSet.GetFieldNames())
                {
                    var stateKey = GetStateKeyPrefix(target, itemId) + fieldName.BuildKey(key);
                    if (roomStateRepository.TryGetValue(stateKey, out var stateValue))
                    {
                        stateValueSet.Update(fieldName, stateValue);
                        hasValue = true;
                    }
                }
                return hasValue;
            }

            void ILogicStateRepository.UpdateState(TargetState targetState, ItemId itemId, IStateValueSet stateValueSet, Queue<string> updatedKeys)
            {
                var keyPrefix = GetStateKeyPrefix(targetState.Target, itemId);
                foreach (var triggerState in stateValueSet.ToTriggerStates(keyPrefix, targetState.Key))
                {
                    roomStateRepository.Update(triggerState.Key, triggerState.Value);
                    updatedKeys.Enqueue(triggerState.Key);
                }
            }

            string GetStateKeyPrefix(GimmickTarget target, ItemId itemId)
            {
                switch (target)
                {
                    case GimmickTarget.Item: return RoomStateKey.GetItemKeyPrefix(itemId.Value);
                    case GimmickTarget.Player: return RoomStateKey.GetPlayerKeyPrefix();
                    case GimmickTarget.Global: return RoomStateKey.GetGlobalKeyPrefix();
                    default: throw new NotImplementedException();
                }
            }

            string GetStateKeyPrefix(TargetStateTarget target, ItemId itemId)
            {
                switch (target)
                {
                    case TargetStateTarget.Item: return RoomStateKey.GetItemKeyPrefix(itemId.Value);
                    case TargetStateTarget.Player: return RoomStateKey.GetPlayerKeyPrefix();
                    case TargetStateTarget.Global: return RoomStateKey.GetGlobalKeyPrefix();
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
