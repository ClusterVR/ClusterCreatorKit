using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.GltfExtensions;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;
using Animation = VGltf.Types.Animation;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public static class HumanoidAnimationExporter
    {
        public static int Export(Exporter exporter, string name, IHumanoidAnimation humanoidAnimation)
        {
            var humanoidAnimationCurves = humanoidAnimation.Curves;
            var context = exporter.Context;

            var channels = new List<Animation.ChannelType>();
            var samplers = new List<Animation.SamplerType>();

            IHumanoidAnimationCurve centerTXCurve = null;
            IHumanoidAnimationCurve centerTYCurve = null;
            IHumanoidAnimationCurve centerTZCurve = null;
            IHumanoidAnimationCurve centerQXCurve = null;
            IHumanoidAnimationCurve centerQYCurve = null;
            IHumanoidAnimationCurve centerQZCurve = null;
            IHumanoidAnimationCurve centerQWCurve = null;

            foreach (var humanoidAnimationCurve in humanoidAnimationCurves)
            {
                switch (humanoidAnimationCurve.PropertyName)
                {
                    case HumanoidAnimationCurvePropertyName.CenterTx:
                        centerTXCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterTy:
                        centerTYCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterTz:
                        centerTZCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQx:
                        centerQXCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQy:
                        centerQYCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQz:
                        centerQZCurve = humanoidAnimationCurve;
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQw:
                        centerQWCurve = humanoidAnimationCurve;
                        break;
                    default:
                        var curve = humanoidAnimationCurve.Curve;

                        var timestamps = new float[curve.keys.Length];
                        var values = new float[curve.keys.Length];

                        foreach (var (keyframe, index) in curve.keys.Select((v, i) => (v, i)))
                        {
                            timestamps[index] = keyframe.time;
                            values[index] = keyframe.value;
                        }

                        var inputAccessorId = ExportScalarAsAccessor(context, timestamps);
                        var outputAccessorId = ExportScalarAsAccessor(context, values);

                        var samplerId = samplers.Count;

                        var sampler = new Animation.SamplerType
                        {
                            Input = inputAccessorId,
                            Output = outputAccessorId,
                            Interpolation = Animation.SamplerType.InterpolationEnum.LINEAR,
                        };
                        samplers.Add(sampler);

                        var target = new Animation.ChannelType.TargetType { Path = "muscle" };
                        target.AddExtension(
                            ClusterHumanoidAnimationTarget.ExtensionName,
                            new ClusterHumanoidAnimationTarget { HumanoidAnimationTargetType = Convert(humanoidAnimationCurve.PropertyName) });
                        var channel = new Animation.ChannelType
                        {
                            Sampler = samplerId,
                            Target = target,
                        };
                        channels.Add(channel);
                        break;
                }
            }

            if (centerTXCurve != null || centerTYCurve != null || centerTZCurve != null)
            {
                var timestamps = new SortedSet<float>();

                void AddKeyFrame(IHumanoidAnimationCurve curve)
                {
                    if (curve != null)
                    {
                        foreach (var time in curve.Curve.keys.Select(k => k.time))
                        {
                            timestamps.Add(time);
                        }
                    }
                }

                AddKeyFrame(centerTXCurve);
                AddKeyFrame(centerTYCurve);
                AddKeyFrame(centerTZCurve);

                var values = new Vector3[timestamps.Count];

                foreach (var (timestamp, index) in timestamps.Select((v, i) => (v, i)))
                {
                    var value = new Vector3(
                        centerTXCurve?.Curve.Evaluate(timestamp) ?? 0f,
                        centerTYCurve?.Curve.Evaluate(timestamp) ?? 0f,
                        centerTZCurve?.Curve.Evaluate(timestamp) ?? 0f);
                    values[index] = value;
                }

                var inputAccessorId = ExportScalarAsAccessor(context, timestamps.ToArray());
                var outputAccessorId = ExportVector3AsAccessor(context, values);

                var samplerId = samplers.Count;

                var sampler = new Animation.SamplerType
                {
                    Input = inputAccessorId,
                    Output = outputAccessorId,
                    Interpolation = Animation.SamplerType.InterpolationEnum.LINEAR,
                };
                samplers.Add(sampler);

                var target = new Animation.ChannelType.TargetType { Path = "translation" };
                target.AddExtension(
                    ClusterHumanoidAnimationTarget.ExtensionName,
                    new ClusterHumanoidAnimationTarget { HumanoidAnimationTargetType = HumanoidAnimationTargetType.CenterPosition });
                var channel = new Animation.ChannelType
                {
                    Sampler = samplerId,
                    Target = target,
                };
                channels.Add(channel);
            }



            if (centerQXCurve != null || centerQYCurve != null || centerQZCurve != null || centerQWCurve != null)
            {
                var timestamps = new SortedSet<float>();

                void AddKeyFrame(IHumanoidAnimationCurve curve)
                {
                    if (curve != null)
                    {
                        foreach (var time in curve.Curve.keys.Select(k => k.time))
                        {
                            timestamps.Add(time);
                        }
                    }
                }

                AddKeyFrame(centerQXCurve);
                AddKeyFrame(centerQYCurve);
                AddKeyFrame(centerQZCurve);
                AddKeyFrame(centerQWCurve);

                var values = new Quaternion[timestamps.Count];

                foreach (var (timestamp, index) in timestamps.Select((v, i) => (v, i)))
                {
                    var value = new Quaternion(
                        centerQXCurve?.Curve.Evaluate(timestamp) ?? 0f,
                        centerQYCurve?.Curve.Evaluate(timestamp) ?? 0f,
                        centerQZCurve?.Curve.Evaluate(timestamp) ?? 0f,
                        centerQWCurve?.Curve.Evaluate(timestamp) ?? 0f);
                    value.Normalize();
                    values[index] = context.CoordUtils.ConvertSpace(value);
                }

                var inputAccessorId = ExportScalarAsAccessor(context, timestamps.ToArray());
                var outputAccessorId = ExportQuaternionAsAccessor(context, values);

                var samplerId = samplers.Count;

                var sampler = new Animation.SamplerType
                {
                    Input = inputAccessorId,
                    Output = outputAccessorId,
                    Interpolation = Animation.SamplerType.InterpolationEnum.LINEAR,
                };
                samplers.Add(sampler);

                var target = new Animation.ChannelType.TargetType { Path = "rotation" };
                target.AddExtension(
                    ClusterHumanoidAnimationTarget.ExtensionName,
                    new ClusterHumanoidAnimationTarget { HumanoidAnimationTargetType = HumanoidAnimationTargetType.CenterRotation });
                var channel = new Animation.ChannelType
                {
                    Sampler = samplerId,
                    Target = target,
                };
                channels.Add(channel);
            }

            var gltfAnim = new Animation
            {
                Name = name,
                Channels = channels,
                Samplers = samplers,
            };
            return context.Gltf.AddAnimation(gltfAnim);
        }

        static HumanoidAnimationTargetType Convert(HumanoidAnimationCurvePropertyName propertyName)
            => propertyName switch
            {
                HumanoidAnimationCurvePropertyName.CenterTx => HumanoidAnimationTargetType.CenterPosition,
                HumanoidAnimationCurvePropertyName.CenterTy => HumanoidAnimationTargetType.CenterPosition,
                HumanoidAnimationCurvePropertyName.CenterTz => HumanoidAnimationTargetType.CenterPosition,
                HumanoidAnimationCurvePropertyName.CenterQx => HumanoidAnimationTargetType.CenterRotation,
                HumanoidAnimationCurvePropertyName.CenterQy => HumanoidAnimationTargetType.CenterRotation,
                HumanoidAnimationCurvePropertyName.CenterQz => HumanoidAnimationTargetType.CenterRotation,
                HumanoidAnimationCurvePropertyName.CenterQw => HumanoidAnimationTargetType.CenterRotation,
                HumanoidAnimationCurvePropertyName.SpineFrontBack => HumanoidAnimationTargetType.SpineFrontBack,
                HumanoidAnimationCurvePropertyName.SpineLeftRight => HumanoidAnimationTargetType.SpineLeftRight,
                HumanoidAnimationCurvePropertyName.SpineTwistLeftRight => HumanoidAnimationTargetType.SpineTwistLeftRight,
                HumanoidAnimationCurvePropertyName.ChestFrontBack => HumanoidAnimationTargetType.ChestFrontBack,
                HumanoidAnimationCurvePropertyName.ChestLeftRight => HumanoidAnimationTargetType.ChestLeftRight,
                HumanoidAnimationCurvePropertyName.ChestTwistLeftRight => HumanoidAnimationTargetType.ChestTwistLeftRight,
                HumanoidAnimationCurvePropertyName.UpperChestFrontBack => HumanoidAnimationTargetType.UpperChestFrontBack,
                HumanoidAnimationCurvePropertyName.UpperChestLeftRight => HumanoidAnimationTargetType.UpperChestLeftRight,
                HumanoidAnimationCurvePropertyName.UpperChestTwistLeftRight => HumanoidAnimationTargetType.UpperChestTwistLeftRight,
                HumanoidAnimationCurvePropertyName.NeckNodDownUp => HumanoidAnimationTargetType.NeckNodDownUp,
                HumanoidAnimationCurvePropertyName.NeckTiltLeftRight => HumanoidAnimationTargetType.NeckTiltLeftRight,
                HumanoidAnimationCurvePropertyName.NeckTurnLeftRight => HumanoidAnimationTargetType.NeckTurnLeftRight,
                HumanoidAnimationCurvePropertyName.HeadNodDownUp => HumanoidAnimationTargetType.HeadNodDownUp,
                HumanoidAnimationCurvePropertyName.HeadTiltLeftRight => HumanoidAnimationTargetType.HeadTiltLeftRight,
                HumanoidAnimationCurvePropertyName.HeadTurnLeftRight => HumanoidAnimationTargetType.HeadTurnLeftRight,
                HumanoidAnimationCurvePropertyName.LeftEyeDownUp => HumanoidAnimationTargetType.LeftEyeDownUp,
                HumanoidAnimationCurvePropertyName.LeftEyeInOut => HumanoidAnimationTargetType.LeftEyeInOut,
                HumanoidAnimationCurvePropertyName.RightEyeDownUp => HumanoidAnimationTargetType.RightEyeDownUp,
                HumanoidAnimationCurvePropertyName.RightEyeInOut => HumanoidAnimationTargetType.RightEyeInOut,
                HumanoidAnimationCurvePropertyName.JawClose => HumanoidAnimationTargetType.JawClose,
                HumanoidAnimationCurvePropertyName.JawLeftRight => HumanoidAnimationTargetType.JawLeftRight,
                HumanoidAnimationCurvePropertyName.LeftUpperLegFrontBack => HumanoidAnimationTargetType.LeftUpperLegFrontBack,
                HumanoidAnimationCurvePropertyName.LeftUpperLegInOut => HumanoidAnimationTargetType.LeftUpperLegInOut,
                HumanoidAnimationCurvePropertyName.LeftUpperLegTwistInOut => HumanoidAnimationTargetType.LeftUpperLegTwistInOut,
                HumanoidAnimationCurvePropertyName.LeftLowerLegStretch => HumanoidAnimationTargetType.LeftLowerLegStretch,
                HumanoidAnimationCurvePropertyName.LeftLowerLegTwistInOut => HumanoidAnimationTargetType.LeftLowerLegTwistInOut,
                HumanoidAnimationCurvePropertyName.LeftFootUpDown => HumanoidAnimationTargetType.LeftFootUpDown,
                HumanoidAnimationCurvePropertyName.LeftFootTwistInOut => HumanoidAnimationTargetType.LeftFootTwistInOut,
                HumanoidAnimationCurvePropertyName.LeftToesUpDown => HumanoidAnimationTargetType.LeftToesUpDown,
                HumanoidAnimationCurvePropertyName.RightUpperLegFrontBack => HumanoidAnimationTargetType.RightUpperLegFrontBack,
                HumanoidAnimationCurvePropertyName.RightUpperLegInOut => HumanoidAnimationTargetType.RightUpperLegInOut,
                HumanoidAnimationCurvePropertyName.RightUpperLegTwistInOut => HumanoidAnimationTargetType.RightUpperLegTwistInOut,
                HumanoidAnimationCurvePropertyName.RightLowerLegStretch => HumanoidAnimationTargetType.RightLowerLegStretch,
                HumanoidAnimationCurvePropertyName.RightLowerLegTwistInOut => HumanoidAnimationTargetType.RightLowerLegTwistInOut,
                HumanoidAnimationCurvePropertyName.RightFootUpDown => HumanoidAnimationTargetType.RightFootUpDown,
                HumanoidAnimationCurvePropertyName.RightFootTwistInOut => HumanoidAnimationTargetType.RightFootTwistInOut,
                HumanoidAnimationCurvePropertyName.RightToesUpDown => HumanoidAnimationTargetType.RightToesUpDown,
                HumanoidAnimationCurvePropertyName.LeftShoulderDownUp => HumanoidAnimationTargetType.LeftShoulderDownUp,
                HumanoidAnimationCurvePropertyName.LeftShoulderFrontBack => HumanoidAnimationTargetType.LeftShoulderFrontBack,
                HumanoidAnimationCurvePropertyName.LeftArmDownUp => HumanoidAnimationTargetType.LeftArmDownUp,
                HumanoidAnimationCurvePropertyName.LeftArmFrontBack => HumanoidAnimationTargetType.LeftArmFrontBack,
                HumanoidAnimationCurvePropertyName.LeftArmTwistInOut => HumanoidAnimationTargetType.LeftArmTwistInOut,
                HumanoidAnimationCurvePropertyName.LeftForearmStretch => HumanoidAnimationTargetType.LeftForearmStretch,
                HumanoidAnimationCurvePropertyName.LeftForearmTwistInOut => HumanoidAnimationTargetType.LeftForearmTwistInOut,
                HumanoidAnimationCurvePropertyName.LeftHandDownUp => HumanoidAnimationTargetType.LeftHandDownUp,
                HumanoidAnimationCurvePropertyName.LeftHandInOut => HumanoidAnimationTargetType.LeftHandInOut,
                HumanoidAnimationCurvePropertyName.RightShoulderDownUp => HumanoidAnimationTargetType.RightShoulderDownUp,
                HumanoidAnimationCurvePropertyName.RightShoulderFrontBack => HumanoidAnimationTargetType.RightShoulderFrontBack,
                HumanoidAnimationCurvePropertyName.RightArmDownUp => HumanoidAnimationTargetType.RightArmDownUp,
                HumanoidAnimationCurvePropertyName.RightArmFrontBack => HumanoidAnimationTargetType.RightArmFrontBack,
                HumanoidAnimationCurvePropertyName.RightArmTwistInOut => HumanoidAnimationTargetType.RightArmTwistInOut,
                HumanoidAnimationCurvePropertyName.RightForearmStretch => HumanoidAnimationTargetType.RightForearmStretch,
                HumanoidAnimationCurvePropertyName.RightForearmTwistInOut => HumanoidAnimationTargetType.RightForearmTwistInOut,
                HumanoidAnimationCurvePropertyName.RightHandDownUp => HumanoidAnimationTargetType.RightHandDownUp,
                HumanoidAnimationCurvePropertyName.RightHandInOut => HumanoidAnimationTargetType.RightHandInOut,
                HumanoidAnimationCurvePropertyName.LeftThumb1Stretched => HumanoidAnimationTargetType.LeftThumb1Stretched,
                HumanoidAnimationCurvePropertyName.LeftThumbSpread => HumanoidAnimationTargetType.LeftThumbSpread,
                HumanoidAnimationCurvePropertyName.LeftThumb2Stretched => HumanoidAnimationTargetType.LeftThumb2Stretched,
                HumanoidAnimationCurvePropertyName.LeftThumb3Stretched => HumanoidAnimationTargetType.LeftThumb3Stretched,
                HumanoidAnimationCurvePropertyName.LeftIndex1Stretched => HumanoidAnimationTargetType.LeftIndex1Stretched,
                HumanoidAnimationCurvePropertyName.LeftIndexSpread => HumanoidAnimationTargetType.LeftIndexSpread,
                HumanoidAnimationCurvePropertyName.LeftIndex2Stretched => HumanoidAnimationTargetType.LeftIndex2Stretched,
                HumanoidAnimationCurvePropertyName.LeftIndex3Stretched => HumanoidAnimationTargetType.LeftIndex3Stretched,
                HumanoidAnimationCurvePropertyName.LeftMiddle1Stretched => HumanoidAnimationTargetType.LeftMiddle1Stretched,
                HumanoidAnimationCurvePropertyName.LeftMiddleSpread => HumanoidAnimationTargetType.LeftMiddleSpread,
                HumanoidAnimationCurvePropertyName.LeftMiddle2Stretched => HumanoidAnimationTargetType.LeftMiddle2Stretched,
                HumanoidAnimationCurvePropertyName.LeftMiddle3Stretched => HumanoidAnimationTargetType.LeftMiddle3Stretched,
                HumanoidAnimationCurvePropertyName.LeftRing1Stretched => HumanoidAnimationTargetType.LeftRing1Stretched,
                HumanoidAnimationCurvePropertyName.LeftRingSpread => HumanoidAnimationTargetType.LeftRingSpread,
                HumanoidAnimationCurvePropertyName.LeftRing2Stretched => HumanoidAnimationTargetType.LeftRing2Stretched,
                HumanoidAnimationCurvePropertyName.LeftRing3Stretched => HumanoidAnimationTargetType.LeftRing3Stretched,
                HumanoidAnimationCurvePropertyName.LeftLittle1Stretched => HumanoidAnimationTargetType.LeftLittle1Stretched,
                HumanoidAnimationCurvePropertyName.LeftLittleSpread => HumanoidAnimationTargetType.LeftLittleSpread,
                HumanoidAnimationCurvePropertyName.LeftLittle2Stretched => HumanoidAnimationTargetType.LeftLittle2Stretched,
                HumanoidAnimationCurvePropertyName.LeftLittle3Stretched => HumanoidAnimationTargetType.LeftLittle3Stretched,
                HumanoidAnimationCurvePropertyName.RightThumb1Stretched => HumanoidAnimationTargetType.RightThumb1Stretched,
                HumanoidAnimationCurvePropertyName.RightThumbSpread => HumanoidAnimationTargetType.RightThumbSpread,
                HumanoidAnimationCurvePropertyName.RightThumb2Stretched => HumanoidAnimationTargetType.RightThumb2Stretched,
                HumanoidAnimationCurvePropertyName.RightThumb3Stretched => HumanoidAnimationTargetType.RightThumb3Stretched,
                HumanoidAnimationCurvePropertyName.RightIndex1Stretched => HumanoidAnimationTargetType.RightIndex1Stretched,
                HumanoidAnimationCurvePropertyName.RightIndexSpread => HumanoidAnimationTargetType.RightIndexSpread,
                HumanoidAnimationCurvePropertyName.RightIndex2Stretched => HumanoidAnimationTargetType.RightIndex2Stretched,
                HumanoidAnimationCurvePropertyName.RightIndex3Stretched => HumanoidAnimationTargetType.RightIndex3Stretched,
                HumanoidAnimationCurvePropertyName.RightMiddle1Stretched => HumanoidAnimationTargetType.RightMiddle1Stretched,
                HumanoidAnimationCurvePropertyName.RightMiddleSpread => HumanoidAnimationTargetType.RightMiddleSpread,
                HumanoidAnimationCurvePropertyName.RightMiddle2Stretched => HumanoidAnimationTargetType.RightMiddle2Stretched,
                HumanoidAnimationCurvePropertyName.RightMiddle3Stretched => HumanoidAnimationTargetType.RightMiddle3Stretched,
                HumanoidAnimationCurvePropertyName.RightRing1Stretched => HumanoidAnimationTargetType.RightRing1Stretched,
                HumanoidAnimationCurvePropertyName.RightRingSpread => HumanoidAnimationTargetType.RightRingSpread,
                HumanoidAnimationCurvePropertyName.RightRing2Stretched => HumanoidAnimationTargetType.RightRing2Stretched,
                HumanoidAnimationCurvePropertyName.RightRing3Stretched => HumanoidAnimationTargetType.RightRing3Stretched,
                HumanoidAnimationCurvePropertyName.RightLittle1Stretched => HumanoidAnimationTargetType.RightLittle1Stretched,
                HumanoidAnimationCurvePropertyName.RightLittleSpread => HumanoidAnimationTargetType.RightLittleSpread,
                HumanoidAnimationCurvePropertyName.RightLittle2Stretched => HumanoidAnimationTargetType.RightLittle2Stretched,
                HumanoidAnimationCurvePropertyName.RightLittle3Stretched => HumanoidAnimationTargetType.RightLittle3Stretched,
                _ => throw new NotImplementedException()
            };

        static int ExportScalarAsAccessor(IExporterContext context, float[] values)
        {
            byte[] buffer = PrimitiveExporter.Marshal(values);
            var viewIndex = context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var accessor = new VGltf.Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = VGltf.Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = values.Length,
                Min = new[] { Mathf.Min(values) },
                Max = new[] { Mathf.Max(values) },
                Type = VGltf.Types.Accessor.TypeEnum.Scalar,
            };
            return context.Gltf.AddAccessor(accessor);
        }

        static int ExportVector3AsAccessor(IExporterContext context, Vector3[] values)
        {
            context.CoordUtils.ConvertSpaces(values);

            byte[] buffer = PrimitiveExporter.Marshal(values);
            var viewIndex = context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var value in values)
            {
                min = Vector3.Min(min, value);
                max = Vector3.Max(max, value);
            }

            var accessor = new VGltf.Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = VGltf.Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = values.Length,
                Min = new[] { min.x, min.y, min.z },
                Max = new[] { max.x, max.y, max.z },
                Type = VGltf.Types.Accessor.TypeEnum.Vec3,
            };
            return context.Gltf.AddAccessor(accessor);
        }

        static int ExportQuaternionAsAccessor(IExporterContext context, Quaternion[] values)
        {
            byte[] buffer = PrimitiveExporter.Marshal(values);
            var viewIndex = context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var accessor = new VGltf.Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = VGltf.Types.Accessor.ComponentTypeEnum.FLOAT,
                Normalized = true,
                Count = values.Length,
                Type = VGltf.Types.Accessor.TypeEnum.Vec4,
            };
            return context.Gltf.AddAccessor(accessor);
        }
    }
}
