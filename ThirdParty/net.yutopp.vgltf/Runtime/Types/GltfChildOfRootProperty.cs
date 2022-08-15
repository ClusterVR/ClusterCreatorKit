//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [Json]
    public abstract class GltfChildOfRootProperty : GltfProperty
    {
        [JsonField(Name = "name"), JsonFieldIgnorable]
        public string Name;
    }
}
