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
    public abstract class ImageImporterHook
    {
        public abstract Task<Texture2D> Import(IImporterContext importer, int imgIndex, bool isLinear, CancellationToken ct);
    }

    public class ImageImporter : ImporterRefHookable<ImageImporterHook>
    {
        public override IImporterContext Context { get; }

        public ImageImporter(IImporterContext context)
        {
            Context = context;
        }

        // TODO: fix interface to check condition of linier/sRGB at here
        public async Task<Texture2D> ImportAsTex(int imgIndex, bool isLinear, CancellationToken ct)
        {
            foreach (var h in Hooks)
            {
                var customTex = await h.Import(Context, imgIndex, isLinear, ct);
                if (customTex != null)
                {
                    return customTex;
                }
            }

            var gltf = Context.Container.Gltf;
            var gltfImage = gltf.Images[imgIndex];

            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false, isLinear);
            try
            {
                // image binary
                var gltfImgResource = Context.GltfResources.GetOrLoadImageResourceAt(imgIndex);

                var imageBuffer = new byte[gltfImgResource.Data.Count];
                Array.Copy(gltfImgResource.Data.Array, gltfImgResource.Data.Offset, imageBuffer, 0, gltfImgResource.Data.Count);

                // TODO: check encodings...
                // TODO: offload texture decoding...
                tex.LoadImage(imageBuffer, Context.ImportingSetting.TextureMakeNoLongerReadable);

                return tex;
            }
            catch
            {
                Utils.Destroy(tex);
                throw;
            }
        }
    }
}
