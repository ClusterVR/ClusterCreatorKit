//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;
using VJson.Schema;

namespace VGltf.Ext.Vrm0.Types
{
    [Json]
    public struct Vector3
    {
        [JsonField] public float x;
        [JsonField] public float y;
        [JsonField] public float z;
    }
}
