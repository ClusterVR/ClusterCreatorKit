//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections;
using System.Reflection;

namespace VJson
{
    public enum EnumConversionType
    {
        AsInt,
        AsString,
    }

    // NOTE:
    //   If you are using VJson on pureC#, this Attribute will not have any effect.
    //   However, as long as you are using it on Unity and IL2CPP, this Attribute will be effective in preventing unnecessary optimization.
    //
    //   See: https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Scripting.PreserveAttribute.html
    //   > For 3rd party libraries that do not want to take on a dependency on UnityEngine.dll,
    //   > it is also possible to define their own PreserveAttribute. The code stripper will respect that too,
    //   > and it will consider any attribute with the exact name "PreserveAttribute" as a reason not to strip the thing it is applied on,
    //   > regardless of the namespace or assembly of the attribute.
    public class PreserveAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public sealed class JsonAttribute : PreserveAttribute
    {
        public bool ImplicitConstructable; // Only for classes
        public EnumConversionType EnumConversion; // Only for enums
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class JsonFieldAttribute : PreserveAttribute
    {
        public string Name;
        public int Order = 0;
        public Type[] TypeHints;

        public static string FieldName(JsonFieldAttribute f, FieldInfo fi)
        {
            if (f != null && !string.IsNullOrEmpty(f.Name))
            {
                return f.Name;
            }

            return fi.Name;
        }

        public static int FieldOrder(JsonFieldAttribute f)
        {
            if (f != null)
            {
                return f.Order;
            }

            return 0;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class JsonFieldIgnorableAttribute : PreserveAttribute
    {
        public object WhenValueIs;
        public int WhenLengthIs;

        public static bool IsIgnorable<T>(JsonFieldIgnorableAttribute f, T o)
        {
            if (f == null)
            {
                return false;
            }

            // Value
            if (Object.Equals(o, f.WhenValueIs))
            {
                return true;
            }

            // Length
            var a = o as Array;
            if (a != null)
            {
                return a.Length == f.WhenLengthIs;
            }

            var l = o as IList;
            if (l != null)
            {
                return l.Count == f.WhenLengthIs;
            }

            // Others
            return false;
        }
    }
}
