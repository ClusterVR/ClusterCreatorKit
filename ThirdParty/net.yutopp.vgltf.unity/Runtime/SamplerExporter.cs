//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;
using VGltf.Types;
using VGltf.Types.Extensions;
using Texture = UnityEngine.Texture;

namespace VGltf.Unity
{
    public class SamplerExporter
    {
        IExporterContext Context { get; }

        public SamplerExporter(IExporterContext context)
        {
            Context = context;
        }

        public int RawExport(Texture tex)
        {
            var gltfSampler = new Sampler
            {
                MagFilter = AsMagFilterEnum(tex.filterMode),
                MinFilter = AsMinFilterEnum(tex.filterMode),
                WrapS = AsWrapEnum(tex.wrapModeU),
                WrapT = AsWrapEnum(tex.wrapModeV),
            };

            return Context.Gltf.AddSampler(gltfSampler);
        }

        static Sampler.MagFilterEnum AsMagFilterEnum(FilterMode filterMode)
        {
            switch (filterMode)
            {
                case FilterMode.Point:
                    return Sampler.MagFilterEnum.NEAREST;
                case FilterMode.Bilinear:
                case FilterMode.Trilinear:
                    return Sampler.MagFilterEnum.LINEAR;
                default:
                    throw new NotImplementedException();
            }
        }

        static Sampler.MinFilterEnum AsMinFilterEnum(FilterMode filterMode)
        {
            switch (filterMode)
            {
                case FilterMode.Point:
                    return Sampler.MinFilterEnum.NEAREST;
                case FilterMode.Bilinear:
                    return Sampler.MinFilterEnum.LINEAR;
                case FilterMode.Trilinear:
                    return Sampler.MinFilterEnum.LINEAR_MIPMAP_LINEAR;
                default:
                    throw new NotImplementedException();
            }
        }

        static Sampler.WrapEnum AsWrapEnum(TextureWrapMode wrapMode)
        {
            switch (wrapMode)
            {
                case TextureWrapMode.Clamp:
                    return Sampler.WrapEnum.ClampToEdge;
                case TextureWrapMode.Repeat:
                    return Sampler.WrapEnum.Repeat;
                case TextureWrapMode.Mirror:
                case TextureWrapMode.MirrorOnce:
                    return Sampler.WrapEnum.MirroredRepeat;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
