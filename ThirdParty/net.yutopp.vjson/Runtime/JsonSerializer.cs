//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace VJson
{
    public sealed class JsonSerializer
    {
        private Type _type;

        public JsonSerializer(Type type)
        {
            this._type = type;
        }

        #region Serializer

        public void Serialize<T>(Stream s, T o, int indent = 0)
        {
            using (var w = new JsonWriter(s, indent))
            {
                SerializeValue(w, o);
            }
        }

        public string Serialize<T>(T o, int indent = 0)
        {
            using (var s = new MemoryStream())
            {
                Serialize(s, o, indent);
                return Encoding.UTF8.GetString(s.ToArray());
            }
        }

        public INode SerializeToNode<T>(T o)
        {
            // TODO: fix performance...
            byte[] buffer = null;
            using (var s = new MemoryStream())
            {
                Serialize<T>(s, o, 0);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var d = new JsonDeserializer(typeof(INode));
                return d.Deserialize(s) as INode;
            }
        }

        void SerializeValue<T>(JsonWriter writer, T o)
        {
            if (o != null && o is INode) {
                // unwrap INode
                SerializeValue(writer, (o as INode).GenericContent);
                return;
            }
            var kind = Node.KindOfValue(o);

            switch (kind)
            {
                case NodeKind.String:
                case NodeKind.Integer:
                case NodeKind.Float:
                case NodeKind.Boolean:
                    SerializePrimitive(writer, o);
                    return;
                case NodeKind.Array:
                    SerializeArray(writer, o);
                    return;
                case NodeKind.Object:
                    SerializeObject(writer, o);
                    return;
                case NodeKind.Null:
                    SerializeNull(writer, o);
                    return;
            }
        }

        static readonly Dictionary<Type, Action<JsonWriter, object>> writeActionMap = new Dictionary<Type, Action<JsonWriter, object>>()
        {
            {typeof(short), (writer, v) => writer.WriteValue((short)v)},
            {typeof(ushort), (writer, v) => writer.WriteValue((ushort)v)},
            {typeof(int), (writer, v) => writer.WriteValue((int)v)},
            {typeof(uint), (writer, v) => writer.WriteValue((uint)v)},
            {typeof(long), (writer, v) => writer.WriteValue((long)v)},
            {typeof(ulong), (writer, v) => writer.WriteValue((ulong)v)},
            {typeof(float), (writer, v) => writer.WriteValue((float)v)},
            {typeof(double), (writer, v) => writer.WriteValue((double)v)},
            {typeof(bool), (writer, v) => writer.WriteValue((bool)v)},
            {typeof(byte), (writer, v) => writer.WriteValue((byte)v)},
            {typeof(sbyte), (writer, v) => writer.WriteValue((sbyte)v)},
            {typeof(char), (writer, v) => writer.WriteValue((char)v)},
            {typeof(string), (writer, v) => writer.WriteValue((string)v)},
            {typeof(decimal), (writer, v) => writer.WriteValue((decimal)v)},
        };

        void SerializePrimitive<T>(JsonWriter writer, T o)
        {
            var ty = o.GetType();
            if (ty.IsEnum)
            {
                var attr = TypeHelper.GetCustomAttribute<JsonAttribute>(ty);
                switch (attr != null ? attr.EnumConversion : EnumConversionType.AsInt)
                {
                    case EnumConversionType.AsInt:
                        // Convert to simple integer
                        SerializeValue(writer, Convert.ChangeType(o, Enum.GetUnderlyingType(ty)));
                        break;

                    case EnumConversionType.AsString:
                        SerializeValue(writer, TypeHelper.GetStringEnumNameOf(o));
                        break;
                }

                return;
            }

            if (writeActionMap.TryGetValue(ty, out var write))
            {
                write(writer, o);
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "SerializePrimitive method require primitive type, but {0} is not primitive type ({1})",
                        o,
                        ty.ToString()
                    )
                );
            }
        }

        void SerializeArray<T>(JsonWriter writer, T o)
        {
            writer.WriteArrayStart();

            foreach (var elem in TypeHelper.ToIEnumerable(o))
            {
                SerializeValue(writer, elem);
            }

            writer.WriteArrayEnd();
        }

        void SerializeObject<T>(JsonWriter writer, T o)
        {
            writer.WriteObjectStart();

            foreach (var kv in TypeHelper.ToKeyValues(o))
            {
                writer.WriteObjectKey(kv.Key);
                SerializeValue(writer, kv.Value);
            }

            writer.WriteObjectEnd();
        }

        void SerializeNull<T>(JsonWriter writer, T o)
        {
            writer.WriteValueNull();
        }
        #endregion

        #region Deserializer
        public object Deserialize(String text)
        {
            var d = new JsonDeserializer(_type);
            return d.Deserialize(text);
        }

        public object Deserialize(Stream s)
        {
            var d = new JsonDeserializer(_type);
            return d.Deserialize(s);
        }
        #endregion
    }
}
