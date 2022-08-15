//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VJson
{
    public enum NodeKind
    {
        Object,
        Array,
        String,
        Integer, // Number
        Float,   // Number
        Boolean,
        Null,

        Undefined,
    }

    public struct NodeKindWrapped
    {
        public NodeKind Kind;
        public bool Wrapped;
    }

    public interface INode
    {
        NodeKind Kind { get; }

        INode this[int index] { get; }
        INode this[string key] { get; }

        object GenericContent { get; }
    }

    public sealed class BooleanNode : INode
    {
        public static NodeKind KIND = NodeKind.Boolean;
        public static Type TYPE = typeof(bool);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public bool Value { get; private set; }
        public object GenericContent { get { return Value; } }

        public BooleanNode(bool v)
        {
            Value = v;
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as BooleanNode;
            if (rhs == null)
            {
                return false;
            }

            return Value.Equals(rhs.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return "BOOLEAN: " + Value;
        }
    }

    public sealed class NullNode : INode
    {
        public static NodeKind KIND = NodeKind.Null;
        public static Type TYPE = typeof(object);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public object GenericContent { get { return null; } }

        public static readonly INode Null = new NullNode();

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as NullNode;
            if (rhs == null)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "NULL";
        }
    }

    public sealed class UndefinedNode : INode
    {
        public static NodeKind KIND = NodeKind.Undefined;
        public static Type TYPE = typeof(object);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return Undef; } }
        public INode this[string key] { get { return Undef; } }

        public object GenericContent { get { return null; } }

        public static readonly INode Undef = new UndefinedNode();

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as UndefinedNode;
            if (rhs == null)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "UNDEFINED";
        }
    }

    public sealed class IntegerNode : INode
    {
        public static NodeKind KIND = NodeKind.Integer;
        public static Type TYPE = typeof(long);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public long Value { get; private set; }
        public object GenericContent { get { return Value; } }

        public IntegerNode(long v)
        {
            Value = v;
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as IntegerNode;
            if (rhs == null)
            {
                return false;
            }

            return Value.Equals(rhs.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return "NUMBER(Int): " + Value;
        }
    }

    public sealed class FloatNode : INode
    {
        public static NodeKind KIND = NodeKind.Float;
        public static Type TYPE = typeof(double);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public double Value { get; private set; }
        public object GenericContent { get { return Value; } }

        public FloatNode(double v)
        {
            Value = v;
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as FloatNode;
            if (rhs == null)
            {
                return false;
            }

            return Value.Equals(rhs.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return "NUMBER(Float): " + Value;
        }
    }

    public sealed class StringNode : INode
    {
        public static NodeKind KIND = NodeKind.String;
        public static Type TYPE = typeof(string);

        public NodeKind Kind { get { return KIND; } }

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public string Value { get; private set; }
        public object GenericContent { get { return Value; } }

        public StringNode(string v)
        {
            Value = v;
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as StringNode;
            if (rhs == null)
            {
                return false;
            }

            return Value.Equals(rhs.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return "STRING: " + Value;
        }
    }

    public sealed class ObjectNode : INode, IEnumerable<KeyValuePair<string, INode>>
    {
        public static NodeKind KIND = NodeKind.Object;
        public static Type TYPE = typeof(Dictionary<string, INode>);

        public NodeKind Kind { get { return KIND; } }

        public Dictionary<string, INode> Elems;

        public INode this[int index] { get { return UndefinedNode.Undef; } }
        public INode this[string key]
        {
            get
            {
                INode n = null;
                if (Elems != null)
                {
                    Elems.TryGetValue(key, out n);
                }

                return n != null ? n : UndefinedNode.Undef;
            }
        }

        public object GenericContent { get { return Elems != null ? Elems : new Dictionary<string, INode>(); } }

        public ObjectNode()
        {
        }

        public ObjectNode(Dictionary<string, INode> v)
        {
            Elems = v;
        }

        public void AddElement(string key, INode elem)
        {
            if (Elems == null)
            {
                Elems = new Dictionary<string, INode>();
            }

            Elems.Add(key, elem); // TODO: check duplication
        }

        public void RemoveElement(string key)
        {
            if (Elems == null)
            {
                return;
            }

            Elems.Remove(key);
        }

        public IEnumerator<KeyValuePair<string, INode>> GetEnumerator()
        {
            if (Elems != null)
            {
                return Elems.OrderBy(p => p.Key).GetEnumerator();
            }

            return Enumerable.Empty<KeyValuePair<string, INode>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as ObjectNode;
            if (rhs == null)
            {
                return false;
            }

            if (Elems == null)
            {
                return rhs.Elems == null;
            }

            return Elems.OrderBy(p => p.Key).SequenceEqual(rhs.Elems.OrderBy(p => p.Key));
        }

        public override int GetHashCode()
        {
            if (Elems == null)
            {
                return 0;
            }

            return Elems.GetHashCode();
        }

        public override string ToString()
        {
            if (Elems == null)
            {
                return "OBJECT: {}";
            }

            return "OBJECT: " + String.Join("; ", Elems.OrderBy(p => p.Key).Select(p => p.Key + " = " + p.Value).ToArray());
        }
    }

    public sealed class ArrayNode : INode, IEnumerable<INode>
    {
        public static NodeKind KIND = NodeKind.Array;
        public static Type TYPE = typeof(List<INode>);

        public NodeKind Kind { get { return KIND; } }

        public List<INode> Elems;

        public INode this[int index]
        {
            get
            {
                var elem = Elems != null ? Elems.ElementAtOrDefault(index) : null;
                return elem != null ? elem : UndefinedNode.Undef;
            }
        }
        public INode this[string key] { get { return UndefinedNode.Undef; } }

        public object GenericContent { get { return Elems != null ? Elems : new List<INode>(); } }

        public ArrayNode()
        {
        }

        public ArrayNode(List<INode> v)
        {
            Elems = v;
        }

        public void AddElement(INode elem)
        {
            if (Elems == null)
            {
                Elems = new List<INode>();
            }

            Elems.Add(elem); // TODO: check duplication
        }

        public void RemoveElementAt(int index)
        {
            if (Elems == null)
            {
                return;
            }

            if (index >= 0 && index < Elems.Count)
            {
                Elems.RemoveAt(index);
            }
        }

        public IEnumerator<INode> GetEnumerator()
        {
            if (Elems != null)
            {
                return Elems.GetEnumerator();
            }

            return Enumerable.Empty<INode>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as ArrayNode;
            if (rhs == null)
            {
                return false;
            }

            if (Elems == null)
            {
                return rhs.Elems == null;
            }

            return Elems.SequenceEqual(rhs.Elems);
        }

        public override int GetHashCode()
        {
            if (Elems == null)
            {
                return 0;
            }

            return Elems.GetHashCode();
        }

        public override string ToString()
        {
            if (Elems == null)
            {
                return "ARRAY: []";
            }

            return "ARRAY: " + String.Join("; ", Elems.Select(e => e.ToString()).ToArray());
        }
    }

    public static class Node
    {
        // TODO: optimize
        public static NodeKind KindOfValue<T>(T o)
        {
            if (o == null)
            {
                return NodeKind.Null;
            }

            var ty = o.GetType();
            return KindOfType(ty);
        }

        public static NodeKindWrapped KindOfTypeWrapped(Type ty)
        {
            if (typeof(INode).IsAssignableFrom(ty))
            {
                return new NodeKindWrapped {
                    Kind = _nodeKindTable[ty],
                    Wrapped = true,
                };
            }

            return new NodeKindWrapped {
                Kind = KindOfType(ty),
                Wrapped = false,
            };
        }

        public static NodeKind KindOfType(Type ty)
        {
            // Unwrap all Nullable<T>s
            // Any class values are nullable, however this library does not treat them as nullables.
            // Thus we adjust logic of Nullable<T> to as same as class values. Nullable<T> will be treated as T.
            var optInnerTy = Nullable.GetUnderlyingType(ty);
            if (optInnerTy != null)
            {
                return KindOfType(optInnerTy);
            }

            if (_primitiveTable.TryGetValue(ty, out var k))
            {
                return k;
            }

            // Enum(integer or string)
            if (ty.IsEnum)
            {
                var attr = TypeHelper.GetCustomAttribute<JsonAttribute>(ty);
                return attr != null && attr.EnumConversion == EnumConversionType.AsString
                    ? NodeKind.String
                    : NodeKind.Integer;
            }

            // Arrays
            // If elem type exists, it can treat as Array(IEnumerable)
            var elemTy = TypeHelper.ElemTypeOfIEnumerable(ty);
            if (elemTy != null)
            {
                return NodeKind.Array;
            }

            // Others
            return NodeKind.Object;
        }

        public static Type ValueTypeOfKind(NodeKind kind)
        {
            return _nodeTypeTable[kind];
        }

        static Dictionary<Type, NodeKind> _primitiveTable = new Dictionary<Type, NodeKind>
        {
            {typeof(bool), NodeKind.Boolean},
            {typeof(byte), NodeKind.Integer},
            {typeof(sbyte), NodeKind.Integer},
            {typeof(char), NodeKind.Integer},
            {typeof(decimal), NodeKind.Integer},
            {typeof(double), NodeKind.Float},
            {typeof(float), NodeKind.Float},
            {typeof(int), NodeKind.Integer},
            {typeof(uint), NodeKind.Integer},
            {typeof(long), NodeKind.Integer},
            {typeof(ulong), NodeKind.Integer},
            {typeof(short), NodeKind.Integer},
            {typeof(ushort), NodeKind.Integer},
            {typeof(string), NodeKind.String},
        };

        static Dictionary<Type, NodeKind> _nodeKindTable = new Dictionary<Type, NodeKind>
        {
            {typeof(INode), ObjectNode.KIND}, // Can become any object

            {typeof(BooleanNode), BooleanNode.KIND},
            {typeof(NullNode), NullNode.KIND},
            {typeof(UndefinedNode), UndefinedNode.KIND},
            {typeof(IntegerNode), IntegerNode.KIND},
            {typeof(FloatNode), FloatNode.KIND},
            {typeof(StringNode), StringNode.KIND},
            {typeof(ObjectNode), ObjectNode.KIND},
            {typeof(ArrayNode), ArrayNode.KIND},
        };

        static Dictionary<NodeKind, Type> _nodeTypeTable = new Dictionary<NodeKind, Type>
        {
            {BooleanNode.KIND, BooleanNode.TYPE},
            {NullNode.KIND, NullNode.TYPE},
            {UndefinedNode.KIND, UndefinedNode.TYPE},
            {IntegerNode.KIND, IntegerNode.TYPE},
            {FloatNode.KIND, FloatNode.TYPE},
            {StringNode.KIND, StringNode.TYPE},
            {ObjectNode.KIND, ObjectNode.TYPE},
            {ArrayNode.KIND, ArrayNode.TYPE},
        };
    }
}
