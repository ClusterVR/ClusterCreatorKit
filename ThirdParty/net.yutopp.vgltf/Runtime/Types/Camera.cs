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
    [JsonSchema(Id = "camera.schema.json")]
    // TODO: not: "required": [ "perspective", "orthographic" ]
    public sealed class Camera : GltfChildOfRootProperty
    {
        [JsonField(Name = "orthographic"), JsonFieldIgnorable]
        public OrthographicType Orthographic;

        [JsonField(Name = "perspective"), JsonFieldIgnorable]
        public PerspectiveType Perspective;

        [JsonField(Name = "type")]
        [JsonSchemaRequired]
        public TypeEnum Type;

        //

        [JsonSchema(Id = "camera.orthographic.schema.json")]
        public sealed class OrthographicType : GltfProperty
        {
            [JsonField(Name = "xmag")]
            [JsonSchemaRequired]
            public float Xmag;

            [JsonField(Name = "ymag")]
            [JsonSchemaRequired]
            public float Ymag;

            [JsonField(Name = "zfar")]
            [JsonSchema(ExclusiveMinimum = 0.0f), JsonSchemaRequired]
            public float Zfar;

            [JsonField(Name = "znear")]
            [JsonSchema(Minimum = 0.0f), JsonSchemaRequired]
            public float Znear;
        }

        [JsonSchema(Id = "camera.perspective.schema.json")]
        public sealed class PerspectiveType
        {
            [JsonField(Name = "aspectRatio"), JsonFieldIgnorable]
            [JsonSchema(ExclusiveMinimum = 0.0f)]
            public float? AspectRatio;

            [JsonField(Name = "yfov")]
            [JsonSchema(ExclusiveMinimum = 0.0f), JsonSchemaRequired]
            public float Yfov;

            [JsonField(Name = "zfar"), JsonFieldIgnorable]
            [JsonSchema(ExclusiveMinimum = 0.0f)]
            public float? Zfar;

            [JsonField(Name = "znear")]
            [JsonSchema(ExclusiveMinimum = 0.0f), JsonSchemaRequired]
            public float Znear;
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum TypeEnum
        {
            [JsonField(Name = "perspective")]
            Perspective,
            [JsonField(Name = "orthographic")]
            Orthographic,
        }
    }
}
