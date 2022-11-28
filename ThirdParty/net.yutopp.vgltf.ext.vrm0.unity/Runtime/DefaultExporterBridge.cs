//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Linq;
using UnityEngine;
using VGltf.Ext.Vrm0.Unity.Extensions;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity
{
    public sealed class DefaultExporterBridge : VGltf.Ext.Vrm0.Unity.Bridge.IExporterBridge
    {
        private readonly DefaultMaterialExporterBridge _materialExporterBridge = new ();

        public void ExportMeta(Exporter exporter, VGltf.Ext.Vrm0.Types.Vrm vrm, GameObject go)
        {
            var meta = go.GetComponent<VRM0Meta>();
            if (meta == null)
            {
                throw new Exception("There is no VRM0Meta component");
            }

            var vrmMeta = new Types.Meta();

            vrmMeta.Title = meta.Title;
            vrmMeta.Version = meta.Version;
            vrmMeta.Author = meta.Author;
            vrmMeta.ContactInformation = meta.ContactInformation;
            vrmMeta.Reference = meta.Reference;
            vrmMeta.Texture = -1; // ???, TODO: implement
            vrmMeta.AllowedUserName = meta.AllowedUserName;
            vrmMeta.ViolentUsage = meta.ViolentUsage;
            vrmMeta.SexualUsage = meta.SexualUsage;
            vrmMeta.CommercialUsage = meta.CommercialUsage;
            vrmMeta.OtherPermissionUrl = meta.OtherPermissionUrl;
            vrmMeta.License = meta.License;
            vrmMeta.OtherLicenseUrl = meta.OtherLicenseUrl;

            vrm.Meta = vrmMeta;
        }

        public void ExportFirstPerson(IExporterContext context, VGltf.Ext.Vrm0.Types.Vrm vrm, GameObject go)
        {
            var fp = go.GetComponent<VRM0FirstPerson>();
            if (fp == null)
            {
                // firstperson is optional
                return;
            }

            var vrmFirstPerson = new Types.FirstPerson();

            if (!context.Resources.Nodes.TryGetValue(fp.FirstPersonBone.gameObject, out var res))
            {
                throw new Exception($"first person bone is not found");
            }
            vrmFirstPerson.FirstPersonBone = res.Index;

            vrmFirstPerson.FirstPersonBoneOffset = fp.FirstPersonOffset.ToVrm();

            vrm.FirstPerson = vrmFirstPerson;

            // TODO: support lookAt* and meshAnnotations
        }

        public void ExportBlendShapeMaster(Exporter exporter, VGltf.Ext.Vrm0.Types.Vrm vrm, GameObject go)
        {
            var proxy = go.GetComponent<VRM0BlendShapeProxy>();
            if (proxy == null)
            {
                // blendshape proxy is optional
                return;
            }

            foreach (var proxyGroup in proxy.Groups)
            {
                var g = new Types.BlendShape.GroupType();
                g.Name = proxyGroup.Name;
                g.PresetName = ToVRM0Preset(proxyGroup.Preset);

                foreach (var shape in proxyGroup.MeshShapes)
                {
                    var smr = shape.SkinnedMeshRenderer;
                    if (!exporter.Context.Resources.Meshes.TryGetValueByName(smr.sharedMesh.name, out var mesh))
                    {
                        continue;
                    }

                    foreach (var weight in shape.Weights)
                    {
                        var index = mesh.Value.GetBlendShapeIndex(weight.ShapeKeyName);
                        if (index == -1)
                        {
                            continue;
                        }

                        g.Binds.Add(new Types.BlendShape.BindType
                        {
                            Mesh = mesh.Index,
                            Index = index,
                            Weight = weight.WeightValue,
                        });
                    }
                }

                vrm.BlendShapeMaster.BlendShapeGroups.Add(g);
            }
        }

        public void ExportSecondaryAnimation(IExporterContext context, VGltf.Ext.Vrm0.Types.Vrm vrm, GameObject go)
        {
            var sa = go.GetComponent<VRM0SecondaryAnimation>();
            if (sa == null)
            {
                // If the node named "secondary" exists, attach VRM0SecondaryAnimation might be attached to this. Thus check that.
                var secondaryNode = go.transform.Find("secondary");
                if (secondaryNode == null)
                {
                    return;
                }

                sa = secondaryNode.gameObject.GetComponent<VRM0SecondaryAnimation>();
                if (sa == null)
                {
                    // secondary animation is optional
                    return;
                }
            }

            var vrmSecondaryAnimation = new Types.SecondaryAnimation();

            vrmSecondaryAnimation.BoneGroups = sa.Springs.Select(sp =>
            {
                var vrmBg = new Types.SecondaryAnimation.Spring();
                vrmBg.comment = sp.Comment;
                vrmBg.stiffiness = sp.Stiffiness;
                vrmBg.gravityPower = sp.GravityPower;
                vrmBg.gravityDir = sp.GravityDir.ToVrm();
                vrmBg.dragForce = sp.DragForce;
                if (sp.Center != null)
                {
                    if (!context.Resources.Nodes.TryGetValue(sp.Center.gameObject, out var res))
                    {
                        throw new Exception($"center object is not found: name={sp.Center.name}");
                    }
                    vrmBg.center = res.Index;
                }
                else
                {
                    vrmBg.center = -1; // For compatibility...
                }
                vrmBg.hitRadius = sp.HitRadius;
                vrmBg.Bones = sp.Bones.Select(vrmBTrans =>
                {
                    if (!context.Resources.Nodes.TryGetValue(vrmBTrans.gameObject, out var res))
                    {
                        throw new Exception($"bone object is not found: name={vrmBTrans.name}");
                    }
                    return res.Index;
                }).ToArray();
                vrmBg.ColliderGroups = sp.ColliderGroups.Select((vrmCgTrans, i) =>
                {
                    if (vrmCgTrans < 0 || vrmCgTrans >= sa.ColliderGroups.Length)
                    {
                        throw new Exception($"collider group[{i}] is out of range: [0, {sa.ColliderGroups.Length}]");
                    }
                    return vrmCgTrans;
                }).ToArray();

                return vrmBg;
            }).ToList();

            vrmSecondaryAnimation.ColliderGroups = sa.ColliderGroups.Select(cg =>
            {
                var vrmCg = new Types.SecondaryAnimation.ColliderGroup();

                if (!context.Resources.Nodes.TryGetValue(cg.Node.gameObject, out var res))
                {
                    throw new Exception($"collider group node is not found: name={cg.Node.name}");
                }
                vrmCg.Node = res.Index;

                vrmCg.Colliders = cg.Colliders.Select(c =>
                {
                    var vrmC = new Types.SecondaryAnimation.Collider();
                    vrmC.Offset = c.Offset.ToVrm();
                    vrmC.Radius = c.Radius;

                    return vrmC;
                }).ToList();

                return vrmCg;
            }).ToList();

            vrm.SecondaryAnimation = vrmSecondaryAnimation;
        }

        public Types.Material CreateMaterialProp(IExporterContext context, Material mat) => _materialExporterBridge.CreateMaterialProp(context, mat);
        
        static Types.BlendShape.GroupType.BlendShapePresetEnum ToVRM0Preset(VRM0BlendShapeProxy.BlendShapePreset kind)
        {
            switch (kind)
            {
                case VRM0BlendShapeProxy.BlendShapePreset.Unknown:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Unknown;

                case VRM0BlendShapeProxy.BlendShapePreset.Neutral:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Neutral;

                case VRM0BlendShapeProxy.BlendShapePreset.A:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.A;
                case VRM0BlendShapeProxy.BlendShapePreset.I:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.I;
                case VRM0BlendShapeProxy.BlendShapePreset.U:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.U;
                case VRM0BlendShapeProxy.BlendShapePreset.E:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.E;
                case VRM0BlendShapeProxy.BlendShapePreset.O:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.O;

                case VRM0BlendShapeProxy.BlendShapePreset.Blink:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Blink;

                case VRM0BlendShapeProxy.BlendShapePreset.Joy:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Joy;
                case VRM0BlendShapeProxy.BlendShapePreset.Angry:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Angry;
                case VRM0BlendShapeProxy.BlendShapePreset.Sorrow:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Sorrow;
                case VRM0BlendShapeProxy.BlendShapePreset.Fun:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Fun;

                case VRM0BlendShapeProxy.BlendShapePreset.LookUp:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.LookUp;
                case VRM0BlendShapeProxy.BlendShapePreset.LookDown:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.LookDown;
                case VRM0BlendShapeProxy.BlendShapePreset.LookLeft:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.LookLeft;
                case VRM0BlendShapeProxy.BlendShapePreset.LookRight:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.LookRight;

                case VRM0BlendShapeProxy.BlendShapePreset.Blink_L:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Blink_L;
                case VRM0BlendShapeProxy.BlendShapePreset.Blink_R:
                    return Types.BlendShape.GroupType.BlendShapePresetEnum.Blink_R;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
