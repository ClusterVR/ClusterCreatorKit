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
    [JsonSchema(Title = "vrm.secondaryanimation",
                Description = "The setting of automatic animation of string-like objects such as tails and hairs.",
                Id = "vrm.secondaryanimation.schema.json"/* TODO: Fix usage of Id */)]
    public class SecondaryAnimation
    {
        [JsonField(Name = "boneGroups")]
        [JsonFieldIgnorable(WhenLengthIs = 0)]
        public List<Spring> BoneGroups = new List<Spring>();

        [JsonField(Name = "colliderGroups")]
        [JsonFieldIgnorable(WhenLengthIs = 0)]
        public List<ColliderGroup> ColliderGroups = new List<ColliderGroup>();

        //

        [JsonSchema(Title = "vrm.secondaryanimation.spring",
                    Id = "vrm.secondaryanimation.spring.schema.json"/* TODO: Fix usage of Id */)]
        public class Spring
        {
            [JsonField(Name = "comment"), JsonFieldIgnorable]
            [JsonSchema(Description = "Annotation comment")]
            public string comment;

            [JsonField(Name = "stiffiness")]
            [JsonSchema(Description = "The resilience of the swaying object (the power of returning to the initial pose).")]
            public float stiffiness;

            [JsonField(Name = "gravityPower")]
            [JsonSchema(Description = "The strength of gravity.")]
            public float gravityPower;

            [JsonField(Name = "gravityDir")]
            [JsonSchema(Description = "The direction of gravity. Set (0, -1, 0) for simulating the gravity. Set (1, 0, 0) for simulating the wind.")]
            public Vector3 gravityDir;

            [JsonField(Name = "dragForce")]
            [JsonSchema(Description = "The resistance (deceleration) of automatic animation.")]
            public float dragForce;

            // NOTE: This value denotes index but may contain -1 as a value.
            // When the value is -1, it means that center node is not specified.
            // This is a historical issue and a compromise for forward compatibility.
            [JsonField(Name = "center")]
            [JsonSchema(Description = "The reference point of a swaying object can be set at any location except the origin. When implementing UI moving with warp, the parent node to move with warp can be specified if you don't want to make the object swaying with warp movement.")]
            public int center;

            [JsonField(Name = "hitRadius")]
            [JsonSchema(Description = "The radius of the sphere used for the collision detection with colliders.")]
            public float hitRadius;

            [JsonField(Name = "bones")]
            [JsonSchema(Description = "Specify the node index of the root bone of the swaying object.")]
            [ItemsJsonSchema(Minimum = 0)]
            public int[] Bones = new int[] { };

            [JsonField(Name = "colliderGroups")]
            [JsonSchema(Description = "Specify the index of the collider group for collisions with swaying objects.")]
            [ItemsJsonSchema(Minimum = 0)]
            public int[] ColliderGroups = new int[] { };
        }

        [JsonSchema(Title = "vrm.secondaryanimation.collidergroup",
                    Description = "Set sphere balls for colliders used for collision detections with swaying objects.",
                    Id = "vrm.secondaryanimation.collidergroup.schema.json"/* TODO: Fix usage of Id */)]
        public class ColliderGroup
        {
            [JsonField(Name = "node")]
            [JsonSchema(Minimum = 0,
                        Description = "The node of the collider group for setting up collision detections.")]
            public int Node;

            [JsonField(Name = "colliders")]
            public List<Collider> Colliders = new List<Collider>();
        }

        [JsonSchema(Title = "vrm.secondaryanimation.collider",
                    Id = "vrm.secondaryanimation.collider.schema.json"/* TODO: Fix usage of Id */)]
        public class Collider
        {
            [JsonField(Name = "offset")]
            [JsonSchema(Description = "The local coordinate from the node of the collider group.")]
            public Vector3 Offset;

            [JsonField(Name = "radius")]
            [JsonSchema(Description = "The radius of the collider.")]
            public float Radius;
        }
    }
}
