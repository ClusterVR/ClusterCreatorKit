//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace VJson.Schema
{
    [Json(ImplicitConstructable = true)]
    public sealed class JsonSchema
    {
        #region Core
        [JsonField(Name = "$schema", Order = -10)]
        [JsonFieldIgnorable]
        public string Schema;

        [JsonField(Name = "$id", Order = -11)]
        [JsonFieldIgnorable]
        public string Id;

        [JsonField(Name = "$ref", Order = -12)]
        [JsonFieldIgnorable]
        public string Ref;
        #endregion

        #region Metadata
        [JsonField(Name = "title", Order = 0)]
        [JsonFieldIgnorable]
        public string Title;

        [JsonField(Name = "description", Order = 1)]
        [JsonFieldIgnorable]
        public string Description;
        #endregion

        #region 6.1: Any instances
        [JsonField(Name = "type", TypeHints = new Type[] { typeof(string), typeof(string[]) }, Order = 10)]
        [JsonFieldIgnorable]
        public object Type;

        [JsonField(Name = "enum", Order = 11)]
        [JsonFieldIgnorable]
        public object[] Enum;

        [JsonField(Name = "const", Order = 12)]
        [JsonFieldIgnorable]
        public object Const;

        bool EqualsOnlyAny(JsonSchema rhs)
        {
            return EqualsSingletonOrArray<string>(Type, rhs.Type)
                && EqualsEnumerable(Enum, rhs.Enum)
                && Object.Equals(Const, rhs.Const)
                ;
        }
        #endregion

        #region 6.2: Numeric instances
        [JsonField(Name = "multipleOf", Order = 20)]
        [JsonFieldIgnorable(WhenValueIs = double.MinValue)]
        public double MultipleOf = double.MinValue;

        [JsonField(Name = "maximum", Order = 21)]
        [JsonFieldIgnorable(WhenValueIs = double.MinValue)]
        public double Maximum = double.MinValue;

        [JsonField(Name = "exclusiveMaximum", Order = 22)]
        [JsonFieldIgnorable(WhenValueIs = double.MinValue)]
        public double ExclusiveMaximum = double.MinValue;

        [JsonField(Name = "minimum", Order = 23)]
        [JsonFieldIgnorable(WhenValueIs = double.MaxValue)]
        public double Minimum = double.MaxValue;

        [JsonField(Name = "exclusiveMinimum", Order = 24)]
        [JsonFieldIgnorable(WhenValueIs = double.MaxValue)]
        public double ExclusiveMinimum = double.MaxValue;

        bool EqualsOnlyNum(JsonSchema rhs)
        {
            return MultipleOf == rhs.MultipleOf
                && Maximum == rhs.Maximum
                && ExclusiveMaximum == rhs.ExclusiveMaximum
                && Minimum == rhs.Minimum
                && ExclusiveMinimum == rhs.ExclusiveMinimum
                ;
        }
        #endregion

        #region 6.3. Strings
        [JsonField(Name = "maxLength", Order = 30)]
        [JsonFieldIgnorable(WhenValueIs = int.MinValue)]
        public int MaxLength = int.MinValue;

        [JsonField(Name = "minLength", Order = 31)]
        [JsonFieldIgnorable(WhenValueIs = int.MaxValue)]
        public int MinLength = int.MaxValue;

        [JsonField(Name = "pattern", Order = 32)]
        [JsonFieldIgnorable]
        public string Pattern;

        bool EqualsOnlyString(JsonSchema rhs)
        {
            return MaxLength == rhs.MaxLength
                && MinLength == rhs.MinLength
                && Object.Equals(Pattern, rhs.Pattern)
                ;
        }
        #endregion

        #region 6.4: Arrays
        [JsonField(Name = "items",
                   TypeHints = new Type[] { typeof(JsonSchema), typeof(JsonSchema[]) },
                   Order = 40)]
        [JsonFieldIgnorable]
        object items;

        [JsonField(Name = "additionalItems", Order = 41)]
        [JsonFieldIgnorable]
        public JsonSchema AdditionalItems;

        [JsonField(Name = "maxItems", Order = 42)]
        [JsonFieldIgnorable(WhenValueIs = int.MinValue)]
        public int MaxItems = int.MinValue;

        [JsonField(Name = "minItems", Order = 43)]
        [JsonFieldIgnorable(WhenValueIs = int.MaxValue)]
        public int MinItems = int.MaxValue;

        [JsonField(Name = "uniqueItems", Order = 44)]
        [JsonFieldIgnorable(WhenValueIs = false)]
        public bool UniqueItems = false;

        // public ... Contains

        public object Items => items;
        public JsonSchema TypedItems { set => items = value; }

        bool EqualsOnlyArray(JsonSchema rhs)
        {
            return EqualsSingletonOrArray<JsonSchema>(Items, rhs.Items)
                && Object.Equals(AdditionalItems, rhs.AdditionalItems)
                && MaxItems == rhs.MaxItems
                && MinItems == rhs.MinItems
                && UniqueItems == rhs.UniqueItems
                ;
        }
        #endregion

        #region 6.5: Objects
        [JsonField(Name = "maxProperties", Order = 50)]
        [JsonFieldIgnorable(WhenValueIs = int.MinValue)]
        public int MaxProperties = int.MinValue;

        [JsonField(Name = "minProperties", Order = 51)]
        [JsonFieldIgnorable(WhenValueIs = int.MaxValue)]
        public int MinProperties = int.MaxValue;

        [JsonField(Name = "required", Order = 52)]
        [JsonFieldIgnorable]
        public string[] Required;

        [JsonField(Name = "properties", Order = 53)]
        [JsonFieldIgnorable]
        public Dictionary<string, JsonSchema> Properties;

        [JsonField(Name = "patternProperties", Order = 54)]
        [JsonFieldIgnorable]
        public Dictionary<string, JsonSchema> PatternProperties;

        [JsonField(Name = "additionalProperties", Order = 55)]
        [JsonFieldIgnorable]
        public JsonSchema AdditionalProperties;

        [JsonField(Name = "dependencies",
                   /* TODO:
                      A type of this field should be Map<string, string[] | JsonSchema>.
                      But there are no ways to represent this type currently...
                    */
                   TypeHints = new Type[] {
                       typeof(Dictionary<string, string[]>),
                       typeof(Dictionary<string, JsonSchema>)
                   },
                   Order = 56)]
        [JsonFieldIgnorable]
        public object Dependencies;

        // public ... PropertyNames

        bool EqualsOnlyObject(JsonSchema rhs)
        {
            // TODO
            return true;
        }
        #endregion

        #region 6.7: Subschemas With Boolean Logic
        [JsonField(Name = "allOf", Order = 70)]
        [JsonFieldIgnorable]
        public List<JsonSchema> AllOf;

        private void AddToAllOf(JsonSchema s)
        {
            if (AllOf == null)
            {
                AllOf = new List<JsonSchema>();
            }

            AllOf.Add(s);
        }

        [JsonField(Name = "anyOf", Order = 71)]
        [JsonFieldIgnorable]
        public List<JsonSchema> AnyOf;

        private void AddToAnyOf(JsonSchema s)
        {
            if (AnyOf == null)
            {
                AnyOf = new List<JsonSchema>();
            }

            AnyOf.Add(s);
        }

        [JsonField(Name = "oneOf", Order = 72)]
        [JsonFieldIgnorable]
        public List<JsonSchema> OneOf;

        private void AddToOneOf(JsonSchema s)
        {
            if (OneOf == null)
            {
                OneOf = new List<JsonSchema>();
            }

            OneOf.Add(s);
        }

        [JsonField(Name = "not", Order = 73)]
        [JsonFieldIgnorable]
        public JsonSchema Not;

        bool EqualsOnlySubBool(JsonSchema rhs)
        {
            return EqualsEnumerable(AllOf, rhs.AllOf)
                && EqualsEnumerable(AnyOf, rhs.AnyOf)
                && EqualsEnumerable(OneOf, rhs.OneOf)
                && Object.Equals(Not, rhs.Not)
                ;
        }
        #endregion


        public override bool Equals(object rhsObj)
        {
            var rhs = rhsObj as JsonSchema;
            if (rhs == null)
            {
                return false;
            }

            return Title == rhs.Title
                && Description == rhs.Description
                && EqualsOnlyAny(rhs)
                && EqualsOnlyNum(rhs)
                && EqualsOnlyString(rhs)
                && EqualsOnlyArray(rhs)
                && EqualsOnlyObject(rhs)
                && EqualsOnlySubBool(rhs)
                ;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var serializer = new JsonSerializer(typeof(JsonSchema));
            return serializer.Serialize(this);
        }

        public JsonSchema()
        {
        }

        [Preserve]
        public JsonSchema(bool b)
        {
            if (!b)
            {
                // Equivalent to {"not": {}}
                Not = new JsonSchema();
            }

            // Equivalent to {}
        }

        public static JsonSchema CreateFromType<T>(JsonSchemaRegistry reg = null, bool asRef = false)
        {
            return CreateFromType(typeof(T), reg, asRef);
        }

        public static JsonSchema CreateFromType(Type ty, JsonSchemaRegistry reg = null, bool asRef = false)
        {
            var kind = Node.KindOfType(ty);
            switch (kind)
            {
                case NodeKind.Boolean:
                    return new JsonSchema
                    {
                        Type = "boolean",
                    };

                case NodeKind.Integer:
                    object[] enumsForInteger = null;
                    if (ty.IsEnum)
                    {
                        enumsForInteger = System.Enum.GetValues(ty).Cast<object>().ToArray();
                    }
                    return new JsonSchema
                    {
                        Type = "integer",
                        Enum = enumsForInteger,
                    };

                case NodeKind.Float:
                    return new JsonSchema
                    {
                        Type = "number",
                    };

                case NodeKind.String:
                    object[] enumsForString = null;
                    if (ty.IsEnum)
                    {
                        enumsForString = TypeHelper.GetStringEnumNames(ty);
                    }
                    return new JsonSchema
                    {
                        Type = "string",
                        Enum = enumsForString,
                    };

                case NodeKind.Array:
                    var elemTy = TypeHelper.ElemTypeOfIEnumerable(ty);
                    return new JsonSchema
                    {
                        Type = "array",
                        TypedItems = elemTy != null ? CreateFromType(elemTy, reg, true) : null,
                    };

                case NodeKind.Object:
                    if (ty == typeof(object))
                    {
                        return new JsonSchema();
                    }

                    if (ty.IsGenericType && ty.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        return new JsonSchema
                        {
                            Type = "object",
                        };
                    }

                    break;

                default:
                    throw new NotImplementedException();
            }

            if (reg == null)
            {
                reg = new JsonSchemaRegistry();
            }

            var schemaAttr = (JsonSchemaAttribute)TypeHelper.GetCustomAttribute<JsonSchemaAttribute>(ty);
            var schemaId = schemaAttr?.Id;
            if (string.IsNullOrEmpty(schemaId))
            {
                schemaId = ty.ToString();
            }

            var refSchema = reg.Resolve(schemaId);
            if (refSchema != null)
            {
                if (asRef)
                {
                    return new JsonSchema
                    {
                        Ref = schemaId,
                    };
                }

                return refSchema;
            }

            var schema = CreateFromSchemaAttr(schemaAttr);
            schema.Type = "object";

            var baseType = ty.BaseType;
            HashSet<string> baseFieldNames = null;
            if (baseType != null)
            {
                Type schemaBaseType;
                if (RefChecker.IsRefTag(baseType, out schemaBaseType))
                {
                    var baseSchemaValue = CreateFromType(schemaBaseType, reg, false);
                    schema.Type = baseSchemaValue.Type;

                    goto skipFields;
                }

                // Nest fields included in the base class
                var baseSchema = CreateFromType(baseType, reg, true);
                if (baseSchema != null && baseSchema.Ref != null)
                {
                    schema.AddToAllOf(baseSchema);

                    var baseFields = TypeHelper.GetSerializableFields(baseType);
                    baseFieldNames = new HashSet<string>(baseFields.Select(f => f.Name));
                }
            }

            var properties = new Dictionary<string, JsonSchema>();
            var required = new List<string>();
            var dependencies = new Dictionary<string, string[]>();

            var fields = TypeHelper.GetSerializableFields(ty);
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;

                var attr = TypeHelper.GetCustomAttribute<JsonFieldAttribute>(field);
                var elemName = JsonFieldAttribute.FieldName(attr, field); // TODO: duplication check

                // If elements are also included in Base classes, skip collecting a schema for the elements.
                if (baseFieldNames != null && baseFieldNames.Contains(field.Name))
                {
                    properties.Add(elemName, new JsonSchema());
                    continue;
                }

                var fieldSchemaAttr = TypeHelper.GetCustomAttribute<JsonSchemaAttribute>(field);
                var fieldSchema = CreateFromSchemaAttr(fieldSchemaAttr);

                var fieldItemsSchema = TypeHelper.GetCustomAttribute<ItemsJsonSchemaAttribute>(field);
                if (fieldItemsSchema != null)
                {
                    fieldSchema.TypedItems = CreateFromSchemaAttr(fieldItemsSchema);
                }

                var fieldItemRequired = TypeHelper.GetCustomAttribute<JsonSchemaRequiredAttribute>(field);
                if (fieldItemRequired != null)
                {
                    required.Add(elemName);
                }

                var fieldItemDependencies = TypeHelper.GetCustomAttribute<JsonSchemaDependenciesAttribute>(field);
                if (fieldItemDependencies != null)
                {
                    dependencies.Add(elemName, fieldItemDependencies.Dependencies);
                }

                var fieldTypeSchema = CreateFromType(fieldType, reg, true);
                if (fieldTypeSchema.Ref != null)
                {
                    fieldSchema = fieldTypeSchema;
                }
                else
                {
                    // Update
                    if (fieldSchema.Type == null)
                    {
                        fieldSchema.Type = fieldTypeSchema.Type;
                    }

                    if (fieldSchema.Enum == null)
                    {
                        fieldSchema.Enum = fieldTypeSchema.Enum;
                    }

                    if (fieldTypeSchema.Items != null)
                    {
                        var fieldTypeSchemaItems = fieldTypeSchema.Items as JsonSchema;
                        if (fieldTypeSchemaItems.Ref != null)
                        {
                            fieldSchema.TypedItems = fieldTypeSchemaItems;
                        }
                        else
                        {
                            if (fieldTypeSchemaItems.Type != null)
                            {
                                var fieldSchemaItems = fieldSchema.Items as JsonSchema;
                                if (fieldSchemaItems != null)
                                {
                                    fieldSchemaItems.Type = fieldTypeSchemaItems.Type;
                                }
                                else
                                {
                                    fieldSchema.TypedItems = new JsonSchema
                                    {
                                        Type = fieldTypeSchemaItems.Type,
                                    };
                                }
                            }

                            if (fieldTypeSchemaItems.Enum != null)
                            {
                                var fieldSchemaItems = fieldSchema.Items as JsonSchema;
                                fieldSchemaItems.Enum = fieldTypeSchemaItems.Enum;
                            }
                        }
                    }
                }

                // Add custom refs to AllOf not to override constrains which already existing.
                var customRef = TypeHelper.GetCustomAttribute<JsonSchemaRefAttribute>(field);
                if (customRef != null)
                {
                    Type schemaBaseType;
                    if (!RefChecker.IsRefTagDerived(customRef.TagType, out schemaBaseType))
                    {
                        throw new ArgumentException("IRefTag<T> must be derived by tagType");
                    }

                    var customSchema = CreateFromType(customRef.TagType, reg, true);
                    switch (customRef.Influence)
                    {
                        case InfluenceRange.Entiry:
                            fieldSchema.AddToAllOf(customSchema);
                            break;

                        case InfluenceRange.AdditionalProperties:
                            if (fieldSchema.AdditionalProperties == null)
                            {
                                fieldSchema.AdditionalProperties = new JsonSchema();
                            }
                            fieldSchema.AdditionalProperties.AddToAllOf(customSchema);
                            break;
                    }
                }

                // Add custom refs to AllOf not to override constrains which already existing.
                var customItemsRef = TypeHelper.GetCustomAttribute<ItemsJsonSchemaRefAttribute>(field);
                if (customItemsRef != null)
                {
                    Type schemaBaseType;
                    if (!RefChecker.IsRefTagDerived(customItemsRef.TagType, out schemaBaseType))
                    {
                        throw new ArgumentException("IRefTag<T> must be derived by tagType");
                    }

                    var customSchema = CreateFromType(customItemsRef.TagType, reg, true);
                    switch (customItemsRef.Influence)
                    {
                        case InfluenceRange.Entiry:
                            if (fieldSchema.Items == null)
                            {
                                fieldSchema.TypedItems = new JsonSchema();
                            }
                            ((JsonSchema)fieldSchema.Items).AddToAllOf(customSchema);
                            break;

                        case InfluenceRange.AdditionalProperties:
                            if (fieldSchema.Items == null)
                            {
                                fieldSchema.TypedItems = new JsonSchema();
                            }
                            if (((JsonSchema)fieldSchema.Items).AdditionalProperties == null)
                            {
                                ((JsonSchema)fieldSchema.Items).AdditionalProperties =
                                    new JsonSchema();
                            }
                            ((JsonSchema)fieldSchema.Items).AdditionalProperties.AddToAllOf(customSchema);
                            break;
                    }
                }

                properties.Add(elemName, fieldSchema);
            }

            schema.Properties = properties;
            if (required.Count != 0)
            {
                schema.Required = required.ToArray();
            }
            if (dependencies.Count != 0)
            {
                schema.Dependencies = dependencies;
            }

skipFields:
            reg.Register(schemaId, schema);

            if (asRef)
            {
                return new JsonSchema
                {
                    Ref = schemaId,
                };
            }

            return schema;
        }

        static JsonSchema CreateFromSchemaAttr(JsonSchemaAttribute attr)
        {
            var schema = new JsonSchema();
            if (attr == null)
            {
                return schema;
            }

            // Core
            schema.Schema = attr.Schema;
            schema.Id = attr.Id;
            schema.Ref = attr.Ref;

            // Metadata
            schema.Title = attr.Title;
            schema.Description = attr.Description;

            // 6.1: Any instances
            // schema.Type = attr.Type;
            // schema.Enum = attr.Enum;
            // schema.Const = attr.Const;

            // 6.2: Numeric instances
            schema.MultipleOf = attr.MultipleOf;
            schema.Maximum = attr.Maximum;
            schema.ExclusiveMaximum = attr.ExclusiveMaximum;
            schema.Minimum = attr.Minimum;
            schema.ExclusiveMinimum = attr.ExclusiveMinimum;

            // 6.3. Strings
            schema.MaxLength = attr.MaxLength;
            schema.MinLength = attr.MinLength;
            schema.Pattern = attr.Pattern;

            // 6.4: Arrays
            // schema.Items = attr.Items;
            // schema.AdditionalItems = attr.AdditionalItems;
            schema.MaxItems = attr.MaxItems;
            schema.MinItems = attr.MinItems;
            schema.UniqueItems = attr.UniqueItems;
            // schema.Contains

            // 6.5: Objects
            schema.MaxProperties = attr.MaxProperties;
            schema.MinProperties = attr.MinProperties;
            schema.Required = attr.Required;
            // schema.Properties = attr.Properties;
            // schema.PatternProperties = attr.PatternProperties;
            // schema.AdditionalProperties = attr.AdditionalProperties;
            // schema.Dependencies = attr.Dependencies;
            // schema.PropertyNames

            // 6.7: Subschemas With Boolean Logic
            // schema.AllOf;
            // schema.AnyOf;
            // schema.OneOf;
            // schema.Not;

            return schema;
        }

        static bool EqualsSingletonOrArray<T>(object lhs, object rhs) where T : class
        {
            if (lhs == null && rhs == null)
            {
                return true;
            }

            if (lhs == null || rhs == null)
            {
                return false;
            }

            var lhsArr = lhs as T[];
            var rhsArr = rhs as T[];
            if (lhsArr != null && rhsArr != null)
            {
                return EqualsEnumerable<T>(lhsArr, rhsArr);
            }

            var lhsSgt = lhs as T;
            var rhsAgt = rhs as T;
            return Object.Equals(lhsSgt, rhsAgt);
        }

        static bool EqualsEnumerable<E>(IEnumerable<E> lhs, IEnumerable<E> rhs)
        {
            return (lhs == null && rhs == null)
                || (lhs != null && lhs != null && lhs.SequenceEqual(rhs))
                ;
        }
    }

    public static class JsonSchemaExtensions
    {
        public static ConstraintsViolationException Validate(this JsonSchema j,
                                                             object o,
                                                             JsonSchemaRegistry reg = null)
        {
            return (new JsonSchemaValidator(j)).Validate(o, reg);
        }

        internal static ConstraintsViolationException Validate(this JsonSchema j,
                                                               object o,
                                                               Internal.State state,
                                                               JsonSchemaRegistry reg)
        {
            return (new JsonSchemaValidator(j)).Validate(o, state, reg);
        }
    }
}
