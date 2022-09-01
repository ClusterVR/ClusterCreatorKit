//
// Copyright (c) 2022 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;
using VJson.Schema;

namespace VGltf.Ext.KhrMaterialsEmissiveStrength.Types
{
    // https://github.com/KhronosGroup/glTF/blob/main/extensions/2.0/Khronos/KHR_materials_emissive_strength/schema/glTF.KHR_materials_emissive_strength.schema.json
    [JsonSchema(Title = "KHR_materials_emissive_strength glTF extension",
            Description = "glTF extension that adjusts the strength of emissive material properties.",
            Id = "glTF.KHR_materials_emissive_strength.schema.json")]
    public sealed class KhrMaterialsEmissiveStrength
    {
        public static readonly string ExtensionName = "KHR_materials_emissive_strength";

        [JsonField(Name = "emissiveStrength"), JsonFieldIgnorable(WhenValueIs = 1.0f)]
        [JsonSchema(Minimum = 0.0f)]
        public float EmissiveStrength = 1.0f;
    }
}
