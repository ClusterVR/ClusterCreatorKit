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
    [JsonSchema(Id = "bufferView.schema.json")]
    public sealed class BufferView : GltfChildOfRootProperty
    {
        [JsonField(Name = "buffer")]
        [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
        public int Buffer;

        [JsonField(Name = "byteOffset"), JsonFieldIgnorable()]
        [JsonSchema(Minimum = 0)]
        public int ByteOffset = 0;

        [JsonField(Name = "byteLength")]
        [JsonSchema(Minimum = 1), JsonSchemaRequired]
        public int ByteLength;

        [JsonField(Name = "byteStride"), JsonFieldIgnorable]
        [JsonSchema(Minimum = 4, Maximum = 252, MultipleOf = 4)]
        public int? ByteStride;

        [JsonField(Name = "target"), JsonFieldIgnorable]
        public TargetEnum? Target;

        //

        [Json]
        public enum TargetEnum
        {
            [JsonField] ARRAY_BUFFER = 34962,
            [JsonField] ELEMENT_ARRAY_BUFFER = 34963,
        }
    }
}
