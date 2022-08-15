//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;
using VJson.Schema;

namespace VGltf.Ext.KhrMaterialsUnlit.Types
{
    // https://github.com/KhronosGroup/glTF/blob/master/extensions/2.0/Khronos/KHR_materials_unlit/schema/glTF.KHR_materials_unlit.schema.json
    [JsonSchema(Title = "KHR_materials_unlit glTF extension",
            Description = "glTF extension that defines the unlit material model.",
            Id = "glTF.KHR_materials_unlit.schema.json")]
    public sealed class KhrMaterialsUnlit
    {
        public static readonly string ExtensionName = "KHR_materials_unlit";
    }
}
