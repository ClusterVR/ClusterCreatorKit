//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "animation.schema.json")]
    public sealed class Animation : GltfChildOfRootProperty
    {
        [JsonField(Name = "channels")]
        [JsonSchema(MinItems = 1), JsonSchemaRequired]
        public List<ChannelType> Channels;

        [JsonField(Name = "samplers")]
        [JsonSchema(MinItems = 1), JsonSchemaRequired]
        public List<SamplerType> Samplers;

        //

        [JsonSchema(Id = "animation.channel.schema.json")]
        public class ChannelType : GltfProperty
        {
            [JsonField(Name = "sampler")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int Sampler;

            [JsonField(Name = "target")]
            [JsonSchemaRequired]
            public TargetType Target;

            //

            [JsonSchema(Id = "animation.channel.target.schema.json")]
            public class TargetType : GltfProperty
            {
                [JsonField(Name = "node")]
                [JsonSchemaRef(typeof(GltfID))]
                public int Node;

                [JsonField(Name = "path")]
                [JsonSchemaRequired]
                public PathEnum Path;

                //

                [Json(EnumConversion = EnumConversionType.AsString)]
                public enum PathEnum
                {
                    [JsonField(Name = "translation")]
                    Translation,
                    [JsonField(Name = "rotation")]
                    Rotation,
                    [JsonField(Name = "scale")]
                    Scale,
                    [JsonField(Name = "weights")]
                    Weights,
                }
            }
        }

        [JsonSchema(Id = "animation.sampler.schema.json")]
        public class SamplerType
        {
            [JsonField(Name = "input")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int Input;

            [JsonField(Name = "interpolation")]
            public InterpolationEnum Interpolation = InterpolationEnum.LINEAR;

            [JsonField(Name = "output")]
            [JsonSchemaRequired, JsonSchemaRef(typeof(GltfID))]
            public int Output;

            //

            [Json(EnumConversion = EnumConversionType.AsString)]
            public enum InterpolationEnum
            {
                [JsonField(Name = "LINEAR")]
                LINEAR,
                [JsonField(Name = "STEP")]
                STEP,
                [JsonField(Name = "CUBICSPLINE")]
                CUBICSPLINE,
            }
        }
    }
}
