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
    [JsonSchema(Id = "scene.schema.json")]
    public sealed class Scene : GltfChildOfRootProperty
    {
        [JsonField(Name = "nodes")]
        [JsonSchema(UniqueItems = true, MinItems = 1)]
        [ItemsJsonSchemaRef(typeof(GltfID))]
        public int[] Nodes;
    }
}
