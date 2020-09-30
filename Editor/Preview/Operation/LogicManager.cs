using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Gimmick;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Preview.Trigger;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Operation;
using UnityEngine;
using ValueType = ClusterVR.CreatorKit.Operation.ValueType;

namespace ClusterVR.CreatorKit.Editor.Preview.Operation
{
    public sealed class LogicManager
    {
        readonly RoomStateRepository roomStateRepository;
        readonly GimmickManager gimmickManager;
        readonly SignalGenerator signalGenerator;

        public LogicManager(ItemCreator itemCreator,
            RoomStateRepository roomStateRepository,
            GimmickManager gimmickManager,
            IEnumerable<IItemLogic> itemLogics,
            IEnumerable<IPlayerLogic> playerLogics,
            IEnumerable<IGlobalLogic> globalLogics,
            SignalGenerator signalGenerator)
        {
            this.roomStateRepository = roomStateRepository;
            this.gimmickManager = gimmickManager;
            this.signalGenerator = signalGenerator;
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
            if (itemId.Value == 0) return;
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

        void Execute(Logic logic, ItemId itemId = default)
        {
            if (logic == null || !logic.IsValid()) return;
            if (!signalGenerator.TryGet(out var signal)) return;
            var updatedKeys = RunLogic(logic, itemId, signal);
            gimmickManager.OnStateUpdated(updatedKeys);
        }

        IEnumerable<string> RunLogic(Logic logic, ItemId itemId, StateValue signal)
        {
            var updatedKeys = new Queue<string>();
            foreach (var statement in logic.Statements)
            {
                if (statement == null) continue;
                RunStatement(statement, itemId, updatedKeys, signal);
            }

            return updatedKeys;
        }

        void RunStatement(Statement statement, ItemId itemId, Queue<string> updatedKeys, StateValue signal)
        {
            if (RunSingleStatement(statement.SingleStatement, itemId, signal, out var updatedKey)) updatedKeys.Enqueue(updatedKey);
        }

        bool RunSingleStatement(SingleStatement singleStatement, ItemId itemId, StateValue signal, out string updatedKey)
        {
            var target = singleStatement.TargetState;
            var parameterType = target.ParameterType;
            updatedKey = GetStateKey(target.Target, target.Key, itemId);
            if (parameterType == ParameterType.Signal)
            {
                var conditionSatisfied = singleStatement.Expression == null || EvaluateExpression(singleStatement.Expression, itemId).ToBool();
                if (conditionSatisfied) roomStateRepository.Update(updatedKey, signal);
                return conditionSatisfied;
            }
            else
            {
                var value = Cast(parameterType, EvaluateExpression(singleStatement.Expression, itemId));
                roomStateRepository.Update(updatedKey, value);
                return true;
            }
        }

        StateValue EvaluateExpression(Expression expression, ItemId itemId)
        {
            switch (expression.Type)
            {
                case ExpressionType.Value:
                    return EvaluateValue(expression.Value, itemId);
                case ExpressionType.OperatorExpression:
                    return Operate(expression.OperatorExpression, itemId);
                default:
                    throw new NotImplementedException();
            }
        }

        StateValue EvaluateValue(Value value, ItemId itemId)
        {
            switch (value.Type)
            {
                case ValueType.Constant:
                    return value.Constant;
                case ValueType.RoomState:
                    return GetRoomStateValue(value.SourceState, itemId);
                default:
                    throw new NotImplementedException();
            }
        }

        StateValue GetRoomStateValue(SourceState state, ItemId itemId)
        {
            if (roomStateRepository.TryGetValue(GetStateKey(state.Target, state.Key, itemId), out var value))
            {
                return value;
            }
            else
            {
                return StateValue.Default;
            }
        }

        StateValue Operate(OperatorExpression operatorExpression, ItemId itemId)
        {
            StateValue GetOperand(int index) => EvaluateExpression(operatorExpression.Operands[index], itemId);

            switch (operatorExpression.Operator)
            {
                case Operator.Not:
                    return new StateValue(!GetOperand(0).ToBool());
                case Operator.Minus:
                    return new StateValue(-GetOperand(0).ToDouble());

                case Operator.Equals:
                    return new StateValue(Mathf.Approximately(GetOperand(0).ToFloat(), GetOperand(1).ToFloat()));
                case Operator.NotEquals:
                    return new StateValue(!Mathf.Approximately(GetOperand(0).ToFloat(), GetOperand(1).ToFloat()));
                case Operator.GreaterThan:
                    return new StateValue(GetOperand(0).ToDouble() > GetOperand(1).ToDouble());
                case Operator.GreaterThanOrEqual:
                    return new StateValue(GetOperand(0).ToDouble() >= GetOperand(1).ToDouble());
                case Operator.LessThan:
                    return new StateValue(GetOperand(0).ToDouble() < GetOperand(1).ToDouble());
                case Operator.LessThanOrEqual:
                    return new StateValue(GetOperand(0).ToDouble() <= GetOperand(1).ToDouble());

                case Operator.Add:
                    return new StateValue(GetOperand(0).ToDouble() + GetOperand(1).ToDouble());
                case Operator.Multiply:
                    return new StateValue(GetOperand(0).ToDouble() * GetOperand(1).ToDouble());
                case Operator.Subtract:
                    return new StateValue(GetOperand(0).ToDouble() - GetOperand(1).ToDouble());
                case Operator.Divide:
                    return new StateValue(GetOperand(0).ToDouble() / GetOperand(1).ToDouble());
                case Operator.Modulo:
                    return new StateValue(GetOperand(0).ToDouble() % GetOperand(1).ToDouble());

                case Operator.And:
                    return new StateValue(GetOperand(0).ToBool() && GetOperand(1).ToBool());
                case Operator.Or:
                    return new StateValue(GetOperand(0).ToBool() || GetOperand(1).ToBool());

                case Operator.Condition:
                    return GetOperand(0).ToBool() ? GetOperand(1) : GetOperand(2);

                case Operator.Min:
                    return new StateValue(Math.Min(GetOperand(0).ToDouble(), GetOperand(1).ToDouble()));
                case Operator.Max:
                    return new StateValue(Math.Max(GetOperand(0).ToDouble(), GetOperand(1).ToDouble()));
                case Operator.Clamp:
                    return new StateValue(Math.Min(Math.Max(GetOperand(0).ToDouble(), GetOperand(1).ToDouble()), GetOperand(2).ToDouble()));

                default: throw new NotImplementedException();
            }
        }

        static string GetStateKey(GimmickTarget target, string key, ItemId itemId)
        {
            switch (target)
            {
                case GimmickTarget.Item: return RoomStateKey.GetItemKey(itemId.Value, key);
                case GimmickTarget.Player: return RoomStateKey.GetPlayerKey(key);
                case GimmickTarget.Global: return RoomStateKey.GetGlobalKey(key);
                default: throw new NotImplementedException();
            }
        }

        static string GetStateKey(TargetStateTarget target, string key, ItemId itemId)
        {
            switch (target)
            {
                case TargetStateTarget.Item: return RoomStateKey.GetItemKey(itemId.Value, key);
                case TargetStateTarget.Player: return RoomStateKey.GetPlayerKey(key);
                case TargetStateTarget.Global: return RoomStateKey.GetGlobalKey(key);
                default: throw new NotImplementedException();
            }
        }

        static StateValue Cast(ParameterType type, StateValue value)
        {
            switch (type)
            {
                case ParameterType.Signal: return new StateValue(value.ToDateTime());
                case ParameterType.Bool: return new StateValue(value.ToBool());
                case ParameterType.Integer: return new StateValue(value.ToInt());
                case ParameterType.Float: return new StateValue(value.ToFloat());
                default: throw new NotImplementedException();
            }
        }
    }
}