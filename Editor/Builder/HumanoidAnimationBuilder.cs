using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class HumanoidAnimationBuilder
    {
        public static HumanoidAnimation Build(AnimationClip animation)
        {
            if (animation == null)
            {
                return null;
            }

            var curves = new List<HumanoidAnimationCurve>();
            var bindings = AnimationUtility.GetCurveBindings(animation);
            foreach (var binding in bindings)
            {
                if (!string.IsNullOrEmpty(binding.path))
                {
                    continue;
                }

                if (TryGetHumanoidAnimationCurvePropertyName(binding.propertyName, out var propertyName))
                {
                    curves.Add(new(propertyName, AnimationUtility.GetEditorCurve(animation, binding)));
                }
            }
            var humanoidAnimation = ScriptableObject.CreateInstance<HumanoidAnimation>();
            humanoidAnimation.Construct(animation.length, animation.isLooping, curves);
            return humanoidAnimation;
        }

        static bool TryGetHumanoidAnimationCurvePropertyName(string propertyNameString, out HumanoidAnimationCurvePropertyName propertyName)
        {
            bool result;
            (result, propertyName) = GetHumanoidAnimationCurvePropertyName(propertyNameString);
            return result;
        }

        static (bool, HumanoidAnimationCurvePropertyName) GetHumanoidAnimationCurvePropertyName(string path)
            => path switch
            {
                "Spine Front-Back" => (true, HumanoidAnimationCurvePropertyName.SpineFrontBack),
                "Spine Left-Right" => (true, HumanoidAnimationCurvePropertyName.SpineLeftRight),
                "Spine Twist Left-Right" => (true, HumanoidAnimationCurvePropertyName.SpineTwistLeftRight),
                "Chest Front-Back" => (true, HumanoidAnimationCurvePropertyName.ChestFrontBack),
                "Chest Left-Right" => (true, HumanoidAnimationCurvePropertyName.ChestLeftRight),
                "Chest Twist Left-Right" => (true, HumanoidAnimationCurvePropertyName.ChestTwistLeftRight),
                "UpperChest Front-Back" => (true, HumanoidAnimationCurvePropertyName.UpperChestFrontBack),
                "UpperChest Left-Right" => (true, HumanoidAnimationCurvePropertyName.UpperChestLeftRight),
                "UpperChest Twist Left-Right" => (true, HumanoidAnimationCurvePropertyName.UpperChestTwistLeftRight),
                "Neck Nod Down-Up" => (true, HumanoidAnimationCurvePropertyName.NeckNodDownUp),
                "Neck Tilt Left-Right" => (true, HumanoidAnimationCurvePropertyName.NeckTiltLeftRight),
                "Neck Turn Left-Right" => (true, HumanoidAnimationCurvePropertyName.NeckTurnLeftRight),
                "Head Nod Down-Up" => (true, HumanoidAnimationCurvePropertyName.HeadNodDownUp),
                "Head Tilt Left-Right" => (true, HumanoidAnimationCurvePropertyName.HeadTiltLeftRight),
                "Head Turn Left-Right" => (true, HumanoidAnimationCurvePropertyName.HeadTurnLeftRight),
                "Left Eye Down-Up" => (true, HumanoidAnimationCurvePropertyName.LeftEyeDownUp),
                "Left Eye In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftEyeInOut),
                "Right Eye Down-Up" => (true, HumanoidAnimationCurvePropertyName.RightEyeDownUp),
                "Right Eye In-Out" => (true, HumanoidAnimationCurvePropertyName.RightEyeInOut),
                "Jaw Close" => (true, HumanoidAnimationCurvePropertyName.JawClose),
                "Jaw Left-Right" => (true, HumanoidAnimationCurvePropertyName.JawLeftRight),
                "Left Upper Leg Front-Back" => (true, HumanoidAnimationCurvePropertyName.LeftUpperLegFrontBack),
                "Left Upper Leg In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftUpperLegInOut),
                "Left Upper Leg Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftUpperLegTwistInOut),
                "Left Lower Leg Stretch" => (true, HumanoidAnimationCurvePropertyName.LeftLowerLegStretch),
                "Left Lower Leg Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftLowerLegTwistInOut),
                "Left Foot Up-Down" => (true, HumanoidAnimationCurvePropertyName.LeftFootUpDown),
                "Left Foot Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftFootTwistInOut),
                "Left Toes Up-Down" => (true, HumanoidAnimationCurvePropertyName.LeftToesUpDown),
                "Right Upper Leg Front-Back" => (true, HumanoidAnimationCurvePropertyName.RightUpperLegFrontBack),
                "Right Upper Leg In-Out" => (true, HumanoidAnimationCurvePropertyName.RightUpperLegInOut),
                "Right Upper Leg Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.RightUpperLegTwistInOut),
                "Right Lower Leg Stretch" => (true, HumanoidAnimationCurvePropertyName.RightLowerLegStretch),
                "Right Lower Leg Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.RightLowerLegTwistInOut),
                "Right Foot Up-Down" => (true, HumanoidAnimationCurvePropertyName.RightFootUpDown),
                "Right Foot Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.RightFootTwistInOut),
                "Right Toes Up-Down" => (true, HumanoidAnimationCurvePropertyName.RightToesUpDown),
                "Left Shoulder Down-Up" => (true, HumanoidAnimationCurvePropertyName.LeftShoulderDownUp),
                "Left Shoulder Front-Back" => (true, HumanoidAnimationCurvePropertyName.LeftShoulderFrontBack),
                "Left Arm Down-Up" => (true, HumanoidAnimationCurvePropertyName.LeftArmDownUp),
                "Left Arm Front-Back" => (true, HumanoidAnimationCurvePropertyName.LeftArmFrontBack),
                "Left Arm Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftArmTwistInOut),
                "Left Forearm Stretch" => (true, HumanoidAnimationCurvePropertyName.LeftForearmStretch),
                "Left Forearm Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftForearmTwistInOut),
                "Left Hand Down-Up" => (true, HumanoidAnimationCurvePropertyName.LeftHandDownUp),
                "Left Hand In-Out" => (true, HumanoidAnimationCurvePropertyName.LeftHandInOut),
                "Right Shoulder Down-Up" => (true, HumanoidAnimationCurvePropertyName.RightShoulderDownUp),
                "Right Shoulder Front-Back" => (true, HumanoidAnimationCurvePropertyName.RightShoulderFrontBack),
                "Right Arm Down-Up" => (true, HumanoidAnimationCurvePropertyName.RightArmDownUp),
                "Right Arm Front-Back" => (true, HumanoidAnimationCurvePropertyName.RightArmFrontBack),
                "Right Arm Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.RightArmTwistInOut),
                "Right Forearm Stretch" => (true, HumanoidAnimationCurvePropertyName.RightForearmStretch),
                "Right Forearm Twist In-Out" => (true, HumanoidAnimationCurvePropertyName.RightForearmTwistInOut),
                "Right Hand Down-Up" => (true, HumanoidAnimationCurvePropertyName.RightHandDownUp),
                "Right Hand In-Out" => (true, HumanoidAnimationCurvePropertyName.RightHandInOut),
                "LeftHand.Thumb.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftThumb1Stretched),
                "LeftHand.Thumb.Spread" => (true, HumanoidAnimationCurvePropertyName.LeftThumbSpread),
                "LeftHand.Thumb.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftThumb2Stretched),
                "LeftHand.Thumb.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftThumb3Stretched),
                "LeftHand.Index.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftIndex1Stretched),
                "LeftHand.Index.Spread" => (true, HumanoidAnimationCurvePropertyName.LeftIndexSpread),
                "LeftHand.Index.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftIndex2Stretched),
                "LeftHand.Index.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftIndex3Stretched),
                "LeftHand.Middle.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftMiddle1Stretched),
                "LeftHand.Middle.Spread" => (true, HumanoidAnimationCurvePropertyName.LeftMiddleSpread),
                "LeftHand.Middle.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftMiddle2Stretched),
                "LeftHand.Middle.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftMiddle3Stretched),
                "LeftHand.Ring.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftRing1Stretched),
                "LeftHand.Ring.Spread" => (true, HumanoidAnimationCurvePropertyName.LeftRingSpread),
                "LeftHand.Ring.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftRing2Stretched),
                "LeftHand.Ring.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftRing3Stretched),
                "LeftHand.Little.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftLittle1Stretched),
                "LeftHand.Little.Spread" => (true, HumanoidAnimationCurvePropertyName.LeftLittleSpread),
                "LeftHand.Little.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftLittle2Stretched),
                "LeftHand.Little.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.LeftLittle3Stretched),
                "RightHand.Thumb.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightThumb1Stretched),
                "RightHand.Thumb.Spread" => (true, HumanoidAnimationCurvePropertyName.RightThumbSpread),
                "RightHand.Thumb.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightThumb2Stretched),
                "RightHand.Thumb.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightThumb3Stretched),
                "RightHand.Index.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightIndex1Stretched),
                "RightHand.Index.Spread" => (true, HumanoidAnimationCurvePropertyName.RightIndexSpread),
                "RightHand.Index.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightIndex2Stretched),
                "RightHand.Index.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightIndex3Stretched),
                "RightHand.Middle.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightMiddle1Stretched),
                "RightHand.Middle.Spread" => (true, HumanoidAnimationCurvePropertyName.RightMiddleSpread),
                "RightHand.Middle.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightMiddle2Stretched),
                "RightHand.Middle.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightMiddle3Stretched),
                "RightHand.Ring.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightRing1Stretched),
                "RightHand.Ring.Spread" => (true, HumanoidAnimationCurvePropertyName.RightRingSpread),
                "RightHand.Ring.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightRing2Stretched),
                "RightHand.Ring.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightRing3Stretched),
                "RightHand.Little.1 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightLittle1Stretched),
                "RightHand.Little.Spread" => (true, HumanoidAnimationCurvePropertyName.RightLittleSpread),
                "RightHand.Little.2 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightLittle2Stretched),
                "RightHand.Little.3 Stretched" => (true, HumanoidAnimationCurvePropertyName.RightLittle3Stretched),
                "RootT.x" => (true, HumanoidAnimationCurvePropertyName.CenterTx),
                "RootT.y" => (true, HumanoidAnimationCurvePropertyName.CenterTy),
                "RootT.z" => (true, HumanoidAnimationCurvePropertyName.CenterTz),
                "RootQ.x" => (true, HumanoidAnimationCurvePropertyName.CenterQx),
                "RootQ.y" => (true, HumanoidAnimationCurvePropertyName.CenterQy),
                "RootQ.z" => (true, HumanoidAnimationCurvePropertyName.CenterQz),
                "RootQ.w" => (true, HumanoidAnimationCurvePropertyName.CenterQw),
                _ => (false, default),
            };
    }
}
