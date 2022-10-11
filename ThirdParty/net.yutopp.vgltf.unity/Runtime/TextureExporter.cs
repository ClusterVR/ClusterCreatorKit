//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    public class TextureExporter : ExporterRefHookable<NodeExporterHook>
    {
        public override IExporterContext Context { get; }

        public TextureExporter(IExporterContext context)
        {
            Context = context;
        }

        public IndexedResource<Texture> Export(Texture tex, bool isLinear = false)
        {
            return Context.Resources.Textures.GetOrCall(tex, () => {
                var texIndex = RawExport(tex, isLinear);

                var res = Context.Resources.Textures.Add(tex, texIndex, tex.name, tex);
                return res;
            });
        }

        public int RawExport(
            Texture tex,
            bool isLinear = false,
            Material mat = null
            )
        {
            var imageIndex = Context.Exporters.Images.RawExport(tex, isLinear, mat);
            var samplerIndex = Context.SamplerExporter.RawExport(tex);

            var gltfImage = new Types.Texture
            {
                Name = tex.name,

                Sampler = samplerIndex,
                Source = imageIndex,
            };
            var texIndex = Context.Gltf.AddTexture(gltfImage);

            return texIndex;
        }
    }
}
