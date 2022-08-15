//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Linq;
using UnityEngine;
using VGltf.Types.Extensions;

namespace VGltf.Unity.Ext
{
    public class AvatarExporter : NodeExporterHook
    {
        public override void PostHook(NodeExporter exporter, GameObject go, VGltf.Types.Node gltfNode)
        {
            var anim = go.GetComponent<Animator>();
            if (anim == null)
            {
                return;
            }

            var extAvatar = new AvatarType();

            var hd = anim.avatar.humanDescription;
            var extHD = new AvatarType.HumanDescriptionType();

            extHD.UpperArmTwist = hd.upperArmTwist;
            extHD.LowerArmTwist = hd.lowerArmTwist;
            extHD.UpperLegTwist = hd.upperLegTwist;
            extHD.LowerLegTwist = hd.lowerLegTwist;
            extHD.ArmStretch = hd.armStretch;
            extHD.LegStretch = hd.legStretch;
            extHD.FeetSpacing = hd.feetSpacing;

            // Regenerate skeleton by referencing bones to support normalized bones.
            var allNodes = anim.GetComponentsInChildren<Transform>();
            extHD.Skeleton = allNodes.Where(n =>
            {
                return !(
                    (n.GetComponent<MeshRenderer>() != null) ||
                    (n.GetComponent<SkinnedMeshRenderer>() != null)
                );
            }).Select(n =>
            {
                // TODO: Coord
                return new AvatarType.HumanDescriptionType.SkeletonBone
                {
                    Name = n.name,
                    Position = PrimitiveExporter.AsArray(n.localPosition),
                    Rotation = PrimitiveExporter.AsArray(n.localRotation),
                    Scale = PrimitiveExporter.AsArray(n.localScale),
                };
            }).ToList();

            extHD.Human = hd.human.Select(h =>
            {
                // TODO: Coord
                var extLimit = new AvatarType.HumanDescriptionType.HumanLimit
                {
                    UseDefaultValues = h.limit.useDefaultValues,
                    Min = PrimitiveExporter.AsArray(h.limit.min),
                    Max = PrimitiveExporter.AsArray(h.limit.max),
                    Center = PrimitiveExporter.AsArray(h.limit.center),
                    AxisLength = h.limit.axisLength,
                };

                return new AvatarType.HumanDescriptionType.HumanBone
                {
                    BoneName = h.boneName,
                    HumanName = h.humanName,
                    Limit = extLimit,
                };
            }).ToList();

            extAvatar.HumanDescription = extHD;

            gltfNode.AddExtra(AvatarType.ExtraName, extAvatar);
        }
    }
}
