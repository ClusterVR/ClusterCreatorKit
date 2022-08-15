//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using VJson;
using VJson.Schema;

namespace VGltf.Ext.Vrm0.Types
{
    [JsonSchema(Title = "vrm.blendshape",
                Id = "vrm.blendshape.schema.json"/* TODO: Fix usage of Id */)]
    public class BlendShape
    {
        [JsonField(Name = "blendShapeGroups")]
        public List<GroupType> BlendShapeGroups = new List<GroupType>();

        //

        [JsonSchema(Title = "vrm.blendshape.group",
                    Id = "vrm.blendshape.group.schema.json"/* TODO: Fix usage of Id */)]
        public class GroupType
        {
            [JsonField(Name = "name")]
            [JsonSchema(Description = "Expression name")]
            public string Name;

            [JsonField(Name = "presetName")]
            [JsonSchema(Description = "Predefined Expression name")]
            public BlendShapePresetEnum PresetName;

            [JsonField(Name = "binds")]
            [JsonSchema(Description = "Low level blendshape references. ")]
            public List<BindType> Binds = new List<BindType>();

            [JsonField(Name = "materialValues")]
            [JsonSchema(Description = "Material animation references.")]
            public List<MaterialValueBindType> MaterialValues = new List<MaterialValueBindType>();

            [JsonField(Name = "isBinary")]
            [JsonSchema(Description = "0 or 1. Do not allow an intermediate value. Value should rounded")]
            public bool IsBinary;

            //

            [Json(EnumConversion = EnumConversionType.AsString)]
            public enum BlendShapePresetEnum
            {
                [JsonField(Name = "unknown")]
                Unknown,

                [JsonField(Name = "neutral")]
                Neutral,

                [JsonField(Name = "a")]
                A,
                [JsonField(Name = "i")]
                I,
                [JsonField(Name = "u")]
                U,
                [JsonField(Name = "e")]
                E,
                [JsonField(Name = "o")]
                O,

                [JsonField(Name = "blink")]
                Blink,

                // 喜怒哀楽
                [JsonField(Name = "joy")]
                Joy,
                [JsonField(Name = "angry")]
                Angry,
                [JsonField(Name = "sorrow")]
                Sorrow,
                [JsonField(Name = "fun")]
                Fun,

                // LookAt
                [JsonField(Name = "lookup")]
                LookUp,
                [JsonField(Name = "lookdown")]
                LookDown,
                [JsonField(Name = "lookleft")]
                LookLeft,
                [JsonField(Name = "lookright")]
                LookRight,

                [JsonField(Name = "blink_l")]
                Blink_L,
                [JsonField(Name = "blink_r")]
                Blink_R,
            }
        }

        [JsonSchema(Title = "vrm.blendshape.bind",
                    Id = "vrm.blendshape.bind.schema.json"/* TODO: Fix usage of Id */)]
        public class BindType
        {
            // TODO: Make this to an optional value.
            [JsonField(Name = "mesh")]
            [JsonSchema(Minimum = 0), JsonSchemaRequired]
            public int Mesh = -1;

            // TODO: Make this to an optional value.
            [JsonField(Name = "index")]
            [JsonSchema(Minimum = 0), JsonSchemaRequired]
            public int Index = -1;

            [JsonField(Name = "weight")]
            [JsonSchema(Minimum = 0,
                        Maximum = 100,
                        Description = @"SkinnedMeshRenderer.SetBlendShapeWeight"),
             JsonSchemaRequired]
            public float Weight = 0.0f;
        }

        [JsonSchema(Title = "vrm.blendshape.materialbind",
                    Id = "vrm.blendshape.materialbind.schema.json"/* TODO: Fix usage of Id */)]
        public class MaterialValueBindType
        {
            [JsonField(Name = "materialName")]
            public string MaterialName;

            [JsonField(Name = "propertyName")]
            public string PropertyName;

            [JsonField(Name = "targetValue")]
            public float[] TargetValue;
        }
    }
}
