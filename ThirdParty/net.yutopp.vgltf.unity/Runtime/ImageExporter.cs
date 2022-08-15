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
    public sealed class ImageExporter : ExporterRefHookable<uint>
    {
        public override IExporterContext Context { get; }

        public ImageExporter(IExporterContext context)
        {
            Context = context;
        }

        public int RawExport(
            Texture tex,
            bool isLinear = false,
            Material mat = null
            )
        {
            byte[] pngBytes;

            var readableTex = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, true, isLinear);
            try
            {
                ImageUtils.BlitTex(tex, readableTex, isLinear, mat);
                readableTex.Apply();

                pngBytes = readableTex.EncodeToPNG();
            }
            finally
            {
                Utils.Destroy(readableTex);
            }

            var viewIndex = Context.BufferBuilder.AddView(new ArraySegment<byte>(pngBytes));

            return Context.Gltf.AddImage(new Types.Image
            {
                Name = tex.name,

                MimeType = Types.Image.MimeTypeImagePng,
                BufferView = viewIndex,
            });
        }
    }
}
