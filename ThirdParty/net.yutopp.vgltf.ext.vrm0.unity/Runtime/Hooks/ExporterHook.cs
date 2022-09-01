//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using UnityEngine;
using VGltf.Unity;
using VGltf.Types.Extensions;
using System;
using System.Linq;
using VGltf.Ext.Vrm0.Unity.Extensions;

namespace VGltf.Ext.Vrm0.Unity.Hooks
{
    public class ExporterHook : ExporterHookBase
    {
        readonly Bridge.IExporterBridge _bridge;

        public ExporterHook(Bridge.IExporterBridge bridge)
        {
            _bridge = bridge;
        }

        public override void PostHook(Exporter exporter, GameObject go)
        {
            var gltf = exporter.Context.Gltf;

            var extVrm = new Types.Vrm();
            extVrm.ExporterVersion = "VGltf.Ext.Vrm0";

            ExportMeta(exporter, extVrm, go);
            ExportHumanoid(exporter, extVrm, go);
            ExportFirstPerson(exporter, extVrm, go);
            ExportBlendShapeMaster(exporter, extVrm, go);
            ExportSecondaryAnimation(exporter, extVrm, go);
            ExportMaterial(exporter, extVrm);

            //
            gltf.AddExtension(Types.Vrm.ExtensionName, extVrm);
            gltf.AddExtensionUsed(Types.Vrm.ExtensionName);
        }

        void ExportMeta(Exporter exporter, Types.Vrm extVrm, GameObject go)
        {
            _bridge.ExportMeta(exporter, extVrm, go);
        }

        void ExportHumanoid(Exporter exporter, Types.Vrm extVrm, GameObject go)
        {
            var anim = go.GetComponent<Animator>();
            if (anim == null)
            {
                throw new Exception("There is no Animation component");
            }

            var avatar = anim.avatar;
            if (!avatar.isValid || !avatar.isHuman)
            {
                throw new Exception("Avatar is invalid or not human");
            }

            var hd = avatar.humanDescription;

            var vrmHum = new Types.Humanoid();

            vrmHum.UpperArmTwist = hd.upperArmTwist;
            vrmHum.LowerArmTwist = hd.lowerArmTwist;
            vrmHum.UpperLegTwist = hd.upperLegTwist;
            vrmHum.LowerLegTwist = hd.lowerLegTwist;
            vrmHum.ArmStretch = hd.armStretch;
            vrmHum.LegStretch = hd.legStretch;
            vrmHum.FeetSpacing = hd.feetSpacing;
            vrmHum.HasTranslationDoF = hd.hasTranslationDoF;

            vrmHum.HumanBones = hd.human.Select(h =>
            {
                var vrmHumBone = new Types.Humanoid.BoneType();

                // HumanLimit (NOTE: coord is still same as Unity. not GL)
                vrmHumBone.UseDefaultValues = h.limit.useDefaultValues;
                vrmHumBone.Min = h.limit.min.ToVrm();
                vrmHumBone.Max = h.limit.max.ToVrm();
                vrmHumBone.Center = h.limit.center.ToVrm();
                vrmHumBone.AxisLength = h.limit.axisLength;

                // HumanBone
                vrmHumBone.Bone = h.humanName.AsHumanBoneNameToVrm();

                //var boneTrans = nodeTransMap[h.boneName];
                if (!exporter.Context.Resources.Nodes.TryGetValueByName(h.boneName, out var boneNode))
                {
                    throw new Exception($"bone transform is not found or not unique: name={h.boneName}");
                }
                vrmHumBone.Node = boneNode.Index;

                return vrmHumBone;
            }).ToList();

            extVrm.Humanoid = vrmHum;
        }

        void ExportFirstPerson(Exporter exporter, Types.Vrm extVrm, GameObject go)
        {
            _bridge.ExportFirstPerson(exporter.Context, extVrm, go);
        }

        void ExportBlendShapeMaster(Exporter exporter, Types.Vrm extVrm, GameObject go)
        {
            _bridge.ExportBlendShapeMaster(exporter, extVrm, go);
        }

        void ExportSecondaryAnimation(Exporter exporter, Types.Vrm extVrm, GameObject go)
        {
            _bridge.ExportSecondaryAnimation(exporter.Context, extVrm, go);
        }

        void ExportMaterial(Exporter exporter, Types.Vrm extVrm)
        {
            var vrmMats = exporter.Context.Resources.Materials.Map(matRes =>
            {
                var vrmMat = _bridge.CreateMaterialProp(exporter.Context, matRes.Value);
                return (matRes.Index, vrmMat);
            }).OrderBy(tup => tup.Index).Select(tup => tup.vrmMat).ToList();

            extVrm.MaterialProperties = vrmMats;
        }
    }
}
