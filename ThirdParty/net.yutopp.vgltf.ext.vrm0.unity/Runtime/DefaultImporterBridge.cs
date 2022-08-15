//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VGltf.Ext.Vrm0.Unity.Extensions;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity
{
    public sealed class DefaultImporterBridge : VGltf.Ext.Vrm0.Unity.Bridge.IImporterBridge
    {
        public void ImportMeta(IImporterContext context, VGltf.Ext.Vrm0.Types.Meta vrmMeta, GameObject go)
        {
            var meta = go.AddComponent<VRM0Meta>();

            meta.Title = vrmMeta.Title;
            meta.Version = vrmMeta.Version;
            meta.Author = vrmMeta.Author;
            meta.ContactInformation = vrmMeta.ContactInformation;
            meta.Reference = vrmMeta.Reference;
            // meta.Texture = vrmMeta.Texture; // TODO: support
            meta.AllowedUserName = vrmMeta.AllowedUserName;
            meta.ViolentUsage = vrmMeta.ViolentUsage;
            meta.SexualUsage = vrmMeta.SexualUsage;
            meta.CommercialUsage = vrmMeta.CommercialUsage;
            meta.OtherPermissionUrl = vrmMeta.OtherPermissionUrl;
            meta.License = vrmMeta.License;
            meta.OtherLicenseUrl = vrmMeta.OtherLicenseUrl;
        }

        public void ImportFirstPerson(IImporterContext context, VGltf.Ext.Vrm0.Types.FirstPerson vrmFirstPerson, GameObject go)
        {
            var fp = go.AddComponent<VRM0FirstPerson>();

            var fpBone = context.Resources.Nodes[vrmFirstPerson.FirstPersonBone];
            fp.FirstPersonBone = fpBone.Value.transform;

            fp.FirstPersonOffset = vrmFirstPerson.FirstPersonBoneOffset.ToUnity();

            // TODO: support lookAt* and meshAnnotations
        }

        public void ImportBlendShapeMaster(IImporterContext context, VGltf.Ext.Vrm0.Types.BlendShape vrmBlendShape, GameObject go)
        {
            var proxy = go.AddComponent<VRM0BlendShapeProxy>();

            var smrs = go.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var vrmBlendShapeGroup in vrmBlendShape.BlendShapeGroups)
            {
                var g = new VRM0BlendShapeProxy.BlendShapeGroup();
                g.Name = vrmBlendShapeGroup.Name;
                g.Preset = FromVRM0Preset(vrmBlendShapeGroup.PresetName);
                // TODO: vrmBlendShapeGroup.IsBinary
                // TODO: vrmBlendShapeGroup.MaterialValues

                var meshShapes = new Dictionary<int, VRM0BlendShapeProxy.MeshShape>();

                foreach (var vrmBind in vrmBlendShapeGroup.Binds)
                {
                    if (!meshShapes.TryGetValue(vrmBind.Mesh, out var meshShape))
                    {
                        var meshRes = context.Resources.Meshes[vrmBind.Mesh];
                        var smr = smrs.Where(s => s.sharedMesh == meshRes.Value).First();

                        meshShape = new VRM0BlendShapeProxy.MeshShape();
                        meshShape.SkinnedMeshRenderer = smr;

                        meshShapes.Add(vrmBind.Mesh, meshShape);
                    }

                    var shapeKeyName = meshShape.SkinnedMeshRenderer.sharedMesh.GetBlendShapeName(vrmBind.Index);

                    meshShape.Weights.Add(new VRM0BlendShapeProxy.Weight
                    {
                        ShapeKeyName = shapeKeyName,
                        WeightValue = vrmBind.Weight,
                    });
                }

                foreach (var meshShape in meshShapes.Values)
                {
                    g.MeshShapes.Add(meshShape);
                }

                proxy.Groups.Add(g);
            }
        }

        public void ImportSecondaryAnimation(IImporterContext context, VGltf.Ext.Vrm0.Types.SecondaryAnimation vrmSecondaryAnimation, GameObject go)
        {
            // if the node named "secondary" exists, attach VRM0SecondaryAnimation to this. Otherwise, ignore that.
            var secondaryNode = go.transform.Find("secondary");
            if (secondaryNode == null)
            {
                return;
            }

            var sa = secondaryNode.gameObject.AddComponent<VRM0SecondaryAnimation>();

            sa.Springs = vrmSecondaryAnimation.BoneGroups.Select(vrmBg =>
            {
                var sp = new VRM0SecondaryAnimation.Spring();
                sp.Comment = vrmBg.comment;
                sp.Stiffiness = vrmBg.stiffiness;
                sp.GravityPower = vrmBg.gravityPower;
                sp.GravityDir = vrmBg.gravityDir.ToUnity();
                sp.DragForce = vrmBg.dragForce;
                if (vrmBg.center != -1)
                {
                    sp.Center = context.Resources.Nodes[vrmBg.center].Value.transform;
                }
                sp.HitRadius = vrmBg.hitRadius;
                sp.Bones = vrmBg.Bones.Select(vrmBIndex =>
                {
                    return context.Resources.Nodes[vrmBIndex].Value.transform;
                }).ToArray();
                sp.ColliderGroups = vrmBg.ColliderGroups;

                return sp;
            }).ToArray();

            sa.ColliderGroups = vrmSecondaryAnimation.ColliderGroups.Select(vrmCg =>
            {
                var cg = new VRM0SecondaryAnimation.ColliderGroup();
                cg.Node = context.Resources.Nodes[vrmCg.Node].Value.transform;
                cg.Colliders = vrmCg.Colliders.Select(vrmC =>
                {
                    var c = new VRM0SecondaryAnimation.Collider();
                    c.Offset = vrmC.Offset.ToUnity();
                    c.Radius = vrmC.Radius;

                    return c;
                }).ToArray();

                return cg;
            }).ToArray();
        }

        public Task ReplaceMaterialByMtoon(IImporterContext context, VGltf.Ext.Vrm0.Types.Material matProp, Material mat, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        static VRM0BlendShapeProxy.BlendShapePreset FromVRM0Preset(Types.BlendShape.GroupType.BlendShapePresetEnum kind)
        {
            switch (kind)
            {
                case Types.BlendShape.GroupType.BlendShapePresetEnum.Unknown:
                    return VRM0BlendShapeProxy.BlendShapePreset.Unknown;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.Neutral:
                    return VRM0BlendShapeProxy.BlendShapePreset.Neutral;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.A:
                    return VRM0BlendShapeProxy.BlendShapePreset.A;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.I:
                    return VRM0BlendShapeProxy.BlendShapePreset.I;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.U:
                    return VRM0BlendShapeProxy.BlendShapePreset.U;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.E:
                    return VRM0BlendShapeProxy.BlendShapePreset.E;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.O:
                    return VRM0BlendShapeProxy.BlendShapePreset.O;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.Blink:
                    return VRM0BlendShapeProxy.BlendShapePreset.Blink;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.Joy:
                    return VRM0BlendShapeProxy.BlendShapePreset.Joy;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.Angry:
                    return VRM0BlendShapeProxy.BlendShapePreset.Angry;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.Sorrow:
                    return VRM0BlendShapeProxy.BlendShapePreset.Sorrow;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.Fun:
                    return VRM0BlendShapeProxy.BlendShapePreset.Fun;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.LookUp:
                    return VRM0BlendShapeProxy.BlendShapePreset.LookUp;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.LookDown:
                    return VRM0BlendShapeProxy.BlendShapePreset.LookDown;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.LookLeft:
                    return VRM0BlendShapeProxy.BlendShapePreset.LookLeft;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.LookRight:
                    return VRM0BlendShapeProxy.BlendShapePreset.LookRight;

                case Types.BlendShape.GroupType.BlendShapePresetEnum.Blink_L:
                    return VRM0BlendShapeProxy.BlendShapePreset.Blink_L;
                case Types.BlendShape.GroupType.BlendShapePresetEnum.Blink_R:
                    return VRM0BlendShapeProxy.BlendShapePreset.Blink_R;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}