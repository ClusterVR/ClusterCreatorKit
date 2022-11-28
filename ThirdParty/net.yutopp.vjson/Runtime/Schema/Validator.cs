//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;

namespace VJson.Schema
{
    using Internal;

    public sealed class JsonSchemaValidator
    {
        readonly JsonSchema _schema;

        public JsonSchemaValidator(JsonSchema j)
        {
            _schema = j;
        }

        public ConstraintsViolationException Validate(object o, JsonSchemaRegistry reg = null)
        {
            return Validate(o, new State(), reg);
        }

        internal ConstraintsViolationException Validate(object o, State state, JsonSchemaRegistry reg)
        {
            if (_schema.Ref != null)
            {
                var schema = reg?.Resolve(_schema.Ref);
                if (schema == null)
                {
                    throw new Exception($"Schema is not registered or registory is null: Ref={_schema.Ref}");
                }
                return schema.Validate(o, state, reg);
            }

            ConstraintsViolationException ex = null;

            if (o is INode) {
                // unwrap INode
                return Validate((o as INode).GenericContent, state, reg);
            }
            var kind = Node.KindOfValue(o);

            if (_schema.Type != null)
            {
                if (_schema.Type.GetType().IsArray)
                {
                    var ts = (string[])_schema.Type;
                    var found = false;
                    foreach (var t in ts)
                    {
                        if (ValidateKind(kind, t))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        var actual = kind.ToString();
                        var expected = String.Join(", ", ts);
                        var msg = state.CreateMessage("Type is not contained(Actual: {0}; Expected: [{1}])",
                                                      actual, expected);
                        return new ConstraintsViolationException(msg);
                    }
                }
                else
                {
                    var t = (string)_schema.Type;
                    if (!ValidateKind(kind, t))
                    {
                        var actual = kind.ToString();
                        var expected = t.ToString();
                        var msg = state.CreateMessage("Type is not matched(Actual: {0}; Expected: {1})",
                                                      actual, expected);
                        return new ConstraintsViolationException(msg);
                    }
                }
            }

            if (_schema.Enum != null)
            {
                var oEnum = o;
                if (o != null && o.GetType().IsEnum && kind == NodeKind.String)
                {
                    oEnum = TypeHelper.GetStringEnumNameOf(o);
                }

                var found = false;
                foreach (var e in _schema.Enum)
                {
                    if (TypeHelper.DeepEquals(oEnum, e))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    var msg = state.CreateMessage("Enum is not matched");
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.Not != null)
            {
                ex = _schema.Not.Validate(o, state, reg);
                if (ex == null)
                {
                    var msg = state.CreateMessage("Not");
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.AllOf != null)
            {
                var i = 0;
                foreach (var jsonSchema in _schema.AllOf)
                {
                    ex = jsonSchema.Validate(o, state, reg);
                    if (ex != null)
                    {
                        var msg = state.CreateMessage("AllOf[{0}] is failed", i);
                        return new ConstraintsViolationException(msg, ex);
                    }

                    ++i;
                }
            }

            if (_schema.AnyOf != null)
            {
                var schemaChecked = false;
                foreach (var jsonSchema in _schema.AnyOf)
                {
                    ex = jsonSchema.Validate(o, state, reg);
                    if (ex == null)
                    {
                        schemaChecked = true;
                        break;
                    }
                }
                if (!schemaChecked)
                {
                    var msg = state.CreateMessage("None of AnyOf is matched");
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.OneOf != null)
            {
                var checkedI = -1;
                var i = 0;
                foreach (var jsonSchema in _schema.OneOf)
                {
                    ex = jsonSchema.Validate(o, state, reg);
                    if (ex == null)
                    {
                        if (checkedI != -1)
                        {
                            var msg = state.CreateMessage("Both of OneOf[{0}] and OneOf[{1}] are matched", checkedI, i);
                            return new ConstraintsViolationException(msg);
                        }

                        checkedI = i;
                    }

                    ++i;
                }
                if (checkedI == -1)
                {
                    var msg = state.CreateMessage("None of AnyOf is matched");
                    return new ConstraintsViolationException(msg);
                }
            }

            switch (kind)
            {
                case NodeKind.Boolean:
                    break;

                case NodeKind.Float:
                case NodeKind.Integer:
                    ex = ValidateNumber(Convert.ToDouble(o, CultureInfo.InvariantCulture), state, reg);

                    if (ex != null)
                    {
                        return new ConstraintsViolationException("Number", ex);
                    }
                    break;

                case NodeKind.String:
                    var oConverted =
                        (o != null && o.GetType().IsEnum)
                        ? TypeHelper.GetStringEnumNameOf(o)
                        : (string)o;

                    ex = ValidateString(oConverted, state, reg);
                    if (ex != null)
                    {
                        return new ConstraintsViolationException("String", ex);
                    }
                    break;

                case NodeKind.Array:
                    ex = ValidateArray(TypeHelper.ToIEnumerable(o), state, reg);
                    if (ex != null)
                    {
                        return new ConstraintsViolationException("Array", ex);
                    }
                    break;

                case NodeKind.Object:
                    ex = ValidateObject(o, state, reg);
                    if (ex != null)
                    {
                        return new ConstraintsViolationException("Object", ex);
                    }
                    break;

                case NodeKind.Null:
                    break;

                default:
                    throw new NotImplementedException(kind.ToString());
            }

            return null;
        }

        ConstraintsViolationException ValidateNumber(double v, State state, JsonSchemaRegistry reg)
        {
            if (_schema.MultipleOf != double.MinValue)
            {
                if (_schema.MultipleOf <= 0)
                {
                    throw new InvalidOperationException("MultipleOf must be greater than 0: Value = " + _schema.MultipleOf);
                }

                var b = v / _schema.MultipleOf;
                if (b != Math.Truncate(b))
                {
                    var msg = state.CreateMessage("MultipleOf assertion !({0} % {1} == 0)",
                                                  v, _schema.MultipleOf);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.Maximum != double.MinValue)
            {
                if (!(v <= _schema.Maximum))
                {
                    var msg = state.CreateMessage("Maximum assertion !({0} <= {1})",
                                                  v, _schema.Maximum);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.ExclusiveMaximum != double.MinValue)
            {
                if (!(v < _schema.ExclusiveMaximum))
                {
                    var msg = state.CreateMessage("ExclusiveMaximum assertion !({0} < {1})",
                                                  v, _schema.ExclusiveMaximum);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.Minimum != double.MaxValue)
            {
                if (!(v >= _schema.Minimum))
                {
                    var msg = state.CreateMessage("Minimum assertion !({0} >= {1})",
                                                  v, _schema.Minimum);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.ExclusiveMinimum != double.MaxValue)
            {
                if (!(v > _schema.ExclusiveMinimum))
                {
                    var msg = state.CreateMessage("ExclusiveMinimum assertion !({0} > {1})",
                                                  v, _schema.ExclusiveMinimum);
                    return new ConstraintsViolationException(msg);
                }
            }

            return null;
        }

        ConstraintsViolationException ValidateString(string v, State state, JsonSchemaRegistry reg)
        {
            StringInfo si = null;

            if (_schema.MaxLength != int.MinValue)
            {
                si = si ?? new StringInfo(v);
                if (!(si.LengthInTextElements <= _schema.MaxLength))
                {
                    var msg = state.CreateMessage("MaxLength assertion !({0} <= {1})",
                                                  si.LengthInTextElements, _schema.MaxLength);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.MinLength != int.MaxValue)
            {
                si = si ?? new StringInfo(v);
                if (!(si.LengthInTextElements >= _schema.MinLength))
                {
                    var msg = state.CreateMessage("MinLength assertion !({0} >= {1})",
                                                  si.LengthInTextElements, _schema.MinLength);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.Pattern != null)
            {
                if (!Regex.IsMatch(v, _schema.Pattern))
                {
                    var msg = state.CreateMessage("Pattern assertion !(\"{0}\" matched \"{1}\")",
                                                  v, _schema.Pattern);
                    return new ConstraintsViolationException(msg);
                }
            }

            return null;
        }

        ConstraintsViolationException ValidateArray(IEnumerable<object> vsIter, State state, JsonSchemaRegistry reg)
        {
            var v = vsIter.ToArray();
            var length = v.Length;

            if (_schema.MaxItems != int.MinValue)
            {
                if (!(length <= _schema.MaxItems))
                {
                    var msg = state.CreateMessage("MaxItems assertion !({0} <= {1})",
                                                  length, _schema.MaxItems);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.MinItems != int.MaxValue)
            {
                if (!(length >= _schema.MinItems))
                {
                    var msg = state.CreateMessage("MinItems assertion !({0} >= {1})",
                                                  length, _schema.MinItems);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.UniqueItems)
            {
                // O(N^2) to use DeepEquals...
                for(int i=0; i<v.Length; ++i)
                {
                    for(int j=0; j<v.Length; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (TypeHelper.DeepEquals(v[i], v[j]))
                        {
                            var msg = state.CreateMessage("UniqueItems assertion: Elements at {0} and {1} are duplicated",
                                                          i, j);
                            return new ConstraintsViolationException(msg);
                        }
                    }
                }
            }

            List<object> extraItems = null;

            if (_schema.Items != null)
            {
                if (_schema.Items.GetType().IsArray)
                {
                    var itemSchemas = (JsonSchema[])_schema.Items;

                    var i = 0;
                    foreach (var elem in v)
                    {
                        var itemSchema = itemSchemas.ElementAtOrDefault(i);
                        if (itemSchema == null)
                        {
                            if (extraItems == null)
                            {
                                extraItems = new List<object>();
                            }
                            extraItems.Add(elem);
                            continue;
                        }

                        var ex = itemSchema.Validate(elem, state.NestAsElem(i), reg);
                        if (ex != null)
                        {
                            return new ConstraintsViolationException("Items", ex);
                        }

                        ++i;
                    }
                }
                else
                {
                    var itemSchema = (JsonSchema)_schema.Items;
                    var i = 0;
                    foreach (var elem in v)
                    {
                        var ex = itemSchema.Validate(elem, state.NestAsElem(i), reg);
                        if (ex != null)
                        {
                            return new ConstraintsViolationException("Items", ex);
                        }

                        ++i;
                    }
                }
            }

            if (_schema.AdditionalItems != null)
            {
                if (extraItems != null)
                {
                    foreach (var elem in extraItems)
                    {
                        var ex = _schema.AdditionalItems.Validate(elem, state, reg);
                        if (ex != null)
                        {
                            return new ConstraintsViolationException("AdditionalItems", ex);
                        }
                    }
                }
            }

            return null;
        }

        ConstraintsViolationException ValidateObject(object v, State state, JsonSchemaRegistry reg)
        {
            var validated = new Dictionary<string, object>();

            foreach (var kv in TypeHelper.ToKeyValues(v))
            {
                var ex = ValidateObjectField(kv.Key, kv.Value, state.NestAsElem(kv.Key), reg);
                if (ex != null)
                {
                    return ex;
                }

                validated.Add(kv.Key, kv.Value);
            }

            if (_schema.Required != null)
            {
                var req = new HashSet<string>(_schema.Required);
                req.IntersectWith(validated.Keys);

                if (req.Count != _schema.Required.Count())
                {
                    var actual = String.Join(", ", req.ToArray());
                    var expected = String.Join(", ", _schema.Required);
                    var msg = state.CreateMessage("Lack of required fields(Actual: [{0}]; Expected: [{1}])",
                                                  actual, expected);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.MaxProperties != int.MinValue)
            {
                if (!(validated.Count <= _schema.MaxProperties))
                {
                    var msg = state.CreateMessage("MaxProperties assertion !({0} <= {1})",
                                                  validated.Count, _schema.MaxProperties);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.MinProperties != int.MaxValue)
            {
                if (!(validated.Count >= _schema.MinProperties))
                {
                    var msg = state.CreateMessage("MaxProperties assertion !({0} >= {1})",
                                                  validated.Count, _schema.MinProperties);
                    return new ConstraintsViolationException(msg);
                }
            }

            if (_schema.Dependencies != null)
            {
                var strDep = _schema.Dependencies as Dictionary<string, string[]>;
                if (strDep != null)
                {
                    foreach (var va in validated)
                    {
                        string[] deps = null;
                        if (strDep.TryGetValue(va.Key, out deps))
                        {
                            var intersected = ((string[])deps.Clone()).Intersect(validated.Keys);
                            if (intersected.Count() != deps.Count())
                            {
                                var actual = String.Join(", ", intersected.ToArray());
                                var expected = String.Join(", ", deps);
                                var msg = state.CreateMessage("Dependencies assertion. Lack of depended fields for {0}(Actual: [{1}]; Expected: [{2}])",
                                                              va.Key, actual, expected);
                                return new ConstraintsViolationException(msg);
                            }
                        }
                    }
                    goto depChecked;
                }

                var schemaDep = _schema.Dependencies as Dictionary<string, JsonSchema>;
                if (schemaDep != null)
                {
                    foreach (var va in validated)
                    {
                        JsonSchema ext = null;
                        if (schemaDep.TryGetValue(va.Key, out ext))
                        {
                            var ex = ext.Validate(v, new State().NestAsElem(va.Key), reg);
                            if (ex != null)
                            {
                                // TODO:
                                var msg = state.CreateMessage("Dependencies assertion. Failed to validation for {0}",
                                                              va.Key);
                                return new ConstraintsViolationException(msg, ex);
                            }
                        }
                    }
                }

            depChecked:
                ;
            }

            return null;
        }

        ConstraintsViolationException ValidateObjectField(string key,
                                                          object value,
                                                          State state,
                                                          JsonSchemaRegistry reg)
        {
            var matched = false;

            if (_schema.Properties != null)
            {
                JsonSchema itemSchema = null;
                if (_schema.Properties.TryGetValue(key, out itemSchema))
                {
                    matched = true;

                    var ex = itemSchema.Validate(value, state, reg);
                    if (ex != null)
                    {
                        return new ConstraintsViolationException("Property", ex);
                    }
                }
            }

            if (_schema.PatternProperties != null)
            {
                foreach (var pprop in _schema.PatternProperties)
                {
                    if (Regex.IsMatch(key, pprop.Key))
                    {
                        matched = true;

                        var ex = pprop.Value.Validate(value, state, reg);
                        if (ex != null)
                        {
                            return new ConstraintsViolationException("PatternProperties", ex);
                        }
                    }
                }
            }

            if (_schema.AdditionalProperties != null && !matched)
            {
                var ex = _schema.AdditionalProperties.Validate(value, state, reg);
                if (ex != null)
                {
                    return new ConstraintsViolationException("AdditionalProperties", ex);
                }
            }

            return null;
        }

        /// <summary>
        ///   true if valid
        /// </summary>
        static bool ValidateKind(NodeKind kind, string typeName)
        {
            switch (typeName)
            {
                case "null":
                    return kind == NodeKind.Null;

                case "boolean":
                    return kind == NodeKind.Boolean;

                case "object":
                    return kind == NodeKind.Object;

                case "array":
                    return kind == NodeKind.Array;

                case "number":
                    return kind == NodeKind.Integer || kind == NodeKind.Float;

                case "string":
                    return kind == NodeKind.String;

                case "integer":
                    return kind == NodeKind.Integer;

                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class ConstraintsViolationException : Exception
    {
        public ConstraintsViolationException(string message)
            : base(message)
        {
        }

        public ConstraintsViolationException(string message, ConstraintsViolationException inner)
            : base(String.Format("{0}.{1}", message, inner.Message))
        {
        }
    }
}
