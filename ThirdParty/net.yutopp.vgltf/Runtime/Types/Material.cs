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
    [JsonSchema(Id = "material.schema.json")]
    public sealed class Material : GltfChildOfRootProperty
    {
        [JsonField(Name = "pbrMetallicRoughness"), JsonFieldIgnorable]
        public PbrMetallicRoughnessType PbrMetallicRoughness;

        [JsonField(Name = "normalTexture"), JsonFieldIgnorable]
        public NormalTextureInfoType NormalTexture;

        [JsonField(Name = "occlusionTexture"), JsonFieldIgnorable]
        public OcclusionTextureInfoType OcclusionTexture;

        [JsonField(Name = "emissiveTexture"), JsonFieldIgnorable]
        public EmissiveTextureInfoType EmissiveTexture;

        [JsonField(Name = "emissiveFactor"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 3, MaxItems = 3)]
        [ItemsJsonSchema(Minimum = 0.0, Maximum = 1.0)]
        public float[] EmissiveFactor = new float[] { 0.0f, 0.0f, 0.0f };

        [JsonField(Name = "alphaMode")]
        public AlphaModeEnum AlphaMode = AlphaModeEnum.Opaque;

        [JsonField(Name = "alphaCutoff")]
        [JsonSchema(Minimum = 0.0), JsonFieldIgnorable(WhenValueIs = 0.0f)]
        [JsonSchemaDependencies(new string[] { "alphaMode" })]
        public float AlphaCutoff = 0.5f;

        [JsonField(Name = "doubleSided")]
        public bool DoubleSided = false;

        //

        [JsonSchema(Id = "material.pbrMetallicRoughness.schema.json")]
        public sealed class PbrMetallicRoughnessType : GltfProperty
        {
            [JsonField(Name = "baseColorFactor")]
            [JsonSchema(MinItems = 4, MaxItems = 4)]
            [ItemsJsonSchema(Minimum = 0.0f, Maximum = 1.0f)]
            public float[] BaseColorFactor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };

            [JsonField(Name = "baseColorTexture"), JsonFieldIgnorable]
            public BaseColorTextureInfoType BaseColorTexture;

            [JsonField(Name = "metallicFactor")]
            [JsonSchema(Minimum = 0.0f, Maximum = 1.0f)]
            public float MetallicFactor = 1.0f;

            [JsonField(Name = "roughnessFactor")]
            [JsonSchema(Minimum = 0.0f, Maximum = 1.0f)]
            public float RoughnessFactor = 1.0f;

            [JsonField(Name = "metallicRoughnessTexture"), JsonFieldIgnorable]
            public MetallicRoughnessTextureInfoType MetallicRoughnessTexture;
        }

        [Json]
        public sealed class BaseColorTextureInfoType : TextureInfo
        {
            public override TextureInfoKind Kind { get { return TextureInfoKind.BaseColor; } }
        }

        [Json]
        public sealed class MetallicRoughnessTextureInfoType : TextureInfo
        {
            public override TextureInfoKind Kind { get { return TextureInfoKind.MetallicRoughness; } }
        }

        [JsonSchema(Id = "material.normalTextureInfo.schema.json")]
        public sealed class NormalTextureInfoType : TextureInfo
        {
            [JsonField(Name = "scale")]
            public float Scale = 1.0f;

            public override TextureInfoKind Kind { get { return TextureInfoKind.Normal; } }
        }

        [JsonSchema(Id = "material.occlusionTextureInfo.schema.json")]
        public sealed class OcclusionTextureInfoType : TextureInfo
        {
            [JsonField(Name = "strength")]
            [JsonSchema(Minimum = 0.0f, Maximum = 1.0f)]
            public float Strength = 1.0f;

            public override TextureInfoKind Kind { get { return TextureInfoKind.Occlusion; } }
        }

        [Json]
        public sealed class EmissiveTextureInfoType : TextureInfo
        {
            public override TextureInfoKind Kind { get { return TextureInfoKind.Emissive; } }
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum AlphaModeEnum
        {
            [JsonField(Name = "OPAQUE")]
            Opaque,
            [JsonField(Name = "MASK")]
            Mask,
            [JsonField(Name = "BLEND")]
            Blend,
        }
    }

    public static class MaterialExtensions
    {
        public static IEnumerable<TextureInfo> GetTextures(this Material mat)
        {
            if (mat.PbrMetallicRoughness != null)
            {
                yield return mat.PbrMetallicRoughness.BaseColorTexture;
                yield return mat.PbrMetallicRoughness.MetallicRoughnessTexture;
            }

            yield return mat.NormalTexture;
            yield return mat.OcclusionTexture;
            yield return mat.EmissiveTexture;
        }
    }
}
