//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using UnityEngine;

namespace VGltf.Ext.Vrm0.Unity.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 ToUnity(this VGltf.Ext.Vrm0.Types.Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
    }

    public static class UnityVector3Extensions
    {
        public static VGltf.Ext.Vrm0.Types.Vector3 ToVrm(this Vector3 v)
        {
            return new VGltf.Ext.Vrm0.Types.Vector3()
            {
                x = v.x,
                y = v.y,
                z = v.z
            };
        }
    }
}
