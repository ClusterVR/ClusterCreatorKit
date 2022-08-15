//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VJson
{
    using Internal;

    public sealed class JsonDeserializer
    {
        private Type _expectedInitialType = null;

        public JsonDeserializer(Type type)
        {
            _expectedInitialType = type;
        }

        public object Deserialize(string text)
        {
            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                return Deserialize(s);
            }
        }

        public object Deserialize(Stream s)
        {
            using (var r = new JsonReader(s))
            {
                var node = r.Read();
                return DeserializeFromNode(node);
            }
        }

        public object DeserializeFromNode(INode node)
        {
            return DeserializeValue(node, _expectedInitialType, new State());
        }

        object DeserializeValue(INode node, Type expectedType, State state)
        {
            var expectedKind = Node.KindOfTypeWrapped(expectedType);
            if (expectedKind.Wrapped) {
                expectedType = Node.ValueTypeOfKind(expectedKind.Kind);
            }

            return DeserializeValueAs(node, expectedKind, expectedType, state);
        }

        object DeserializeValueAs(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            switch (targetKind.Kind)
            {
                case NodeKind.Boolean:
                    return DeserializeToBoolean(node, targetKind, targetType, state);

                case NodeKind.Integer:
                case NodeKind.Float:
                    return DeserializeToNumber(node, targetKind, targetType, state);

                case NodeKind.String:
                    return DeserializeToString(node, targetKind, targetType, state);

                case NodeKind.Array:
                    return DeserializeToArray(node, targetKind, targetType, state);

                case NodeKind.Object:
                    return DeserializeToObject(node, targetKind, targetType, state);

                case NodeKind.Null:
                    return DeserializeToNull(node, targetKind, targetType, state);

                default:
                    throw new NotImplementedException("Unmatched kind: " + targetKind.Kind);
            }
        }

        object DeserializeToBoolean(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            if (node is NullNode)
            {
                if (!TypeHelper.IsBoxed(targetType))
                {
                    var msg = state.CreateNodeConversionFailureMessage(node, targetType);
                    throw new DeserializeFailureException(msg);
                }

                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            var bNode = node as BooleanNode;
            if (bNode != null)
            {
                return targetKind.Wrapped
                    ? node
                    : CreateInstanceIfConstrucutable<bool>(targetType, bNode.Value, state);
            }

            var msg0 = state.CreateNodeConversionFailureMessage(node, targetType);
            throw new DeserializeFailureException(msg0);
        }

        object DeserializeToNumber(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            if (node is NullNode)
            {
                if (!TypeHelper.IsBoxed(targetType))
                {
                    var msg = state.CreateNodeConversionFailureMessage(node, targetType);
                    throw new DeserializeFailureException(msg);
                }

                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            var iNode = node as IntegerNode;
            if (iNode != null)
            {
                return targetKind.Wrapped
                    ? node
                    : CreateInstanceIfConstrucutable<long>(targetType, iNode.Value, state);
            }

            var fNode = node as FloatNode;
            if (fNode != null)
            {
                return targetKind.Wrapped
                    ? node
                    : CreateInstanceIfConstrucutable<double>(targetType, fNode.Value, state);
            }

            var msg0 = state.CreateNodeConversionFailureMessage(node, targetType);
            throw new DeserializeFailureException(msg0);
        }

        object DeserializeToString(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            if (node is NullNode)
            {
                if (!TypeHelper.IsBoxed(targetType))
                {
                    var msg = state.CreateNodeConversionFailureMessage(node, targetType);
                    throw new DeserializeFailureException(msg);
                }

                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            var sNode = node as StringNode;
            if (sNode != null)
            {
                return targetKind.Wrapped
                    ? node
                    : CreateInstanceIfConstrucutable<string>(targetType, sNode.Value, state);
            }

            var msg0 = state.CreateNodeConversionFailureMessage(node, targetType);
            throw new DeserializeFailureException(msg0);
        }

        object DeserializeToArray(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            bool isConvertible =
                targetType == typeof(object)
                || (targetType.IsArray)
                || (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(List<>))
                ;
            if (!isConvertible)
            {
                var msg = state.CreateNodeConversionFailureMessage(node, targetType);
                throw new DeserializeFailureException(msg);
            }

            if (node is NullNode)
            {
                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            var aNode = node as ArrayNode;
            if (aNode != null)
            {
                if (targetType.IsArray || targetType == typeof(object))
                {
                    // To Array
                    var conteinerTy = targetType;
                    if (conteinerTy == typeof(object))
                    {
                        conteinerTy = typeof(object[]);
                    }

                    var len = aNode.Elems != null ? aNode.Elems.Count : 0;
                    var container = (Array)Activator.CreateInstance(conteinerTy, new object[] { len });

                    var elemType = conteinerTy.GetElementType();
                    for (int i = 0; i < len; ++i)
                    {
                        var v = DeserializeValue(aNode.Elems[i], elemType, state.NestAsElem(i));
                        container.SetValue(v, i);
                    }

                    return container;
                }
                else
                {
                    // To List
                    var conteinerTy = targetType;

                    var len = aNode.Elems != null ? aNode.Elems.Count : 0;
                    var container = (IList)Activator.CreateInstance(conteinerTy);

                    var elemType = conteinerTy.GetGenericArguments()[0];
                    for (int i = 0; i < len; ++i)
                    {
                        var v = DeserializeValue(aNode.Elems[i], elemType, state.NestAsElem(i));
                        container.Add(v);
                    }

                    if (targetKind.Wrapped) {
                        return new ArrayNode(container as List<INode>);
                    } else {
                        return container;
                    }
                }
            }

            var msg0 = state.CreateNodeConversionFailureMessage(node, targetType);
            throw new DeserializeFailureException(msg0);
        }

        object DeserializeToObject(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            if (targetKind.Kind != NodeKind.Object)
            {
                var msg = state.CreateNodeConversionFailureMessage(node, targetType);
                throw new DeserializeFailureException(msg);
            }

            if (node is NullNode)
            {
                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            var oNode = node as ObjectNode;
            if (oNode != null)
            {
                bool asDictionary =
                    targetType == typeof(object)
                    || (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    ;
                if (asDictionary)
                {
                    // To Dictionary
                    Type containerTy = targetType;
                    if (containerTy == typeof(object))
                    {
                        containerTy = targetKind.Wrapped
                            ? typeof(Dictionary<string, INode>)
                            : typeof(Dictionary<string, object>);
                    }

                    var keyType = containerTy.GetGenericArguments()[0];
                    if (keyType != typeof(string))
                    {
                        throw new NotImplementedException();
                    }

                    var container = (IDictionary)Activator.CreateInstance(containerTy);

                    if (oNode.Elems == null)
                    {
                        goto dictionaryDecoded;
                    }

                    var allElemType = containerTy.GetGenericArguments()[1];
                    foreach (var elem in oNode.Elems)
                    {
                        var elemType = allElemType;

                        // TODO: duplication check
                        var v = DeserializeValue(elem.Value, elemType, state.NestAsElem(elem.Key));
                        container.Add(elem.Key, v);
                    }

                dictionaryDecoded:
                    if (targetKind.Wrapped) {
                        return new ObjectNode(container as Dictionary<string, INode>);
                    } else {
                        return container;
                    }
                }
                else
                {
                    // Mapping to the structure (class | struct)

                    // TODO: add type check
                    var container = Activator.CreateInstance(targetType);
                    var fields = TypeHelper.GetSerializableFields(targetType);
                    foreach (var field in fields)
                    {
                        var attr = TypeHelper.GetCustomAttribute<JsonFieldAttribute>(field);

                        // TODO: duplication check
                        var elemName = JsonFieldAttribute.FieldName(attr, field);

                        INode elem = null;
                        if (oNode.Elems == null || !oNode.Elems.TryGetValue(elemName, out elem))
                        {
                            // TODO: ignore or raise errors?
                            continue;
                        }

                        var elemState = state.NestAsElem(elemName);

                        if (attr != null && attr.TypeHints != null)
                        {
                            bool resolved = false;
                            foreach (var hint in attr.TypeHints)
                            {
                                var elemType = hint;
                                try
                                {
                                    var v = DeserializeValue(elem, elemType, elemState);
                                    field.SetValue(container, v);

                                    resolved = true;
                                    break;
                                }
                                catch (Exception)
                                {
                                }
                            }
                            if (!resolved)
                            {
                                var msg = elemState.CreateNodeConversionFailureMessage(elem, attr.TypeHints);
                                throw new DeserializeFailureException(msg);
                            }
                        }
                        else
                        {
                            var elemType = field.FieldType;

                            var v = DeserializeValue(elem, elemType, elemState);
                            field.SetValue(container, v);
                        }
                    }

                    return container;
                }
            }

            // A json node type is NOT an object but the target type is an object.
            // Thus, change a target kind and retry.
            var nodeKindWrapped = new NodeKindWrapped {
                Kind = node.Kind,
                Wrapped = targetKind.Wrapped,
            };
            if (targetKind.Wrapped) {
                targetType = Node.ValueTypeOfKind(node.Kind);
            }
            return DeserializeValueAs(node, nodeKindWrapped, targetType, state);
        }

        object DeserializeToNull(INode node, NodeKindWrapped targetKind, Type targetType, State state)
        {
            if (node is NullNode)
            {
                // TODO: type check of targetType
                return targetKind.Wrapped
                    ? NullNode.Null
                    : null;
            }

            // TODO: Should raise error?
            throw new NotImplementedException();
        }

        static object CreateInstanceIfConstrucutable<T>(Type targetType, T value, State state)
        {
            // Raw
            if (targetType == typeof(object))
            {
                return value;
            }

            // Is Nullable
            var nullableInnerTy = Nullable.GetUnderlyingType(targetType);
            if (nullableInnerTy != null)
            {
                // Treat as primitive types
                targetType = nullableInnerTy;
            }

            TypeHelper.Converter convFunc;
            if (TypeHelper.GetConverter(typeof(T), targetType, out convFunc))
            {
                if (convFunc == null) // Can return the value as is
                {
                    return value;
                }

                object ret;
                if (convFunc(value, out ret))
                {
                    return ret;
                }

                var msg = state.CreateTypeConversionFailureMessage<T>(value, targetType);
                throw new DeserializeFailureException(msg);
            }

            if (targetType.IsEnum)
            {
                var enumAttr = TypeHelper.GetCustomAttribute<JsonAttribute>(targetType);
                switch (enumAttr != null ? enumAttr.EnumConversion : EnumConversionType.AsInt)
                {
                    case EnumConversionType.AsInt:
                        var enumUnderlyingType = Enum.GetUnderlyingType(targetType);
                        TypeHelper.Converter enumConvFunc;
                        if (!TypeHelper.GetConverter(typeof(T), enumUnderlyingType, out enumConvFunc))
                        {
                            var msg = state.CreateTypeConversionFailureMessage<T>(value, targetType);
                            throw new DeserializeFailureException(msg);
                        }

                        object enumValue;
                        if (enumConvFunc != null)
                        {
                            if (!enumConvFunc(value, out enumValue))
                            {
                                var msg = state.CreateTypeConversionFailureMessage<T>(value, targetType);
                                throw new DeserializeFailureException(msg);
                            }
                        } else {
                            enumValue = value;
                        }

                        if (!Enum.IsDefined(targetType, enumValue))
                        {
                            var msg = state.CreateTypeConversionFailureMessage<T>(value,
                                                                                  targetType,
                                                                                  "Enum value is not defined");
                            throw new DeserializeFailureException(msg);
                        }

                        return Enum.ToObject(targetType, enumValue);

                    case EnumConversionType.AsString:
                        var stringEnumNames = TypeHelper.GetStringEnumNames(targetType);
                        var enumIndex = Array.IndexOf(stringEnumNames, value);
                        if (enumIndex == -1)
                        {
                            var msg = state.CreateTypeConversionFailureMessage<T>(value,
                                                                                  targetType,
                                                                                  "Enum name is not defined");
                            throw new DeserializeFailureException(msg);
                        }

                        return Enum.GetValues(targetType).GetValue(enumIndex);
                }
            }

            // Try to convert value implicitly
            var attr = TypeHelper.GetCustomAttribute<JsonAttribute>(targetType);
            if (attr == null || !attr.ImplicitConstructable)
            {
                var msg = state.CreateTypeConversionFailureMessage<T>(value, targetType, "Not implicit constructable");
                throw new DeserializeFailureException(msg);
            }

            var ctor = targetType.GetConstructor(new[] { typeof(T) });
            if (ctor == null)
            {
                var msg = state.CreateTypeConversionFailureMessage<T>(value,
                                                                      targetType,
                                                                      "Suitable constructers are not found");
                throw new DeserializeFailureException(msg);
            }

            return ctor.Invoke(new object[] { value });
        }
    }

    public class DeserializeFailureException : Exception
    {
        public DeserializeFailureException(string message)
            : base(message)
        {
        }

        public DeserializeFailureException(string message, DeserializeFailureException inner)
            : base(String.Format("{0}.{1}", message, inner.Message))
        {
        }
    }
}
