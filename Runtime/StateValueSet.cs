using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClusterVR.CreatorKit
{
    public interface IStateValueSet
    {
        ParameterType ParameterType { get; }
        IEnumerable<FieldName> GetFieldNames();
        void Update(string key, StateValue value);
        void Update(FieldName fieldName, StateValue value);
        IEnumerable<KeyValuePair<FieldName, StateValue>> ToFieldStateValues();
        GimmickValue ToGimmickValue();
        StateValue GetStateValue(FieldName fieldName);
    }

    public sealed class SignalStateValueSet : IStateValueSet
    {
        StateValue value;

        public SignalStateValueSet()
        {
            value = StateValue.Default;
        }

        public SignalStateValueSet(StateValue value)
        {
            this.value = value;
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Signal;

        IEnumerable<FieldName> IStateValueSet.GetFieldNames()
        {
            yield return FieldName.None;
        }

        void IStateValueSet.Update(string key, StateValue value) => Update(value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(value);

        void Update(StateValue value)
        {
            this.value = value;
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            yield return new KeyValuePair<FieldName, StateValue>(FieldName.None, value);
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(value.ToDateTime());
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            return value;
        }
    }

    public sealed class BoolStateValueSet : IStateValueSet
    {
        bool value;

        public BoolStateValueSet()
        {
        }

        public BoolStateValueSet(bool value)
        {
            this.value = value;
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Bool;

        IEnumerable<FieldName> IStateValueSet.GetFieldNames()
        {
            yield return FieldName.None;
        }

        void IStateValueSet.Update(string key, StateValue value) => Update(value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(value);

        void Update(StateValue value)
        {
            this.value = value.ToBool();
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            yield return new KeyValuePair<FieldName, StateValue>(FieldName.None, new StateValue(value));
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(value);
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            return new StateValue(value);
        }
    }

    public sealed class FloatStateValueSet : IStateValueSet
    {
        float value;

        public FloatStateValueSet()
        {
        }

        public FloatStateValueSet(float value)
        {
            this.value = value;
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Float;

        IEnumerable<FieldName> IStateValueSet.GetFieldNames()
        {
            yield return FieldName.None;
        }

        void IStateValueSet.Update(string key, StateValue value) => Update(value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(value);

        void Update(StateValue value)
        {
            this.value = value.ToFloat();
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            yield return new KeyValuePair<FieldName, StateValue>(FieldName.None, new StateValue(value));
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(value);
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            return new StateValue(value);
        }
    }

    public sealed class IntegerStateValueSet : IStateValueSet
    {
        int value;

        public IntegerStateValueSet()
        {
        }

        public IntegerStateValueSet(int value)
        {
            this.value = value;
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Integer;

        IEnumerable<FieldName> IStateValueSet.GetFieldNames()
        {
            yield return FieldName.None;
        }

        void IStateValueSet.Update(string key, StateValue value) => Update(value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(value);

        void Update(StateValue value)
        {
            this.value = value.ToInt();
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            yield return new KeyValuePair<FieldName, StateValue>(FieldName.None, new StateValue(value));
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(value);
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            return new StateValue(value);
        }
    }

    public sealed class Vector2StateValueSet : IStateValueSet
    {
        static readonly FieldName FieldNameX = new FieldName("x");
        static readonly FieldName FieldNameY = new FieldName("y");

        static readonly FieldName[] FieldNames = { FieldNameX, FieldNameY };

        readonly Dictionary<FieldName, StateValue> fieldValues = new Dictionary<FieldName, StateValue>(2)
        {
            { FieldNameX, StateValue.Default },
            { FieldNameY, StateValue.Default },
        };

        public Vector2StateValueSet()
        {
        }

        public Vector2StateValueSet(Vector2 value)
        {
            fieldValues[FieldNameX] = new StateValue(value.x);
            fieldValues[FieldNameY] = new StateValue(value.y);
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Vector2;

        Vector2 GetValue()
        {
            return new Vector2(fieldValues[FieldNameX].ToFloat(), fieldValues[FieldNameY].ToFloat());
        }

        IEnumerable<FieldName> IStateValueSet.GetFieldNames() => FieldNames;

        void IStateValueSet.Update(string key, StateValue value) => Update(new FieldName(key), value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(fieldName, value);

        void Update(FieldName fieldName, StateValue value)
        {
            Assert.IsTrue(fieldValues.ContainsKey(fieldName));
            fieldValues[fieldName] = value;
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            return fieldValues;
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(GetValue());
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            Assert.IsTrue(fieldValues.ContainsKey(fieldName));
            return fieldValues[fieldName];
        }
    }

    public sealed class Vector3StateValueSet : IStateValueSet
    {
        static readonly FieldName FieldNameX = new FieldName("x");
        static readonly FieldName FieldNameY = new FieldName("y");
        static readonly FieldName FieldNameZ = new FieldName("z");

        static readonly FieldName[] FieldNames = { FieldNameX, FieldNameY, FieldNameZ };

        readonly Dictionary<FieldName, StateValue> fieldValues = new Dictionary<FieldName, StateValue>(3)
        {
            { FieldNameX, StateValue.Default },
            { FieldNameY, StateValue.Default },
            { FieldNameZ, StateValue.Default },
        };

        public Vector3StateValueSet()
        {
        }

        public Vector3StateValueSet(Vector3 value)
        {
            fieldValues[FieldNameX] = new StateValue(value.x);
            fieldValues[FieldNameY] = new StateValue(value.y);
            fieldValues[FieldNameZ] = new StateValue(value.z);
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Vector3;

        Vector3 GetValue()
        {
            return new Vector3(fieldValues[FieldNameX].ToFloat(),
                fieldValues[FieldNameY].ToFloat(),
                fieldValues[FieldNameZ].ToFloat());
        }

        IEnumerable<FieldName> IStateValueSet.GetFieldNames() => FieldNames;

        void IStateValueSet.Update(string key, StateValue value) => Update(new FieldName(key), value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(fieldName, value);

        void Update(FieldName fieldName, StateValue value)
        {
            Assert.IsTrue(fieldValues.ContainsKey(fieldName));
            fieldValues[fieldName] = value;
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            return fieldValues;
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            return new GimmickValue(GetValue());
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            Assert.IsTrue(fieldValues.ContainsKey(fieldName));
            return fieldValues[fieldName];
        }
    }

    public sealed class DoubleStateValueSet : IStateValueSet
    {
        double value;

        public DoubleStateValueSet()
        {
        }

        public DoubleStateValueSet(double value)
        {
            this.value = value;
        }

        ParameterType IStateValueSet.ParameterType => ParameterType.Double;

        IEnumerable<FieldName> IStateValueSet.GetFieldNames()
        {
            yield return FieldName.None;
        }

        void IStateValueSet.Update(string key, StateValue value) => Update(value);

        void IStateValueSet.Update(FieldName fieldName, StateValue value) => Update(value);

        void Update(StateValue value)
        {
            this.value = value.ToDouble();
        }

        IEnumerable<KeyValuePair<FieldName, StateValue>> IStateValueSet.ToFieldStateValues()
        {
            yield return new KeyValuePair<FieldName, StateValue>(FieldName.None, new StateValue(value));
        }

        GimmickValue IStateValueSet.ToGimmickValue()
        {
            throw new NotImplementedException();
        }

        StateValue IStateValueSet.GetStateValue(FieldName fieldName)
        {
            return new StateValue(value);
        }
    }

    public static class StateValueSet
    {
        public static IStateValueSet Create(ParameterType type)
        {
            switch (type)
            {
                case ParameterType.Signal:
                    return new SignalStateValueSet();
                case ParameterType.Bool:
                    return new BoolStateValueSet();
                case ParameterType.Integer:
                    return new IntegerStateValueSet();
                case ParameterType.Float:
                    return new FloatStateValueSet();
                case ParameterType.Vector2:
                    return new Vector2StateValueSet();
                case ParameterType.Vector3:
                    return new Vector3StateValueSet();
                case ParameterType.Double:
                    return new DoubleStateValueSet();
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public static class StateValueSetExtensions
    {
        public static IEnumerable<KeyValuePair<string, StateValue>> ToTriggerStates(this IStateValueSet stateValueSet, string keyPrefix, string rawKey)
        {
            return stateValueSet.ToFieldStateValues()
                .Select(f => new KeyValuePair<string, StateValue>(keyPrefix + f.Key.BuildKey(rawKey), f.Value));
        }

        public static IStateValueSet CastTo(this IStateValueSet stateValueSet, ParameterType parameterType)
        {
            if (stateValueSet.ParameterType == parameterType) return stateValueSet;
            var result = StateValueSet.Create(parameterType);
            foreach (var fieldName in stateValueSet.GetFieldNames().Intersect(result.GetFieldNames()))
            {
                result.Update(fieldName, stateValueSet.GetStateValue(fieldName));
            }
            return result;
        }
    }
}
