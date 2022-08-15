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
    [JsonSchema(Id = "asset.schema.json")]
    public sealed class Asset : GltfProperty
    {
        [JsonField(Name = "copyright"), JsonFieldIgnorable]
        public string Copyright;

        [JsonField(Name = "generator"), JsonFieldIgnorable]
        public string Generator;

        [JsonField(Name = "version")]
        [JsonSchema(Pattern = "^[0-9]+\\.[0-9]+$"), JsonSchemaRequired]
        public string Version;

        [JsonField(Name = "minVersion"), JsonFieldIgnorable]
        [JsonSchema(Pattern = "^[0-9]+\\.[0-9]+$")]
        public string MinVersion;
    }
}
