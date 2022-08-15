//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "accessor.schema.json")]
    public sealed class Accessor : GltfChildOfRootProperty
    {
        [JsonField(Name = "bufferView"), JsonFieldIgnorable]
        [JsonSchemaRef(typeof(GltfID))]
        public int? BufferView;

        [JsonField(Name = "byteOffset")]
        [JsonSchema(Minimum = 0), JsonSchemaDependencies("bufferView")]
        public int ByteOffset = 0;

        [JsonField(Name = "componentType")]
        [JsonSchemaRequired]
        public ComponentTypeEnum ComponentType;

        [JsonField(Name = "normalized")]
        public bool Normalized = false;

        [JsonField(Name = "count")]
        [JsonSchema(Minimum = 1), JsonSchemaRequired]
        public int Count;

        [JsonField(Name = "type")]
        [JsonSchemaRequired]
        public TypeEnum Type;

        [JsonField(Name = "max"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1, MaxItems = 16)]
        public float[] Max;

        [JsonField(Name = "min"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1, MaxItems = 16)]
        public float[] Min;

        [JsonField(Name = "sparse"), JsonFieldIgnorable]
        public SparseType Sparse;

        //

        [Json]
        public enum ComponentTypeEnum
        {
            [JsonField] BYTE = 5120,
            [JsonField] UNSIGNED_BYTE = 5121,
            [JsonField] SHORT = 5122,
            [JsonField] UNSIGNED_SHORT = 5123,
            [JsonField] UNSIGNED_INT = 5125,
            [JsonField] FLOAT = 5126,
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum TypeEnum
        {
            [JsonField(Name = "SCALAR")]
            Scalar,
            [JsonField(Name = "VEC2")]
            Vec2,
            [JsonField(Name = "VEC3")]
            Vec3,
            [JsonField(Name = "VEC4")]
            Vec4,
            [JsonField(Name = "MAT2")]
            Mat2,
            [JsonField(Name = "MAT3")]
            Mat3,
            [JsonField(Name = "MAT4")]
            Mat4
        }

        [JsonSchema(Id = "accessor.sparse.schema.json")]
        public sealed class SparseType : GltfProperty
        {
            [JsonField(Name = "count")]
            [JsonSchema(Minimum = 0), JsonSchemaRequired]
            public int Count;

            [JsonField(Name = "indices")]
            [JsonSchemaRequired]
            public IndicesType Indices;

            [JsonField(Name = "values")]
            [JsonSchemaRequired]
            public ValuesType Values;

            //

            [JsonSchema(Id = "accessor.sparse.indices.schema.json")]
            public class IndicesType : GltfProperty
            {
                [JsonField(Name = "bufferView")]
                [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
                public int BufferView;

                [JsonField(Name = "byteOffset")]
                [JsonSchema(Minimum = 0)]
                public int ByteOffset = 0;

                [JsonField(Name = "componentType")]
                [JsonSchemaRequired]
                public ComponentTypeEnum ComponentType;

                //

                [Json]
                public enum ComponentTypeEnum
                {
                    [JsonField] UNSIGNED_BYTE = 5121,
                    [JsonField] UNSIGNED_SHORT = 5123,
                    [JsonField] UNSIGNED_INT = 5125,
                }
            }

            [JsonSchema(Id = "accessor.sparse.values.schema.json")]
            public sealed class ValuesType : GltfProperty
            {
                [JsonField(Name = "bufferView")]
                [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
                public int BufferView;

                [JsonField(Name = "byteOffset")]
                [JsonSchema(Minimum = 0)]
                public int ByteOffset = 0;
            }
        }
    }

    public static class AccessorComponentTypeEnumExtensions
    {
        public static int SizeInBytes(this Accessor.ComponentTypeEnum e)
        {
            switch (e)
            {
                case Accessor.ComponentTypeEnum.BYTE:
                    return 1;

                case Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                    return 1;

                case Accessor.ComponentTypeEnum.SHORT:
                    return 2;

                case Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                    return 2;

                case Accessor.ComponentTypeEnum.UNSIGNED_INT:
                    return 4;

                case Accessor.ComponentTypeEnum.FLOAT:
                    return 4;

                default:
                    throw new NotImplementedException();
            }
        }

        public static Type TypeOf(this Accessor.ComponentTypeEnum e)
        {
            switch (e)
            {
                case Accessor.ComponentTypeEnum.BYTE:
                    return typeof(sbyte);

                case Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                    return typeof(byte);

                case Accessor.ComponentTypeEnum.SHORT:
                    return typeof(short);

                case Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                    return typeof(ushort);

                case Accessor.ComponentTypeEnum.UNSIGNED_INT:
                    return typeof(uint);

                case Accessor.ComponentTypeEnum.FLOAT:
                    return typeof(float);

                default:
                    throw new NotImplementedException();
            }
        }
    }

    public static class AccessorTypeEnumExtensions
    {
        public static int NumOfComponents(this Accessor.TypeEnum t)
        {
            switch (t)
            {
                case Accessor.TypeEnum.Scalar:
                    return 1;

                case Accessor.TypeEnum.Vec2:
                    return 2;

                case Accessor.TypeEnum.Vec3:
                    return 3;

                case Accessor.TypeEnum.Vec4:
                    return 4;

                case Accessor.TypeEnum.Mat2:
                    return 4;

                case Accessor.TypeEnum.Mat3:
                    return 9;

                case Accessor.TypeEnum.Mat4:
                    return 16;

                default:
                    throw new NotImplementedException();
            }
        }
    }

    public static class AccessorSparseIndicesComponentTypeEnumExtensions
    {
        public static Accessor.ComponentTypeEnum CommonType(this Accessor.SparseType.IndicesType.ComponentTypeEnum e)
        {
            switch (e)
            {
                case Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_BYTE:
                    return Accessor.ComponentTypeEnum.UNSIGNED_BYTE;

                case Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_SHORT:
                    return Accessor.ComponentTypeEnum.UNSIGNED_SHORT;

                case Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_INT:
                    return Accessor.ComponentTypeEnum.UNSIGNED_INT;

                default:
                    throw new NotImplementedException();
            }
        }

        public static int SizeInBytes(this Accessor.SparseType.IndicesType.ComponentTypeEnum e)
        {
            return e.CommonType().SizeInBytes();
        }

        public static Type TypeOf(this Accessor.SparseType.IndicesType.ComponentTypeEnum e)
        {
            return e.CommonType().TypeOf();
        }
    }
}
