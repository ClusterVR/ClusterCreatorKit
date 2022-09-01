//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Runtime.CompilerServices;
using UnityEngine;

namespace VGltf.Unity
{
    public static class ValueConv
    {
        // IMPORTANT: We assume that Linear workflow is used.
        // Unity(Color[sRGB]) | Shader(sRGB->Linear)
        // Unity(HDR Color[Linear]) | Shader(Linear)

        // glTF world(sRGB) -> as is
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ColorFromSRGB(Vector4 v)
        {
            return v;
        }

        // glTF world(sRGB) -> sRGB
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ColorFromLinear(Vector3 v)
        {
            return ColorFromLinear(new Vector4(v.x, v.y, v.z, 1.0f));
        }

        // glTF world(Linear) -> sRGB
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ColorFromLinear(Vector4 v)
        {
            return ((Color)v).gamma;
        }

        // Unity(Color[sRGB]) -> Linear
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ColorToLinear(Color c)
        {
            return c.linear;
        }

        // Unity(Color[sRGB]) -> Linear
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ColorToLinearRGB(Color c)
        {
            var l = ColorToLinear(c);
            return new Vector3(l.r, l.g, l.b);
        }

        // ---

        // glTF:  roughness : 0 -> 1 (rough)
        // Unity: smoothness: 0 -> 1 (smooth)
        // https://blog.unity.com/ja/technology/ggx-in-unity-5-3
        // roughness = (1 - smoothness) ^ 2
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothnessToRoughness(float glossiness)
        {
            return Mathf.Pow(1.0f - glossiness, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoughnessToSmoothness(float roughness)
        {
            return 1.0f - Mathf.Sqrt(roughness);
        }
    }
}
