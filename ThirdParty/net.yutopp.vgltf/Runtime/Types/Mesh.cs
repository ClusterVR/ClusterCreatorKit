//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "mesh.schema.json")]
    public sealed class Mesh : GltfChildOfRootProperty
    {
        [JsonField(Name = "primitives")]
        [JsonSchema(MinItems = 1), JsonSchemaRequired]
        public List<PrimitiveType> Primitives;

        [JsonField(Name = "weights"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public float[] Weights;

        //

        [JsonSchema(Id = "mesh.primitive.schema.json")]
        public sealed class PrimitiveType : GltfProperty
        {
            [JsonField(Name = "attributes")]
            [JsonSchema(MinProperties = 1), JsonSchemaRequired]
            [JsonSchemaRef(typeof(GltfID), InfluenceRange.AdditionalProperties)]
            public Dictionary<string, int> Attributes;

            [JsonField(Name = "indices"), JsonFieldIgnorable]
            [JsonSchemaRef(typeof(GltfID))]
            public int? Indices;

            [JsonField(Name = "material"), JsonFieldIgnorable]
            [JsonSchemaRef(typeof(GltfID))]
            public int? Material;

            [JsonField(Name = "mode"), JsonFieldIgnorable]
            public ModeEnum? Mode = ModeEnum.TRIANGLES;

            [JsonField(Name = "targets"), JsonFieldIgnorable]
            [JsonSchema(MinItems = 1)]
            [ItemsJsonSchema(MinProperties = 1)]
            [ItemsJsonSchemaRef(typeof(GltfID), InfluenceRange.AdditionalProperties)]
            public List<Dictionary<string, int>> Targets;

            //

            [Json]
            public enum ModeEnum
            {
                [JsonField] POINTS = 0,
                [JsonField] LINES = 1,
                [JsonField] LINE_LOOP = 2,
                [JsonField] LINE_STRIP = 3,
                [JsonField] TRIANGLES = 4,
                [JsonField] TRIANGLE_STRIP = 5,
                [JsonField] TRIANGLE_FAN = 6,
            }

            public static class AttributeName
            {
                public static readonly string POSITION = "POSITION";
                public static readonly string NORMAL = "NORMAL";
                public static readonly string TANGENT = "TANGENT";
                public static readonly string TEXCOORD_0 = "TEXCOORD_0";
                public static readonly string TEXCOORD_1 = "TEXCOORD_1";
                public static readonly string COLOR_0 = "COLOR_0";
                public static readonly string JOINTS_0 = "JOINTS_0";
                public static readonly string WEIGHTS_0 = "WEIGHTS_0";
            }
        }
    }
}
