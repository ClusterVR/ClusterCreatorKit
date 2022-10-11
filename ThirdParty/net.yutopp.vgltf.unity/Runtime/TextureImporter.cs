//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VGltf.Unity
{
    public abstract class TextureImporterHook
    {
        public virtual void PostHook(IImporterContext context, int tex)
        {
        }
    }

    // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#textures
    public sealed class TextureImporter : ImporterRefHookable<TextureImporterHook>
    {
        public override IImporterContext Context { get; }

        public TextureImporter(IImporterContext context)
        {
            Context = context;
        }

        public async Task<IndexedResource<Texture2D>> Import(int texIndex, bool isLinear, CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;

            return await Context.Resources.Textures.GetOrCallAsync(texIndex, async () =>
            {
                var tex = await RawImport(texIndex, isLinear, ct);

                var res = Context.Resources.Textures.Add(texIndex, texIndex, tex.name, tex);
                return res;
            });
        }

        public async Task<Texture2D> RawImport(int texIndex, bool isLinear, CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;
            var gltfTex = gltf.Textures[texIndex];

            var tex = default(Texture2D);

            // When texture.source is undefined, the image SHOULD be provided by an extension or application-specific means, otherwise the texture object is undefined.
            if (gltfTex.Source != null)
            {
                tex = await Context.Importers.Images.ImportAsTex(gltfTex.Source.Value, isLinear, ct);
                tex.name = gltfTex.Name;
            }
            else
            {
                // Source is undefined, thus add dummy texture (u.b.).
                tex = new Texture2D(2, 2, TextureFormat.RGBA32, false, isLinear);
                tex.name = gltfTex.Name;
            }

            Context.SamplerApplier.ApplySampler(gltfTex.Sampler, tex);

            return tex;
        }
    }
}
