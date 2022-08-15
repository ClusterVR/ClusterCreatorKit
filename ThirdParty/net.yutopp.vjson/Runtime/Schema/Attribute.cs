//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;

namespace VJson.Schema
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, Inherited = false)]
    public class JsonSchemaAttribute : PreserveAttribute
    {
        #region Core
        public string Schema;
        public string Id;
        public string Ref;
        #endregion

        #region Metadata
        public string Title;
        public string Description;
        #endregion

        #region 6.1: Any instances
        // public object Type;
        // public object[] Enum;
        // public object Const;
        #endregion

        #region 6.2: Numeric instances
        public double MultipleOf = double.MinValue;
        public double Maximum = double.MinValue;
        public double ExclusiveMaximum = double.MinValue;
        public double Minimum = double.MaxValue;
        public double ExclusiveMinimum = double.MaxValue;
        #endregion

        #region 6.3. Strings
        public int MaxLength = int.MinValue;
        public int MinLength = int.MaxValue;
        public string Pattern;
        #endregion

        #region 6.4: Arrays
        // public object Items;
        // public JsonSchemaAttribute AdditionalItems;
        public int MaxItems = int.MinValue;
        public int MinItems = int.MaxValue;
        public bool UniqueItems = false;
        // contains
        #endregion

        #region 6.5: Objects
        public int MaxProperties = int.MinValue;
        public int MinProperties = int.MaxValue;
        public string[] Required; // Use [JsonSchemaRequired] instead when specify it by attributes.
        // public Dictionary<string, JsonSchemaAttribute> Properties;
        // public Dictionary<string, JsonSchemaAttribute> PatternProperties;
        // public JsonSchemaAttribute AdditionalProperties;
        // public object Dependencies; // Use [JsonSchemaDependencies] instead when specify it by attributes.
        // propertyNames
        #endregion

        #region 6.7: Subschemas With Boolean Logic
        // public List<JsonSchemaAttribute> AllOf;
        // public List<JsonSchemaAttribute> AnyOf;
        // public List<JsonSchemaAttribute> OneOf;
        // public JsonSchemaAttribute Not;
        #endregion
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public sealed class ItemsJsonSchemaAttribute : JsonSchemaAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public sealed class JsonSchemaRequiredAttribute : PreserveAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public sealed class JsonSchemaDependenciesAttribute : PreserveAttribute
    {
        public string[] Dependencies { get; private set; }

        public JsonSchemaDependenciesAttribute(params string[] deps)
        {
            Dependencies = deps;
        }
    }

    public enum InfluenceRange
    {
        Entiry,
        AdditionalProperties,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class JsonSchemaRefAttribute : PreserveAttribute
    {
        public Type TagType { get; private set; }
        public InfluenceRange Influence { get; private set; }

        public JsonSchemaRefAttribute(Type tagType, InfluenceRange influence = InfluenceRange.Entiry)
        {
            Type schemaBaseType;
            if (!RefChecker.IsRefTagDerived(tagType, out schemaBaseType))
            {
                throw new ArgumentException("IRefTag<T> must be derived by tagType");
            }

            TagType = tagType;
            Influence = influence;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public sealed class ItemsJsonSchemaRefAttribute : JsonSchemaRefAttribute
    {
        public ItemsJsonSchemaRefAttribute(Type tagType, InfluenceRange influence = InfluenceRange.Entiry)
        : base(tagType, influence)
        {
        }
    }
}
