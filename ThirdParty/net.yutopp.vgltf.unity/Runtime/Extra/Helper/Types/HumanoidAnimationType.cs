//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VGltf.Types;
using VJson;
using VJson.Schema;

namespace VGltf.Unity.Ext.Helper.Types
{
    /// <summary>
    /// </summary>
    [Json]
    public sealed class HumanoidAnimationType
    {
        public static readonly string ExtraName = "VGLTF_unity_humanoid_animation";

        [Json]
        public sealed class SamplerType
        {
            public static readonly string ExtraName = "VGLTF_unity_humanoid_animation_sampler";

            [JsonField(Name = "inTangent")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int InTangent; // Accessor (Scalar, FLOAT)

            [JsonField(Name = "inWeight")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int InWeight; // Accessor (Scalar, FLOAT)

            [JsonField(Name = "outTangent")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int OutTangent; // Accessor (Scalar, FLOAT)

            [JsonField(Name = "outWeight")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int OutWeight; // Accessor (Scalar, FLOAT)
        }

        [Json]
        public sealed class ChannelType
        {
            public static readonly string ExtraName = "VGLTF_unity_humanoid_animation_channel";

            [JsonField(Name = "relativePath")]
            public string RelativePath;

            [JsonField(Name = "propertyName")]
            public string PropertyName;
        }
    }
}
