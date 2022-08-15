//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//
using System.Collections.Generic;
using VJson;
using VJson.Schema;

namespace VGltf.Unity.Ext
{
    /// <summary>
    /// Avatar for Unity Humanoid
    /// </summary>
    [Json]
    public class AvatarType
    {
        public static readonly string ExtraName = "VGLTF_unity_avatar";

        [JsonField(Name = "humanDescription")]
        public HumanDescriptionType HumanDescription;

        [Json]
        public class HumanDescriptionType
        {
            [JsonField(Name = "upperArmTwist")]
            public float UpperArmTwist;

            [JsonField(Name = "lowerArmTwist")]
            public float LowerArmTwist;

            [JsonField(Name = "upperLegTwist")]
            public float UpperLegTwist;

            [JsonField(Name = "lowerLegTwist")]
            public float LowerLegTwist;

            [JsonField(Name = "armStretch")]
            public float ArmStretch;

            [JsonField(Name = "legStretch")]
            public float LegStretch;

            [JsonField(Name = "feetSpacing")]
            public float FeetSpacing;

            [JsonField(Name = "human"), JsonFieldIgnorable]
            public List<HumanBone> Human = new List<HumanBone>();

            [JsonField(Name = "skeleton"), JsonFieldIgnorable]
            public List<SkeletonBone> Skeleton = new List<SkeletonBone>();

            [Json]
            public class HumanBone
            {
                [JsonField(Name = "boneName")]
                public string BoneName;

                [JsonField(Name = "humanName")]
                public string HumanName;

                [JsonField(Name = "limit")]
                public HumanLimit Limit;
            }

            [Json]
            public class SkeletonBone
            {
                [JsonField(Name = "name")]
                public string Name;

                [JsonField(Name = "position")]
                [JsonSchema(MinItems = 3, MaxItems = 3)]
                public float[] Position;

                [JsonField(Name = "rotation")]
                [JsonSchema(MinItems = 4, MaxItems = 4)]
                public float[] Rotation;

                [JsonField(Name = "scale")]
                [JsonSchema(MinItems = 3, MaxItems = 3)]
                public float[] Scale;
            }

            [Json]
            public class HumanLimit
            {
                [JsonField(Name = "useDefaultValues")]
                public bool UseDefaultValues;

                [JsonField(Name = "min")]
                [JsonSchema(MinItems = 3, MaxItems = 3)]
                public float[] Min;

                [JsonField(Name = "max")]
                [JsonSchema(MinItems = 3, MaxItems = 3)]
                public float[] Max;

                [JsonField(Name = "center")]
                [JsonSchema(MinItems = 3, MaxItems = 3)]
                public float[] Center;

                [JsonField(Name = "axisLength")]
                public float AxisLength;
            }
        }
    }
}
