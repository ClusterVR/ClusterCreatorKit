//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;
using VGltf.Types;

namespace VGltf.Unity
{
    // https://registry.khronos.org/glTF/specs/2.0/glTF-2.0.html#samplers
    public sealed class SamplerApplier
    {
        IImporterContext Context { get; }

        public SamplerApplier(IImporterContext context)
        {
            Context = context;
        }

        public void ApplySampler(int? samplerIndex, Texture2D tex)
        {
            var gltf = Context.Container.Gltf;

            if (samplerIndex.HasValue)
            {
                var sampler = gltf.Samplers[samplerIndex.Value];
                // UnityEngine.Texture2D has single filterMode, so either one of filters can be used.
                tex.filterMode = sampler.MinFilter.HasValue ? AsFilterMode(sampler.MinFilter.Value) : FilterMode.Bilinear;
                tex.wrapModeU = AsWrapMode(sampler.WrapS);
                tex.wrapModeV = AsWrapMode(sampler.WrapT);
            }
            else
            {
                // When texture.sampler is undefined, a sampler with repeat wrapping (in both directions) and auto filtering MUST be used.
                tex.filterMode = FilterMode.Bilinear;
                tex.wrapMode = TextureWrapMode.Repeat;
            }
        }

        static FilterMode AsFilterMode(Sampler.MinFilterEnum minFilterEnum)
        {
            switch (minFilterEnum)
            {
                case Sampler.MinFilterEnum.NEAREST:
                case Sampler.MinFilterEnum.NEAREST_MIPMAP_LINEAR:
                case Sampler.MinFilterEnum.NEAREST_MIPMAP_NEAREST:
                    return FilterMode.Point;
                case Sampler.MinFilterEnum.LINEAR:
                case Sampler.MinFilterEnum.LINEAR_MIPMAP_NEAREST:
                    return FilterMode.Bilinear;
                case Sampler.MinFilterEnum.LINEAR_MIPMAP_LINEAR:
                    return FilterMode.Trilinear;
                default:
                    throw new NotImplementedException();
            }
        }

        static TextureWrapMode AsWrapMode(Sampler.WrapEnum wrapEnum)
        {
            switch (wrapEnum)
            {
                case Sampler.WrapEnum.ClampToEdge:
                    return TextureWrapMode.Clamp;
                case Sampler.WrapEnum.Repeat:
                    return TextureWrapMode.Repeat;
                case Sampler.WrapEnum.MirroredRepeat:
                    return TextureWrapMode.Mirror;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
