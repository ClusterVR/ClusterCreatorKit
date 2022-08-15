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
using System.Reflection;

namespace VJson
{
    static partial class TypeHelper
    {
        public static bool IsBoxed(Type ty)
        {
            if (ty.IsClass) {
                return true;
            }

            var optInnerTy = Nullable.GetUnderlyingType(ty);
            return optInnerTy != null;
        }

        public static T GetCustomAttribute<T>(FieldInfo fi) where T : Attribute
        {
            return (T)fi.GetCustomAttributes(typeof(T), false)
                .Where(a => a.GetType() == typeof(T))
                .FirstOrDefault();
        }

        public static T GetCustomAttribute<T>(Type ty) where T : Attribute
        {
            return (T)ty.GetCustomAttributes(typeof(T), false)
                .Where(a => a.GetType() == typeof(T))
                .FirstOrDefault();
        }

        // TODO: implement cache
        public static string[] GetStringEnumNames(Type ty)
        {
            var enumFields = ty.GetFields(BindingFlags.Static|BindingFlags.Public);
            return enumFields.Select(fi => {
                    var attr = GetCustomAttribute<JsonFieldAttribute>(fi);
                    if (attr != null && attr.Name != null) {
                        return attr.Name;
                    }

                    return fi.Name;
                }).ToArray();
        }

        public static string GetStringEnumNameOf(object e)
        {
            var eTy = e.GetType();
            var enumIndex = Array.IndexOf(Enum.GetValues(eTy), e);

            return GetStringEnumNames(eTy)[enumIndex];
        }

        public static IEnumerable<object> ToIEnumerable(object o)
        {
            var ty = o.GetType();
            if (ty.IsArray)
            {
                if (ty.HasElementType && ty.GetElementType().IsClass)
                {
                    return ((IEnumerable<object>)o);
                }
                else
                {
                    return ((IEnumerable)o).Cast<object>();
                }
            }
            else
            {
                return ((IEnumerable)o).Cast<object>();
            }
        }

        public static Type ElemTypeOfIEnumerable(Type ty)
        {
            if (ty.IsArray)
            {
                if (ty.HasElementType)
                {
                    return ty.GetElementType();
                }

                return null;
            }

            if (ty.IsGenericType && ty.GetGenericTypeDefinition() == typeof(List<>))
            {
                return ty.GetGenericArguments()[0];
            }

            return null;
        }

        public static IEnumerable<KeyValuePair<string, object>> ToKeyValues(object o)
        {
            var pairs = ToRankedKeyValuesUnordered(o).ToList();
            pairs.Sort(new RankedKeyValueComparer());

            return pairs.Select(v => new KeyValuePair<string, object>(v.Key.Key, v.Value));
        }

        public static IEnumerable<FieldInfo> GetSerializableFields(Type ty)
        {
            var publicFields = ty.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var privateFields = ty.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(field => {
                return GetCustomAttribute<JsonFieldAttribute>(field) != null;
            });
            return publicFields.Concat(privateFields);
        }

        private struct RankedKey
        {
            public int Order;
            public string Key;
        }

        class RankedKeyValueComparer : IComparer<KeyValuePair<RankedKey, object>>
        {
            public int Compare(KeyValuePair<RankedKey, object> a, KeyValuePair<RankedKey, object> b)
            {
                throw new NotImplementedException();
            }

            int IComparer<KeyValuePair<RankedKey, object>>.Compare(KeyValuePair<RankedKey, object> a,
                                                                   KeyValuePair<RankedKey, object> b)
            {
                if (a.Key.Order != b.Key.Order)
                {
                    return a.Key.Order - b.Key.Order; // TODO: Concider when values are overflowed...
                }

                return string.Compare(a.Key.Key, b.Key.Key);
            }
        }

        private static IEnumerable<KeyValuePair<RankedKey, object>> ToRankedKeyValuesUnordered(object o)
        {
            var ty = o.GetType();
            if (ty.IsGenericType && ty.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var keyType = ty.GetGenericArguments()[0];
                if (keyType != typeof(string))
                {
                    // TODO: Should allow them and call `ToString`?
                    throw new NotImplementedException();
                }

                foreach (DictionaryEntry elem in (IDictionary)o)
                {
                    yield return new KeyValuePair<RankedKey, object>(
                        new RankedKey{
                            Order = 0, // Dictionary has no order infomation
                            Key = (string)elem.Key,
                        },
                        elem.Value);
                }
            }
            else
            {
                var fields = GetSerializableFields(ty);
                foreach (var field in fields)
                {
                    var fieldAttr = GetCustomAttribute<JsonFieldAttribute>(field);

                    // TODO: duplication check
                    var elemName = JsonFieldAttribute.FieldName(fieldAttr, field);
                    var elemValue = field.GetValue(o);

                    var fieldIgnoreAttr = GetCustomAttribute<JsonFieldIgnorableAttribute>(field);
                    if (JsonFieldIgnorableAttribute.IsIgnorable(fieldIgnoreAttr, elemValue))
                    {
                        continue;
                    }

                    yield return new KeyValuePair<RankedKey, object>(
                        new RankedKey{
                            Order = JsonFieldAttribute.FieldOrder(fieldAttr),
                            Key = elemName,
                        },
                        elemValue);
                }
            }
        }

        class DeepEqualityComparer : EqualityComparer<object>
        {
            public override bool Equals(object a, object b)
            {
                return DeepEquals(a, b);
            }

            public override int GetHashCode(object a)
            {
                return a.GetHashCode();
            }
        }

        public static bool DeepEquals(object lhs, object rhs)
        {
            var lhsKind = Node.KindOfValue(lhs);
            var rhsKind = Node.KindOfValue(rhs);
            if (lhsKind != rhsKind)
            {
                return false;
            }

            switch (lhsKind)
            {
                case NodeKind.Boolean:
                case NodeKind.Integer:
                case NodeKind.Float:
                case NodeKind.String:
                    return Object.Equals(lhs, rhs);

                case NodeKind.Array:
                    var lhsArr = ToIEnumerable(lhs);
                    var rhsArr = ToIEnumerable(rhs);
                    return lhsArr.SequenceEqual(rhsArr, new DeepEqualityComparer());

                case NodeKind.Object:
#if NETCOREAPP2_0
                    var lhsKvs = new Dictionary<string, object>(ToKeyValues(lhs));
                    var rhsKvs = new Dictionary<string, object>(ToKeyValues(rhs));
#else
                    var lhsKvs = new Dictionary<string, object>();
                    foreach (var kv in ToKeyValues(lhs))
                    {
                        lhsKvs.Add(kv.Key, kv.Value);
                    }
                    var rhsKvs = new Dictionary<string, object>();
                    foreach (var kv in ToKeyValues(rhs))
                    {
                        rhsKvs.Add(kv.Key, kv.Value);
                    }
#endif
                    if (!lhsKvs.Keys.SequenceEqual(rhsKvs.Keys))
                    {
                        return false;
                    }
                    return lhsKvs.All(kv => DeepEquals(kv.Value, rhsKvs[kv.Key]));

                case NodeKind.Null:
                    return true;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
