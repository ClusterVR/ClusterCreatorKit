//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VGltf.Unity.Ext.Helper
{
    public sealed class HumanoidAnimationImporter : AnimationClipImporterHook
    {
        public override Task<IndexedResource<AnimationClip>> Import(IImporterContext context, int animIndex, CancellationToken ct)
        {
            var gltf = context.Container.Gltf;
            var gltfAnim = gltf.Animations[animIndex];

            var animExtra = default(Types.HumanoidAnimationType);
            if (!gltfAnim.TryGetExtra(Types.HumanoidAnimationType.ExtraName, context.Container.JsonSchemas, out animExtra))
            {
                // This importer aims to importing only HumanoidAnimation extras, thus skip process.
                return null;
            }

            var animClip = new AnimationClip();
            animClip.name = gltfAnim.Name;

            var resource = context.Resources.Animations.Add(animIndex, animIndex, animClip.name, new Utils.DestroyOnDispose<AnimationClip>(animClip));
            // Ownership is already held by "context.Resources". This is just a reference.
            var resourceTyped = new IndexedResource<AnimationClip>(resource.Index, animClip);

            foreach (var channel in gltfAnim.Channels)
            {
                var channelExtra = default(Types.HumanoidAnimationType.ChannelType);
                if (!channel.TryGetExtra(Types.HumanoidAnimationType.ChannelType.ExtraName, context.Container.JsonSchemas, out channelExtra))
                {
                    throw new NotImplementedException($"Extras {Types.HumanoidAnimationType.ChannelType.ExtraName} is not given");
                }

                var sampler = gltfAnim.Samplers[channel.Sampler];

                var samplerExtra = default(Types.HumanoidAnimationType.SamplerType);
                if (!sampler.TryGetExtra(Types.HumanoidAnimationType.SamplerType.ExtraName, context.Container.JsonSchemas, out samplerExtra))
                {
                    throw new NotImplementedException($"Extras {Types.HumanoidAnimationType.SamplerType.ExtraName} is not given");
                }

                var timestamps = ImportTimestamp(context, sampler.Input);
                var values = ImportHumanoidFloatScalarValue(context, sampler.Output);
                Debug.Assert(timestamps.Length == values.Length);

                var inTangents = ImportHumanoidFloatScalarValue(context, samplerExtra.InTangent);
                var inWeights = ImportHumanoidFloatScalarValue(context, samplerExtra.InWeight);
                var outTangents = ImportHumanoidFloatScalarValue(context, samplerExtra.OutTangent);
                var outWeights = ImportHumanoidFloatScalarValue(context, samplerExtra.OutWeight);

                // TODO: support interpolation
                var keyframes = new Keyframe[timestamps.Length];
                for (var i = 0; i < timestamps.Length; ++i)
                {
                    var keyframe = new Keyframe(
                        time: timestamps[i],
                        value: values[i],
                        inTangent: inTangents[i],
                        outTangent: outTangents[i],
                        inWeight: inWeights[i],
                        outWeight: outWeights[i]);
                    keyframes[i] = keyframe;
                }

                var curve = new AnimationCurve(keyframes);
                animClip.SetCurve(channelExtra.RelativePath, typeof(Animator), channelExtra.PropertyName, curve);
            }

            return Task.FromResult(resourceTyped);
        }

        public static float[] ImportTimestamp(IImporterContext context, int index)
        {
            // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#_animation_sampler_input

            // SCALAR | FLOAT
            var buf = context.GltfResources.GetOrLoadTypedBufferByAccessorIndex(index);
            var acc = buf.Accessor;
            if (acc.Type == VGltf.Types.Accessor.TypeEnum.Scalar)
            {
                if (acc.ComponentType == VGltf.Types.Accessor.ComponentTypeEnum.FLOAT)
                {
                    return buf.GetEntity<float, float>((xs, i) => xs[i]).AsArray();
                }
            }

            throw new NotImplementedException(); // TODO
        }

        public static float[] ImportHumanoidFloatScalarValue(IImporterContext context, int index)
        {
            // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#_animation_sampler_input

            // SCALAR | FLOAT
            var buf = context.GltfResources.GetOrLoadTypedBufferByAccessorIndex(index);
            var acc = buf.Accessor;
            if (acc.Type == VGltf.Types.Accessor.TypeEnum.Scalar)
            {
                if (acc.ComponentType == VGltf.Types.Accessor.ComponentTypeEnum.FLOAT)
                {
                    return buf.GetEntity<float, float>((xs, i) => xs[i]).AsArray();
                }
            }

            throw new NotImplementedException(); // TODO
        }
    }
}
