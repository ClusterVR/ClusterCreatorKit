//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using VGltf.Types.Extensions;

namespace VGltf.Unity.Ext.Helper
{
    public sealed class HumanoidAnimationExporter : AnimationClipExporterHook
    {
#if UNITY_EDITOR
        public override IndexedResource<AnimationClip> Export(IExporterContext context, AnimationClip clip)
        {
            // If clip is not HumanMotion, Do nothing.
            if (!clip.isHumanMotion)
            {
                return null;
            }

            var curveBindings = AnimationUtility.GetCurveBindings(clip);

            var channels = new List<VGltf.Types.Animation.ChannelType>();
            var samplers = new List<VGltf.Types.Animation.SamplerType>();

            foreach (var binding in curveBindings)
            {
                var curve = AnimationUtility.GetEditorCurve(clip, binding);

                var timestamps = new float[curve.keys.Length];
                var values = new float[curve.keys.Length];

                var inTangents = new float[curve.keys.Length];
                var inWeights = new float[curve.keys.Length];
                var outTangents = new float[curve.keys.Length];
                var outWeights = new float[curve.keys.Length];

                foreach (var (keyframe, index) in curve.keys.Select((v, i) => (v, i)))
                {
                    timestamps[index] = keyframe.time;
                    values[index] = keyframe.value;

                    inTangents[index] = keyframe.inTangent;
                    inWeights[index] = keyframe.inWeight;
                    outTangents[index] = keyframe.outTangent;
                    outWeights[index] = keyframe.outWeight;
                    // keyframe.weightedMode
                }

                var inputAccessorId = ExportTimestamp(context, timestamps);
                var outputAccessorId = ExportHumanoidFloatScalarValue(context, values);

                var inTangentAccessorId = ExportHumanoidFloatScalarValue(context, inTangents);
                var inWeightAccessorId = ExportHumanoidFloatScalarValue(context, inWeights);
                var outTangentAccessorId = ExportHumanoidFloatScalarValue(context, outTangents);
                var outWeightAccessorId = ExportHumanoidFloatScalarValue(context, outWeights);

                // sampler
                var samplerId = samplers.Count;

                var sampler = new VGltf.Types.Animation.SamplerType
                {
                    Input = inputAccessorId,
                    // Interpolation will not be used when "VGLTF_unity_humanoid_animation_sampler" specified
                    Output = outputAccessorId,
                };
                sampler.AddExtra(Types.HumanoidAnimationType.SamplerType.ExtraName, new Types.HumanoidAnimationType.SamplerType
                {
                    InTangent = inTangentAccessorId,
                    InWeight = inWeightAccessorId,
                    OutTangent = outTangentAccessorId,
                    OutWeight = outWeightAccessorId,
                });
                samplers.Add(sampler);

                // node (dummy)
                var dummyNode = context.Gltf.AddNode(new VGltf.Types.Node
                {
                    Matrix = null,
                    Translation = null,
                    Rotation = null,
                    Scale = null,
                });

                // channel
                var channel = new VGltf.Types.Animation.ChannelType
                {
                    Sampler = samplerId,
                    // NOTE: Target will not be used when "VGLTF_unity_humanoid_animation_channel" specified
                    Target = new VGltf.Types.Animation.ChannelType.TargetType
                    {
                        Node = dummyNode, // Specify the dummy node to suppress validation errors...
                        Path = "", // Never used for Unity humanoid
                    },
                };
                channel.AddExtra(Types.HumanoidAnimationType.ChannelType.ExtraName, new Types.HumanoidAnimationType.ChannelType
                {
                    RelativePath = binding.path,
                    PropertyName = binding.propertyName
                });
                channels.Add(channel);
            }

            // TODO: Append standard animations if needed.

            var gltfAnim = new VGltf.Types.Animation
            {
                Name = clip.name,
                Channels = channels,
                Samplers = samplers,
            };
            gltfAnim.AddExtra(Types.HumanoidAnimationType.ExtraName, new Types.HumanoidAnimationType{});
            var animIndex = context.Gltf.AddAnimation(gltfAnim);
            
            var res = context.Resources.Animations.Add(clip, animIndex, clip.name, clip);

            return res;
        }
#else
        public override IndexedResource<AnimationClip> Export(IExporterContext context, AnimationClip clip)
        {
            throw new NotImplementedException("Could not export AnimationClips without Editor");
        }
#endif

        public static int ExportTimestamp(IExporterContext context, float[] timestamps)
        {
            // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#_animation_sampler_input

            // Scalar | FLOAT

            byte[] buffer = PrimitiveExporter.Marshal(timestamps);
            var viewIndex = context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var viewComponentType = VGltf.Types.Accessor.ComponentTypeEnum.FLOAT;

            var accessor = new VGltf.Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = viewComponentType,
                Count = timestamps.Length,
                Min = new float[]{ Mathf.Min(timestamps) },
                Max = new float[]{ Mathf.Max(timestamps) },
                Type = VGltf.Types.Accessor.TypeEnum.Scalar,
            };
            return context.Gltf.AddAccessor(accessor);
        }
        
        public static int ExportHumanoidFloatScalarValue(IExporterContext context, float[] values)
        {
            // Scalar | FLOAT

            byte[] buffer = PrimitiveExporter.Marshal(values);
            var viewIndex = context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var viewComponentType = VGltf.Types.Accessor.ComponentTypeEnum.FLOAT;

            var accessor = new VGltf.Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = viewComponentType,
                Count = values.Length,
                Min = new float[]{ Mathf.Min(values) },
                Max = new float[]{ Mathf.Max(values) },
                Type = VGltf.Types.Accessor.TypeEnum.Scalar,
            };
            return context.Gltf.AddAccessor(accessor);
        }
    }
}
