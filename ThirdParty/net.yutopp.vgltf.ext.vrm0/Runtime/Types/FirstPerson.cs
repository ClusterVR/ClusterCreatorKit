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
    [JsonSchema(Title = "vrm.firstperson",
                Id = "vrm.firstperson.schema.json"/* TODO: Fix usage of Id */)]
    public class FirstPerson
    {
        // When the value is -1, it means that no bone for first person is found.
        // TODO: Make this to an optional value
        [JsonField(Name = "firstPersonBone"), JsonFieldIgnorable(WhenValueIs = -1)]
        [JsonSchema(Description = "The bone whose rendering should be turned off in first-person view. Usually Head is specified.",
                    Minimum = 0)]
        public int FirstPersonBone = -1;

        [JsonField(Name = "firstPersonBoneOffset")]
        [JsonSchema(Description = "The target position of the VR headset in first-person view. It is assumed that an offset from the head bone to the VR headset is added.")]
        public Vector3 FirstPersonBoneOffset;

        [JsonField(Name = "meshAnnotations")]
        [JsonSchema(Description = "Switch display / undisplay for each mesh in first-person view or the others.")]
        public List<MeshAnnotationType> MeshAnnotations = new List<MeshAnnotationType>();

        // lookat
        [JsonField(Name = "lookAtTypeName")]
        [JsonSchema(Description = "Eye controller mode.")]
        public LookAtTypeEnum LookAtType = LookAtTypeEnum.Bone;

        [JsonField(Name = "lookAtHorizontalInner")]
        [JsonSchema(Description = "Eye controller setting.")]
        public DegreeMapType LookAtHorizontalInner = new DegreeMapType();

        [JsonField(Name = "lookAtHorizontalOuter")]
        [JsonSchema(Description = "Eye controller setting.")]
        public DegreeMapType LookAtHorizontalOuter = new DegreeMapType();

        [JsonField(Name = "lookAtVerticalDown")]
        [JsonSchema(Description = "Eye controller setting.")]
        public DegreeMapType LookAtVerticalDown = new DegreeMapType();

        [JsonField(Name = "lookAtVerticalUp")]
        [JsonSchema(Description = "Eye controller setting.")]
        public DegreeMapType LookAtVerticalUp = new DegreeMapType();

        //

        [JsonSchema(Title = "vrm.firstperson.meshannotation",
                    Id = "vrm.firstperson.meshannotation.schema.json"/* TODO: Fix usage of Id */)]
        public class MeshAnnotationType
        {
            // TODO: Make this to an optional value.
            // When the value is -1, it means that no target mesh is found.
            [JsonField(Name = "mesh")]
            [JsonSchema(Minimum = 0)]
            public int Mesh;

            [JsonField(Name = "firstPersonFlag")]
            public FirstPersonFlagEnum FirstPersonFlag;

            //

            [Json(EnumConversion = EnumConversionType.AsString)]
            public enum FirstPersonFlagEnum
            {
                [JsonField] Auto, // Create headlessModel
                [JsonField] Both, // Default layer
                [JsonField] ThirdPersonOnly,
                [JsonField] FirstPersonOnly,
            }
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum LookAtTypeEnum
        {
            [JsonField] Bone,
            [JsonField] BlendShape,
        }

        [JsonSchema(Title = "vrm.firstperson.degreemap",
                    Id = "vrm.firstperson.degreemap.schema.json"/* TODO: Fix usage of Id */)]
        public class DegreeMapType
        {
            [JsonField(Name = "curve"), JsonFieldIgnorable]
            [JsonSchema(Description = "None linear mapping params. time, value, inTangent, outTangent")]
            public float[] Curve;

            [JsonField(Name = "xRange")]
            [JsonSchema(Description = "Look at input clamp range degree.")]
            public float XRange = 90.0f;

            [JsonField(Name = "yRange")]
            [JsonSchema(Description = "Look at map range degree from xRange.")]
            public float YRange = 10.0f;
        }
    }
}
