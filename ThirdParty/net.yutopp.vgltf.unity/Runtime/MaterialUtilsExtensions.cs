//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using UnityEngine;

namespace VGltf.Unity
{
    public static class MaterialUtilsExtensions
    {
        public static bool TryGetFloatOrDefault(this Material mat, string name, float defaultValue, out float value)
        {
            if (mat.HasProperty(name))
            {
                value = mat.GetFloat(name);
                return true;
            }

            value = defaultValue;
            return false;
        }

        public static bool TryGetColorOrDefault(this Material mat, string name, Color defaultValue, out Color value)
        {
            if (mat.HasProperty(name))
            {
                value = mat.GetColor(name);
                return true;
            }

            value = defaultValue;
            return false;
        }
    }
}
