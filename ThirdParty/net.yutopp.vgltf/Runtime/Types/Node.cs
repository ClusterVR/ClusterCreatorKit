//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "node.schema.json")]
    // TODO: not
    public sealed class Node : GltfChildOfRootProperty
    {
        [JsonField(Name = "camera"), JsonFieldIgnorable]
        [JsonSchemaRef(typeof(GltfID))]
        public int? Camera;

        [JsonField(Name = "children"), JsonFieldIgnorable]
        [JsonSchema(UniqueItems = true, MinItems = 1)]
        [ItemsJsonSchemaRef(typeof(GltfID))]
        public int[] Children;

        [JsonField(Name = "skin"), JsonFieldIgnorable]
        [JsonSchemaDependencies("mesh"), JsonSchemaRef(typeof(GltfID))]
        public int? Skin;

        [JsonField(Name = "matrix"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 16, MaxItems = 16)]
        public float[] Matrix = new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f };

        [JsonField(Name = "mesh"), JsonFieldIgnorable]
        [JsonSchemaRef(typeof(GltfID))]
        public int? Mesh;

        [JsonField(Name = "rotation"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 4, MaxItems = 4)]
        [ItemsJsonSchema(Minimum = -1.0, Maximum = 1.0)]
        public float[] Rotation = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

        [JsonField(Name = "scale"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] Scale = new float[] { 1.0f, 1.0f, 1.0f };

        [JsonField(Name = "translation"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 3, MaxItems = 3)]
        public float[] Translation = new float[] { 0.0f, 0.0f, 0.0f };

        [JsonField(Name = "weights"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1), JsonSchemaDependencies("mesh")]
        public float[] Weights;
    }
}
