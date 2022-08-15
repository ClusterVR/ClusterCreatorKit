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
    [JsonSchema(Id = "texture.schema.json")]
    public sealed class Texture : GltfChildOfRootProperty
    {
        [JsonField(Name = "sampler"), JsonFieldIgnorable]
        [JsonSchemaRef(typeof(GltfID))]
        public int? Sampler;

        [JsonField(Name = "source"), JsonFieldIgnorable]
        [JsonSchemaRef(typeof(GltfID))]
        public int? Source;
    }

    public enum TextureInfoKind
    {
        BaseColor,
        MetallicRoughness,
        Normal,
        Occlusion,
        Emissive
    }

    [Json]
    public abstract class TextureInfo : GltfProperty
    {
        [JsonField(Name = "index")]
        [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
        public int Index;

        [JsonField(Name = "texCoord")]
        [JsonSchema(Minimum = 0), JsonSchemaRequired]
        public int TexCoord = 0;

        public abstract TextureInfoKind Kind { get; }
    }
}
