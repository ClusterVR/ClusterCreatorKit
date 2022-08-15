//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    public static class ImageUtils
    {
        public static void BlitTex(Texture src, Texture2D dst, bool isLinear, Material mat = null)
        {
            var previous = RenderTexture.active;

            var renderTex = RenderTexture.GetTemporary(
                src.width,
                src.height,
                0,
                RenderTextureFormat.Default,
                isLinear ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB);
            try
            {
                if (mat != null)
                {
                    Graphics.Blit(src, renderTex, mat);
                }
                else
                {
                    Graphics.Blit(src, renderTex);
                }

                dst.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0); // read from render texture
            }
            finally
            {
                RenderTexture.active = previous;

                RenderTexture.ReleaseTemporary(renderTex);
            }
        }
    }
}
