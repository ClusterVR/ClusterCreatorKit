//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;

namespace VGltf.Ext.Vrm0.Unity.Extensions
{
    public static class HumanoidBoneTypeBoneEnumExtensions
    {
        public static string ToUnity(this VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum e)
        {
            switch (e)
            {
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.hips:
                    return "Hips";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftUpperLeg:
                    return "LeftUpperLeg";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightUpperLeg:
                    return "RightUpperLeg";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLowerLeg:
                    return "LeftLowerLeg";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLowerLeg:
                    return "RightLowerLeg";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftFoot:
                    return "LeftFoot";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightFoot:
                    return "RightFoot";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.spine:
                    return "Spine";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.chest:
                    return "Chest";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.neck:
                    return "Neck";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.head:
                    return "Head";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftShoulder:
                    return "LeftShoulder";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightShoulder:
                    return "RightShoulder";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftUpperArm:
                    return "LeftUpperArm";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightUpperArm:
                    return "RightUpperArm";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLowerArm:
                    return "LeftLowerArm";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLowerArm:
                    return "RightLowerArm";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftHand:
                    return "LeftHand";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightHand:
                    return "RightHand";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftToes:
                    return "LeftToes";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightToes:
                    return "RightToes";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftEye:
                    return "LeftEye";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightEye:
                    return "RightEye";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.jaw:
                    return "Jaw";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbProximal:
                    return "LeftThumbProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbIntermediate:
                    return "LeftThumbIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbDistal:
                    return "LeftThumbDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexProximal:
                    return "LeftIndexProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexIntermediate:
                    return "LeftIndexIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexDistal:
                    return "LeftIndexDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleProximal:
                    return "LeftMiddleProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleIntermediate:
                    return "LeftMiddleIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleDistal:
                    return "LeftMiddleDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingProximal:
                    return "LeftRingProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingIntermediate:
                    return "LeftRingIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingDistal:
                    return "LeftRingDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleProximal:
                    return "LeftLittleProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleIntermediate:
                    return "LeftLittleIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleDistal:
                    return "LeftLittleDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbProximal:
                    return "RightThumbProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbIntermediate:
                    return "RightThumbIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbDistal:
                    return "RightThumbDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexProximal:
                    return "RightIndexProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexIntermediate:
                    return "RightIndexIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexDistal:
                    return "RightIndexDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleProximal:
                    return "RightMiddleProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleIntermediate:
                    return "RightMiddleIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleDistal:
                    return "RightMiddleDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingProximal:
                    return "RightRingProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingIntermediate:
                    return "RightRingIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingDistal:
                    return "RightRingDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleProximal:
                    return "RightLittleProximal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleIntermediate:
                    return "RightLittleIntermediate";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleDistal:
                    return "RightLittleDistal";
                case VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.upperChest:
                    return "UpperChest";
                default:
                    throw new ArgumentException();
            }
        }
    }

    public static class UnityHumanBoneNameStringExtensions
    {
        public static VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum AsHumanBoneNameToVrm(this string n)
        {
            switch (n.Replace(" ", String.Empty))
            {
                case "Hips":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.hips;
                case "LeftUpperLeg":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftUpperLeg;
                case "RightUpperLeg":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightUpperLeg;
                case "LeftLowerLeg":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLowerLeg;
                case "RightLowerLeg":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLowerLeg;
                case "LeftFoot":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftFoot;
                case "RightFoot":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightFoot;
                case "Spine":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.spine;
                case "Chest":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.chest;
                case "Neck":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.neck;
                case "Head":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.head;
                case "LeftShoulder":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftShoulder;
                case "RightShoulder":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightShoulder;
                case "LeftUpperArm":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftUpperArm;
                case "RightUpperArm":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightUpperArm;
                case "LeftLowerArm":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLowerArm;
                case "RightLowerArm":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLowerArm;
                case "LeftHand":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftHand;
                case "RightHand":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightHand;
                case "LeftToes":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftToes;
                case "RightToes":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightToes;
                case "LeftEye":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftEye;
                case "RightEye":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightEye;
                case "Jaw":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.jaw;
                case "LeftThumbProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbProximal;
                case "LeftThumbIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbIntermediate;
                case "LeftThumbDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftThumbDistal;
                case "LeftIndexProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexProximal;
                case "LeftIndexIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexIntermediate;
                case "LeftIndexDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftIndexDistal;
                case "LeftMiddleProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleProximal;
                case "LeftMiddleIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleIntermediate;
                case "LeftMiddleDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftMiddleDistal;
                case "LeftRingProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingProximal;
                case "LeftRingIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingIntermediate;
                case "LeftRingDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftRingDistal;
                case "LeftLittleProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleProximal;
                case "LeftLittleIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleIntermediate;
                case "LeftLittleDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.leftLittleDistal;
                case "RightThumbProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbProximal;
                case "RightThumbIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbIntermediate;
                case "RightThumbDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightThumbDistal;
                case "RightIndexProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexProximal;
                case "RightIndexIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexIntermediate;
                case "RightIndexDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightIndexDistal;
                case "RightMiddleProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleProximal;
                case "RightMiddleIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleIntermediate;
                case "RightMiddleDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightMiddleDistal;
                case "RightRingProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingProximal;
                case "RightRingIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingIntermediate;
                case "RightRingDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightRingDistal;
                case "RightLittleProximal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleProximal;
                case "RightLittleIntermediate":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleIntermediate;
                case "RightLittleDistal":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.rightLittleDistal;
                case "UpperChest":
                    return VGltf.Ext.Vrm0.Types.Humanoid.BoneType.BoneEnum.upperChest;
                default:
                    throw new ArgumentException($"'{n}' is not supported");
            }
        }
    }
}
