using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation
{

    [Serializable]
    public sealed class Logic
    {
        [SerializeField] Statement[] statements;
        public Statement[] Statements => statements;

        public Logic()
        {
        }

        public Logic(Statement[] statements)
        {
            this.statements = statements;
        }

        public bool IsValid()
        {
            return statements != null && statements.All(s => s == null || s.IsValid());
        }
    }

    [Serializable]
    public sealed class Statement
    {
        [SerializeField] SingleStatement singleStatement;
        public SingleStatement SingleStatement => singleStatement;

        public Statement()
        {
        }

        public Statement(SingleStatement singleStatement)
        {
            this.singleStatement = singleStatement;
        }

        public bool IsValid()
        {
            return singleStatement != null && singleStatement.IsValid();
        }
    }

    [Serializable]
    public sealed class SingleStatement
    {
        [SerializeField] TargetState targetState;
        [SerializeField] Expression expression;
        public TargetState TargetState => targetState;
        public Expression Expression => expression;

        public SingleStatement()
        {
        }

        public SingleStatement(TargetState targetState, Expression expression)
        {
            this.targetState = targetState;
            this.expression = expression;
        }

        public bool IsValid()
        {
            if (targetState == null || !targetState.IsValid())
            {
                return false;
            }
            if (targetState.ParameterType == ParameterType.Signal)
            {
                return expression == null || (expression.IsValid(out var parameterType) && parameterType.CanCastToValue());
            }
            else
            {
                return expression != null && expression.IsValid(out var parameterType) &&
                    ParameterTypeExtensions.TryGetCommonType(targetState.ParameterType, parameterType, out _);
            }
        }
    }

    [Serializable]
    public sealed class Expression
    {
        [SerializeField] ExpressionType type;
        [SerializeField] Value value;
        [SerializeField] OperatorExpression operatorExpression;
        public ExpressionType Type => type;
        public Value Value => value;
        public OperatorExpression OperatorExpression => operatorExpression;

        public Expression()
        {
        }

        public Expression(Value value)
        {
            type = ExpressionType.Value;
            this.value = value;
        }

        public Expression(OperatorExpression operatorExpression)
        {
            type = ExpressionType.OperatorExpression;
            this.operatorExpression = operatorExpression;
        }

        public bool IsValid(out ParameterType parameterType)
        {
            switch (type)
            {
                case ExpressionType.Value:
                    if (value == null)
                    {
                        parameterType = default;
                        return false;
                    }
                    else
                    {
                        return value.IsValid(out parameterType);
                    }
                case ExpressionType.OperatorExpression:
                    if (operatorExpression == null)
                    {
                        parameterType = default;
                        return false;
                    }
                    else
                    {
                        return operatorExpression.IsValid(out parameterType);
                    }
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
    public sealed class OperatorExpression
    {
        [SerializeField] Operator @operator;

        [SerializeField] Expression[] operands;
        public Operator Operator => @operator;
        public Expression[] Operands => operands;

        public OperatorExpression()
        {
        }

        public OperatorExpression(Operator @operator, Expression[] operands)
        {
            this.@operator = @operator;
            this.operands = operands;
        }

        public bool IsValid(out ParameterType parameterType)
        {
            var requiredLength = @operator.GetRequiredLength();
            if (operands == null || operands.Length < requiredLength)
            {
                parameterType = default;
                return false;
            }

            switch (@operator)
            {
                case Operator.Not:
                {
                    parameterType = ParameterType.Bool;
                    return operands[0].IsValid(out var type) && type.CanCastToValue();
                }
                case Operator.Minus:
                case Operator.Sqrt:
                {
                    return operands[0].IsValid(out parameterType);
                }
                case Operator.Length:
                {
                    parameterType = ParameterType.Double;
                    return operands[0].IsValid(out _);
                }
                case Operator.Add:
                case Operator.Subtract:
                case Operator.Min:
                case Operator.Max:
                {
                    if (!operands[0].IsValid(out var type1) || !operands[1].IsValid(out var type2))
                    {
                        parameterType = default;
                        return false;
                    }
                    else
                    {
                        return ParameterTypeExtensions.TryGetCommonType(type1, type2, out parameterType);
                    }
                }
                case Operator.Multiply:
                {
                    if (operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2))
                    {
                        if (type1.CanCastToVector())
                        {
                            parameterType = type1;
                            return type2.CanCastToValue();
                        }
                        else if(type2.CanCastToVector())
                        {
                            parameterType = type2;
                            return type1.CanCastToValue();
                        }
                        else
                        {
                            return ParameterTypeExtensions.TryGetCommonType(type1, type2, out parameterType);
                        }
                    }
                    else
                    {
                        parameterType = default;
                        return false;
                    }
                }
                case Operator.Modulo:
                case Operator.Divide:
                {
                    if (operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2) && type2.CanCastToValue())
                    {
                        if (type1.CanCastToVector())
                        {
                            parameterType = type1;
                            return true;
                        }
                        else
                        {
                            return ParameterTypeExtensions.TryGetCommonType(type1, type2, out parameterType);
                        }
                    }
                    else
                    {
                        parameterType = default;
                        return false;
                    }
                }
                case Operator.Equals:
                case Operator.NotEquals:
                case Operator.GreaterThan:
                case Operator.GreaterThanOrEqual:
                case Operator.LessThan:
                case Operator.LessThanOrEqual:
                {
                    parameterType = ParameterType.Bool;
                    return operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2) &&
                        ParameterTypeExtensions.TryGetCommonType(type1, type2, out _);
                }
                case Operator.And:
                case Operator.Or:
                {
                    parameterType = ParameterType.Bool;
                    return operands[0].IsValid(out var type1) && type1.CanCastToValue() &&
                        operands[1].IsValid(out var type2) && type2.CanCastToValue();
                }
                case Operator.Dot:
                {
                    parameterType = ParameterType.Double;
                    return operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2) &&
                        ParameterTypeExtensions.TryGetCommonType(type1, type2, out _);
                }
                case Operator.Cross:
                {
                    parameterType = ParameterType.Vector3;
                    return operands[0].IsValid(out var type1) && type1.CanCastToVector() &&
                        operands[1].IsValid(out var type2) && type2.CanCastToVector();
                }
                case Operator.Rotate:
                {
                    if (operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2))
                    {
                        if (type1 == ParameterType.Vector3)
                        {
                            parameterType = ParameterType.Vector3;
                            return type2.CanCastToVector();
                        }
                        else if (type1.CanCastToValue())
                        {
                            parameterType = ParameterType.Vector2;
                            return type2.CanCastToVector();
                        }
                        else
                        {
                            parameterType = default;
                            return false;
                        }
                    }
                    else
                    {
                        parameterType = default;
                        return false;
                    }
                }
                case Operator.Condition:
                {
                    if (operands[0].IsValid(out var type1) && type1.CanCastToValue() &&
                        operands[1].IsValid(out var type2) && operands[2].IsValid(out var type3) &&
                        ParameterTypeExtensions.TryGetCommonType(type2, type3, out parameterType))
                    {
                        return true;
                    }
                    else
                    {
                        parameterType = default;
                        return false;
                    }
                }
                case Operator.Clamp:
                {
                    if (operands[0].IsValid(out var type1) && operands[1].IsValid(out var type2) && operands[2].IsValid(out var type3))
                    {
                        return ParameterTypeExtensions.TryGetCommonType(type1, type2, out parameterType) &&
                            ParameterTypeExtensions.TryGetCommonType(parameterType, type3, out parameterType);
                    }
                    else
                    {
                        parameterType = default;
                        return false;
                    }
                }
                default: throw new NotImplementedException();
            }
        }
    }

    [Serializable]
    public sealed class Value
    {
        [SerializeField] ValueType type;
        [SerializeField] ConstantValue constant;
        [SerializeField] SourceState sourceState;
        public ValueType Type => type;
        public IStateValueSet Constant => constant.StateValueSet;
        public SourceState SourceState => sourceState;

        public Value()
        {
        }

        public Value(ConstantValue constant)
        {
            type = ValueType.Constant;
            this.constant = constant;
        }
        public Value(SourceState sourceState)
        {
            type = ValueType.RoomState;
            this.sourceState = sourceState;
        }

        public bool IsValid(out ParameterType parameterType)
        {
            switch (type)
            {
                case ValueType.Constant:
                    if (constant == null)
                    {
                        parameterType = default;
                        return false;
                    }
                    else
                    {
                        parameterType = constant.Type;
                        return constant.IsValid();
                    }
                case ValueType.RoomState:
                    if (sourceState == null)
                    {
                        parameterType = default;
                        return false;
                    }
                    else
                    {
                        parameterType = sourceState.Type;
                        return sourceState.IsValid();
                    }
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
        Not,
        Minus,
        Add,
        Multiply,
        Subtract,
        Divide,
        Modulo,
        Equals,
        NotEquals,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        And,
        Or,
        Condition,
        Min,
        Max,
        Clamp,
        Length,
        Sqrt,
        Dot,
        Cross,
        Rotate,
    }

    public static class OperatorExtensions
    {
        public static int GetRequiredLength(this Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Not:
                case Operator.Minus:
                case Operator.Length:
                case Operator.Sqrt:
                    return 1;
                case Operator.Add:
                case Operator.Multiply:
                case Operator.Subtract:
                case Operator.Divide:
                case Operator.Modulo:
                case Operator.Equals:
                case Operator.NotEquals:
                case Operator.GreaterThan:
                case Operator.GreaterThanOrEqual:
                case Operator.LessThan:
                case Operator.LessThanOrEqual:
                case Operator.And:
                case Operator.Or:
                case Operator.Min:
                case Operator.Max:
                case Operator.Dot:
                case Operator.Cross:
                case Operator.Rotate:
                    return 2;
                case Operator.Condition:
                case Operator.Clamp:
                    return 3;
                default: throw new NotImplementedException();
            }
        }
    }

    [Serializable]
    public sealed class TargetState
    {
        public static readonly List<ParameterType> SelectableTypes = new List<ParameterType>(6)
            { ParameterType.Signal, ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector2, ParameterType.Vector3 };

        [SerializeField] TargetStateTarget target;
        [SerializeField, StateKeyString] string key;
        [SerializeField] ParameterType parameterType;
        public TargetStateTarget Target => target;
        public string Key => key;
        public ParameterType ParameterType => parameterType;

        public TargetState()
        {
        }

        public TargetState(TargetStateTarget target, string key, ParameterType parameterType)
        {
            this.target = target;
            this.key = key;
            this.parameterType = parameterType;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(key) && SelectableTypes.Contains(parameterType);
        }
    }

    public enum TargetStateTarget
    {
        Item,
        Player,
        Global
    }

    [Serializable]
    public sealed class ConstantValue
    {
        public static readonly List<ParameterType> SelectableTypes = new List<ParameterType>(5)
            { ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector2, ParameterType.Vector3 };

        [SerializeField] ParameterType type = ParameterType.Bool;
        [SerializeField] bool boolValue;
        [SerializeField] float floatValue;
        [SerializeField] int integerValue;
        [SerializeField] Vector2 vector2Value;
        [SerializeField] Vector3 vector3Value;

        public ParameterType Type => type;

        public IStateValueSet StateValueSet
        {
            get
            {
                switch (type)
                {
                    case ParameterType.Bool:
                        return new BoolStateValueSet(boolValue);
                    case ParameterType.Float:
                        return new FloatStateValueSet(floatValue);
                    case ParameterType.Integer:
                        return new IntegerStateValueSet(integerValue);
                    case ParameterType.Vector2:
                        return new Vector2StateValueSet(vector2Value);
                    case ParameterType.Vector3:
                        return new Vector3StateValueSet(vector3Value);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public ConstantValue()
        {
        }

        public ConstantValue(bool boolValue)
        {
            type = ParameterType.Bool;
            this.boolValue = boolValue;
        }

        public ConstantValue(float floatValue)
        {
            type = ParameterType.Float;
            this.floatValue = floatValue;
        }

        public ConstantValue(int integerValue)
        {
            type = ParameterType.Integer;
            this.integerValue = integerValue;
        }

        public ConstantValue(Vector2 vector2Value)
        {
            type = ParameterType.Vector2;
            this.vector2Value = vector2Value;
        }

        public ConstantValue(Vector3 vector3Value)
        {
            type = ParameterType.Vector3;
            this.vector3Value = vector3Value;
        }

        public bool IsValid()
        {
            return SelectableTypes.Contains(type);
        }
    }

    [Serializable]
    public sealed class SourceState
    {
        public static readonly List<ParameterType> SelectableTypes = new List<ParameterType>(6)
            { ParameterType.Double, ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector2, ParameterType.Vector3 };

        [SerializeField] GimmickTarget target;
        [SerializeField, StateKeyString] string key;
        [SerializeField] ParameterType type = ParameterType.Double;

        public GimmickTarget Target => target;
        public string Key => key;
        public ParameterType Type => type;

        public SourceState()
        {
        }

        public SourceState(GimmickTarget target, string key, ParameterType type)
        {
            this.target = target;
            this.key = key;
            this.type = type;
        }

        public bool IsValid()
        {
            var typeIsValid = SelectableTypes.Contains(type);
            var keyIsValid = !string.IsNullOrWhiteSpace(key);
            return typeIsValid && keyIsValid;
        }
    }
}
