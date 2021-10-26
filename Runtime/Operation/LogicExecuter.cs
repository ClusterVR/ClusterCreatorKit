using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClusterVR.CreatorKit.Operation
{
    public interface ILogicStateRepository
    {
        IStateValueSet GetRoomStateValueSet(SourceState state, ItemId itemId);
        void UpdateState(TargetState targetState, ItemId itemId, IStateValueSet stateValueSet, Queue<string> updatedKeys);
    }

    public sealed class LogicExecutor
    {
        readonly ISignalGenerator signalGenerator;
        readonly ILogicStateRepository stateRepository;
        readonly IGimmickUpdater gimmickUpdater;

        public LogicExecutor(ISignalGenerator signalGenerator, ILogicStateRepository stateRepository, IGimmickUpdater gimmickUpdater)
        {
            this.signalGenerator = signalGenerator;
            this.stateRepository = stateRepository;
            this.gimmickUpdater = gimmickUpdater;
        }

        public void Execute(Logic logic, ItemId itemId = default)
        {
            try
            {
                if (logic == null || !logic.IsValid()) return;
                if (!signalGenerator.TryGet(out var signal)) return;
                var updatedKeys = RunLogic(logic, itemId, signal);
                gimmickUpdater.OnStateUpdated(updatedKeys);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
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
            RunSingleStatement(statement.SingleStatement, itemId, updatedKeys, signal);
        }

        void RunSingleStatement(SingleStatement singleStatement, ItemId itemId, Queue<string> updatedKeys, StateValue signal)
        {
            var target = singleStatement.TargetState;
            var parameterType = target.ParameterType;
            if (parameterType == ParameterType.Signal)
            {
                var conditionSatisfied = singleStatement.Expression == null ||
                    EvaluateExpression(singleStatement.Expression, itemId).CastTo(ParameterType.Bool).GetStateValue(FieldName.None).ToBool();
                if (conditionSatisfied)
                {
                    stateRepository.UpdateState(target, itemId, new SignalStateValueSet(signal), updatedKeys);
                }
            }
            else
            {
                var stateValueSet = EvaluateExpression(singleStatement.Expression, itemId);
                stateRepository.UpdateState(target, itemId, stateValueSet.CastTo(parameterType), updatedKeys);
            }
        }

        IStateValueSet EvaluateExpression(Expression expression, ItemId itemId)
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

        IStateValueSet EvaluateValue(Value value, ItemId itemId)
        {
            switch (value.Type)
            {
                case ValueType.Constant:
                    return value.Constant;
                case ValueType.RoomState:
                    return stateRepository.GetRoomStateValueSet(value.SourceState, itemId);
                default:
                    throw new NotImplementedException();
            }
        }

        IStateValueSet Operate(OperatorExpression operatorExpression, ItemId itemId)
        {
            IStateValueSet GetOperand(int index) => EvaluateExpression(operatorExpression.Operands[index], itemId);

            ParameterType GetCommonType(ParameterType left, ParameterType right)
            {
                var hasCommonType = ParameterTypeExtensions.TryGetCommonType(left, right, out var commonType);
                Assert.IsTrue(hasCommonType);
                return commonType;
            }

            bool CastToBoolValue(IStateValueSet stateValueSet) => stateValueSet.GetStateValue(FieldName.None).ToBool();
            double CastToDoubleValue(IStateValueSet stateValueSet) => stateValueSet.GetStateValue(FieldName.None).ToDouble();
            double GetDoubleValue(IStateValueSet stateValueSet, FieldName fieldName) => stateValueSet.GetStateValue(fieldName).ToDouble();
            float GetFloatValue(IStateValueSet stateValueSet, FieldName fieldName) => stateValueSet.GetStateValue(fieldName).ToFloat();

            switch (operatorExpression.Operator)
            {
                case Operator.Not:
                    return new BoolStateValueSet(!CastToBoolValue(GetOperand(0)));
                case Operator.Minus:
                {
                    var operand = GetOperand(0);
                    var result = StateValueSet.Create(operand.ParameterType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(-GetDoubleValue(operand, fieldName)));
                    }
                    return result;
                }
                case Operator.Length:
                {
                    var operand = GetOperand(0);
                    var result = 0d;
                    foreach (var fieldName in operand.GetFieldNames())
                    {
                        var value = GetDoubleValue(operand, fieldName);
                        result += value * value;
                    }
                    return new DoubleStateValueSet(Math.Sqrt(result));
                }
                case Operator.Sqrt:
                {
                    var operand = GetOperand(0);
                    var result = StateValueSet.Create(operand.ParameterType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(Math.Sqrt(GetDoubleValue(operand, fieldName))));
                    }
                    return result;
                }

                case Operator.Equals:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    foreach (var fieldName in operand1.GetFieldNames())
                    {
                        if (!Mathf.Approximately(GetFloatValue(operand1, fieldName), GetFloatValue(operand2, fieldName)))
                        {
                            return new BoolStateValueSet(false);
                        }
                    }
                    return new BoolStateValueSet(true);
                }
                case Operator.NotEquals:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    foreach (var fieldName in operand1.GetFieldNames())
                    {
                        if (!Mathf.Approximately(GetFloatValue(operand1, fieldName), GetFloatValue(operand2, fieldName)))
                        {
                            return new BoolStateValueSet(true);
                        }
                    }
                    return new BoolStateValueSet(false);
                }
                case Operator.GreaterThan:
                    return new BoolStateValueSet(CastToDoubleValue(GetOperand(0)) > CastToDoubleValue(GetOperand(1)));
                case Operator.GreaterThanOrEqual:
                    return new BoolStateValueSet(CastToDoubleValue(GetOperand(0)) >= CastToDoubleValue(GetOperand(1)));
                case Operator.LessThan:
                    return new BoolStateValueSet(CastToDoubleValue(GetOperand(0)) < CastToDoubleValue(GetOperand(1)));
                case Operator.LessThanOrEqual:
                    return new BoolStateValueSet(CastToDoubleValue(GetOperand(0)) <= CastToDoubleValue(GetOperand(1)));

                case Operator.Add:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    var result = StateValueSet.Create(commonType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(GetDoubleValue(operand1, fieldName) + GetDoubleValue(operand2, fieldName)));
                    }
                    return result;
                }
                case Operator.Multiply:
                {
                    IStateValueSet MultiplyVector(IStateValueSet vector, IStateValueSet scalar)
                    {
                        var result = StateValueSet.Create(vector.ParameterType);
                        var value = CastToDoubleValue(scalar);
                        foreach (var fieldName in result.GetFieldNames())
                        {
                            result.Update(fieldName, new StateValue(GetDoubleValue(vector, fieldName) * value));
                        }
                        return result;
                    }

                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    if (operand1.ParameterType == ParameterType.Vector2 || operand1.ParameterType == ParameterType.Vector3)
                    {
                        return MultiplyVector(operand1, operand2);
                    }
                    else if (operand2.ParameterType == ParameterType.Vector2 || operand2.ParameterType == ParameterType.Vector3)
                    {
                        return MultiplyVector(operand2, operand1);
                    }
                    else
                    {
                        var result = StateValueSet.Create(GetCommonType(operand1.ParameterType, operand2.ParameterType));
                        result.Update(FieldName.None, new StateValue(CastToDoubleValue(operand1) * CastToDoubleValue(operand2)));
                        return result;
                    }
                }
                case Operator.Subtract:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    var result = StateValueSet.Create(commonType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(GetDoubleValue(operand1, fieldName) - GetDoubleValue(operand2, fieldName)));
                    }
                    return result;
                }
                case Operator.Divide:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    if (operand1.ParameterType == ParameterType.Vector2 || operand1.ParameterType == ParameterType.Vector3)
                    {
                        var result = StateValueSet.Create(operand1.ParameterType);
                        var value = CastToDoubleValue(operand2);
                        foreach (var fieldName in result.GetFieldNames())
                        {
                            result.Update(fieldName, new StateValue(GetDoubleValue(operand1, fieldName) / value));
                        }
                        return result;
                    }
                    else
                    {
                        var result = StateValueSet.Create(GetCommonType(operand1.ParameterType, operand2.ParameterType));
                        result.Update(FieldName.None, new StateValue(CastToDoubleValue(operand1) / CastToDoubleValue(operand2)));
                        return result;
                    }
                }
                case Operator.Modulo:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    if (operand1.ParameterType == ParameterType.Vector2 || operand1.ParameterType == ParameterType.Vector3)
                    {
                        var result = StateValueSet.Create(operand1.ParameterType);
                        var value = CastToDoubleValue(operand2);
                        foreach (var fieldName in result.GetFieldNames())
                        {
                            result.Update(fieldName, new StateValue(GetDoubleValue(operand1, fieldName) % value));
                        }
                        return result;
                    }
                    else
                    {
                        var result = StateValueSet.Create(GetCommonType(operand1.ParameterType, operand2.ParameterType));
                        result.Update(FieldName.None, new StateValue(CastToDoubleValue(operand1) % CastToDoubleValue(operand2)));
                        return result;
                    }
                }
                case Operator.And:
                    return new BoolStateValueSet(CastToBoolValue(GetOperand(0)) && CastToBoolValue(GetOperand(1)));
                case Operator.Or:
                    return new BoolStateValueSet(CastToBoolValue(GetOperand(0)) || CastToBoolValue(GetOperand(1)));
                case Operator.Dot:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    var result = 0d;
                    foreach (var fieldName in operand1.GetFieldNames())
                    {
                        var value1 = GetDoubleValue(operand1, fieldName);
                        var value2 = GetDoubleValue(operand2, fieldName);
                        result += value1 * value2;
                    }
                    return new DoubleStateValueSet(result);
                }
                case Operator.Cross:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var value1 = operand1.CastTo(ParameterType.Vector3).ToGimmickValue().Vector3Value;
                    var value2 = operand2.CastTo(ParameterType.Vector3).ToGimmickValue().Vector3Value;
                    var result = Vector3.Cross(value1, value2);
                    return new Vector3StateValueSet(result);
                }
                case Operator.Rotate:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    if (operand1.ParameterType == ParameterType.Vector3)
                    {
                        var value1 = operand1.ToGimmickValue().Vector3Value;
                        var value2 = operand2.CastTo(ParameterType.Vector3).ToGimmickValue().Vector3Value;
                        var result = Quaternion.Euler(value1) * value2;
                        return new Vector3StateValueSet(result);
                    }
                    else
                    {
                        var value1 = CastToDoubleValue(operand1) * Mathf.Deg2Rad;
                        var value2 = operand2.CastTo(ParameterType.Vector2).ToGimmickValue().Vector2Value;
                        var sin = Math.Sin(value1);
                        var cos = Math.Cos(value1);
                        var result = new Vector2((float) (value2.x * cos - value2.y * sin), (float) (value2.x * sin + value2.y * cos));
                        return new Vector2StateValueSet(result);
                    }
                }

                case Operator.Condition:
                {
                    var isValid2 = operatorExpression.Operands[1].IsValid(out var type2);
                    Assert.IsTrue(isValid2);
                    var isValid3 = operatorExpression.Operands[2].IsValid(out var type3);
                    Assert.IsTrue(isValid3);
                    var result = CastToBoolValue(GetOperand(0)) ?
                        GetOperand(1) :
                        GetOperand(2);
                    return result.CastTo(GetCommonType(type2, type3));
                }

                case Operator.Min:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    var result = StateValueSet.Create(commonType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(Math.Min(GetDoubleValue(operand1, fieldName), GetDoubleValue(operand2, fieldName))));
                    }
                    return result;
                }
                case Operator.Max:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var commonType = GetCommonType(operand1.ParameterType, operand2.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    var result = StateValueSet.Create(commonType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        result.Update(fieldName, new StateValue(Math.Max(GetDoubleValue(operand1, fieldName), GetDoubleValue(operand2, fieldName))));
                    }
                    return result;
                }
                case Operator.Clamp:
                {
                    var operand1 = GetOperand(0);
                    var operand2 = GetOperand(1);
                    var operand3 = GetOperand(2);
                    var commonType = GetCommonType(GetCommonType(operand1.ParameterType, operand2.ParameterType), operand3.ParameterType);
                    operand1 = operand1.CastTo(commonType);
                    operand2 = operand2.CastTo(commonType);
                    operand3 = operand3.CastTo(commonType);
                    var result = StateValueSet.Create(commonType);
                    foreach (var fieldName in result.GetFieldNames())
                    {
                        var value = Math.Min(Math.Max(GetDoubleValue(operand1, fieldName), GetDoubleValue(operand2, fieldName)), GetDoubleValue(operand3, fieldName));
                        result.Update(fieldName, new StateValue(value));
                    }
                    return result;
                }
                default: throw new NotImplementedException();
            }
        }
    }
}
