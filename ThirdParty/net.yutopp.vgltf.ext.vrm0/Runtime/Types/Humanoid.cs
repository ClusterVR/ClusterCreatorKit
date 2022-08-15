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
    [JsonSchema(Title = "vrm.humanoid",
                Id = "vrm.humanoid.schema.json"/* TODO: Fix usage of Id */)]
    public class Humanoid
    {
        [JsonField(Name = "humanBones")]
        public List<BoneType> HumanBones = new List<BoneType>();

        [JsonField(Name = "armStretch")]
        [JsonSchema(Description = "Unity's HumanDescription.armStretch")]
        public float ArmStretch = 0.05f;

        [JsonField(Name = "legStretch")]
        [JsonSchema(Description = "Unity's HumanDescription.legStretch")]
        public float LegStretch = 0.05f;

        [JsonField(Name = "upperArmTwist")]
        [JsonSchema(Description = "Unity's HumanDescription.upperArmTwist")]
        public float UpperArmTwist = 0.5f;

        [JsonField(Name = "lowerArmTwist")]
        [JsonSchema(Description = "Unity's HumanDescription.lowerArmTwist")]
        public float LowerArmTwist = 0.5f;

        [JsonField(Name = "upperLegTwist")]
        [JsonSchema(Description = "Unity's HumanDescription.upperLegTwist")]
        public float UpperLegTwist = 0.5f;

        [JsonField(Name = "lowerLegTwist")]
        [JsonSchema(Description = "Unity's HumanDescription.lowerLegTwist")]
        public float LowerLegTwist = 0.5f;

        [JsonField(Name = "feetSpacing")]
        [JsonSchema(Description = "Unity's HumanDescription.feetSpacing")]
        public float FeetSpacing = 0;

        [JsonField(Name = "hasTranslationDoF")]
        [JsonSchema(Description = "Unity's HumanDescription.hasTranslationDoF")]
        public bool HasTranslationDoF = false;

        //

        [JsonSchema(Title = "vrm.humanoid.bone",
                    Id = "vrm.humanoid.bone.schema.json"/* TODO: Fix usage of Id */)]
        public class BoneType
        {
            [JsonField(Name = "bone")]
            [JsonSchema(Description = "Human bone name.")]
            public BoneEnum Bone;

            // When the value is -1, it means that no node is found.
            // TODO: Make this to an optional value
            [JsonField(Name = "node")]
            [JsonSchema(Description = "Reference node index", Minimum = 0)]
            public int Node = -1;

            [JsonField(Name = "useDefaultValues")]
            [JsonSchema(Description = "Unity's HumanLimit.useDefaultValues")]
            public bool UseDefaultValues = true;

            [JsonField(Name = "min")]
            [JsonSchema(Description = "Unity's HumanLimit.min")]
            public Vector3 Min;

            [JsonField(Name = "max")]
            [JsonSchema(Description = "Unity's HumanLimit.max")]
            public Vector3 Max;

            [JsonField(Name = "center")]
            [JsonSchema(Description = "Unity's HumanLimit.center")]
            public Vector3 Center;

            [JsonField(Name = "axisLength")]
            [JsonSchema(Description = "Unity's HumanLimit.axisLength")]
            public float AxisLength;

            //

            [Json(EnumConversion = EnumConversionType.AsString)]
            public enum BoneEnum
            {
                [JsonField] hips,
                [JsonField] leftUpperLeg,
                [JsonField] rightUpperLeg,
                [JsonField] leftLowerLeg,
                [JsonField] rightLowerLeg,
                [JsonField] leftFoot,
                [JsonField] rightFoot,
                [JsonField] spine,
                [JsonField] chest,
                [JsonField] neck,
                [JsonField] head,
                [JsonField] leftShoulder,
                [JsonField] rightShoulder,
                [JsonField] leftUpperArm,
                [JsonField] rightUpperArm,
                [JsonField] leftLowerArm,
                [JsonField] rightLowerArm,
                [JsonField] leftHand,
                [JsonField] rightHand,
                [JsonField] leftToes,
                [JsonField] rightToes,
                [JsonField] leftEye,
                [JsonField] rightEye,
                [JsonField] jaw,
                [JsonField] leftThumbProximal,
                [JsonField] leftThumbIntermediate,
                [JsonField] leftThumbDistal,
                [JsonField] leftIndexProximal,
                [JsonField] leftIndexIntermediate,
                [JsonField] leftIndexDistal,
                [JsonField] leftMiddleProximal,
                [JsonField] leftMiddleIntermediate,
                [JsonField] leftMiddleDistal,
                [JsonField] leftRingProximal,
                [JsonField] leftRingIntermediate,
                [JsonField] leftRingDistal,
                [JsonField] leftLittleProximal,
                [JsonField] leftLittleIntermediate,
                [JsonField] leftLittleDistal,
                [JsonField] rightThumbProximal,
                [JsonField] rightThumbIntermediate,
                [JsonField] rightThumbDistal,
                [JsonField] rightIndexProximal,
                [JsonField] rightIndexIntermediate,
                [JsonField] rightIndexDistal,
                [JsonField] rightMiddleProximal,
                [JsonField] rightMiddleIntermediate,
                [JsonField] rightMiddleDistal,
                [JsonField] rightRingProximal,
                [JsonField] rightRingIntermediate,
                [JsonField] rightRingDistal,
                [JsonField] rightLittleProximal,
                [JsonField] rightLittleIntermediate,
                [JsonField] rightLittleDistal,
                [JsonField] upperChest,

                [JsonField] unknown, // TODO: remove
            }
        }
    }
}
