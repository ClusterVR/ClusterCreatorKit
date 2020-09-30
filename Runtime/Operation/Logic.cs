using System;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation
{
    [Serializable]
    public class Logic
    {
        [SerializeField] Statement[] statements;
        public Statement[] Statements => statements;

        public bool IsValid()
        {
            return statements != null && statements.All(s => s == null || s.IsValid());
        }
    }

    [Serializable]
    public class Statement
    {
        [SerializeField] SingleStatement singleStatement;
        public SingleStatement SingleStatement => singleStatement;

        public bool IsValid() => singleStatement != null && singleStatement.IsValid();
    }

    [Serializable]
    public class SingleStatement
    {
        [SerializeField] TargetState targetState;
        [SerializeField] Expression expression;
        public TargetState TargetState => targetState;
        public Expression Expression => expression;

        public bool IsValid()
        {
            return targetState != null && targetState.IsValid() &&
                   (targetState.ParameterType == ParameterType.Signal ?
                       expression == null || expression.IsValid():
                       expression != null && expression.IsValid());
        }
    }

    [Serializable]
    public class Expression
    {
        [SerializeField] ExpressionType type;
        [SerializeField] Value value;
        [SerializeField] OperatorExpression operatorExpression;
        public ExpressionType Type => type;
        public Value Value => value;
        public OperatorExpression OperatorExpression => operatorExpression;

        public bool IsValid()
        {
            switch (type)
            {
                case ExpressionType.Value:
                    return value != null && value.IsValid();
                case ExpressionType.OperatorExpression:
                    return operatorExpression != null && operatorExpression.IsValid();
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum ExpressionType
    {
        Value,
        OperatorExpression
    }

    [Serializable]
    public class OperatorExpression
    {
        [SerializeField] Operator @operator;

        [SerializeField] Expression[] operands;
        public Operator Operator => @operator;
        public Expression[] Operands => operands;

        public bool IsValid()
        {
            var requiredLength = @operator.GetRequiredLength();
            return operands != null && operands.Length >= requiredLength &&
                   operands.Take(requiredLength).All(o => o != null && o.IsValid());
        }
    }

    [Serializable]
    public class Value
    {
        [SerializeField] ValueType type;
        [SerializeField] ConstantValue constant;
        [SerializeField] SourceState sourceState;
        public ValueType Type => type;
        public StateValue Constant => constant.StateValue;
        public SourceState SourceState => sourceState;

        public bool IsValid()
        {
            switch (type)
            {
                case ValueType.Constant:
                    return constant != null && constant.IsValid();
                case ValueType.RoomState:
                    return sourceState != null && sourceState.IsValid();
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum ValueType
    {
        Constant,
        RoomState
    }

    public enum Operator
    {
        Not, Minus, Add, Multiply, Subtract, Divide, Modulo, Equals, NotEquals, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, And, Or, Condition, Min, Max, Clamp,
    }

    public static class OperatorExtensions
    {
        public static int GetRequiredLength(this Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Not: case Operator.Minus:
                    return 1;
                case Operator.Add: case Operator.Multiply: case Operator.Subtract: case Operator.Divide: case Operator.Modulo:
                case Operator.Equals: case Operator.NotEquals: case Operator.GreaterThan: case Operator.GreaterThanOrEqual:
                case Operator.LessThan: case Operator.LessThanOrEqual:
                case Operator.And: case Operator.Or:
                case Operator.Min: case Operator.Max:
                    return 2;
                case Operator.Condition:
                case Operator.Clamp:
                    return 3;
                default: throw new NotImplementedException();
            }
        }
    }

    [Serializable]
    public class TargetState
    {
        [SerializeField] TargetStateTarget target;
        [SerializeField] string key;
        [SerializeField] ParameterType parameterType;
        public TargetStateTarget Target => target;
        public string Key => key;
        public ParameterType ParameterType => parameterType;

        public bool IsValid() => !string.IsNullOrWhiteSpace(key);
    }

    public enum TargetStateTarget
    {
        Item,
        Player,
        Global
    }

    [Serializable]
    public class ConstantValue
    {
        [SerializeField] ParameterType type = ParameterType.Bool;
        [SerializeField] bool boolValue;
        [SerializeField] float floatValue;
        [SerializeField] int integerValue;

        public StateValue StateValue
        {
            get
            {
                switch (type)
                {
                    case ParameterType.Bool:
                        return new StateValue(boolValue);
                    case ParameterType.Float:
                        return new StateValue(floatValue);
                    case ParameterType.Integer:
                        return new StateValue(integerValue);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public bool IsValid() => type == ParameterType.Bool || type == ParameterType.Float || type == ParameterType.Integer;
    }

    [Serializable]
    public class SourceState
    {
        [SerializeField] GimmickTarget target;
        [SerializeField] string key;
        public GimmickTarget Target => target;
        public string Key => key;

        public bool IsValid() => !string.IsNullOrWhiteSpace(key);
    }
}
