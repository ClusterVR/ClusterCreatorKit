//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace VGltf.Unity
{
    public class PrimitiveImporter
    {
        public static Vector2 AsVector2(float[] fx)
        {
            return new Vector2(fx[0], fx[1]);
        }

        public static Vector3 AsVector3(float[] fx)
        {
            return new Vector3(fx[0], fx[1], fx[2]);
        }

        public static Vector4 AsVector4(float[] fx)
        {
            return new Vector4(fx[0], fx[1], fx[2], fx[3]);
        }

        public static Quaternion AsQuaternion(float[] fx)
        {
            return new Quaternion(fx[0], fx[1], fx[2], fx[3]);
        }

        public static Matrix4x4 AsMatrix4x4(float[] fx)
        {
            var c0 = AsVector4(fx.Skip(4 * 0).Take(4).ToArray());
            var c1 = AsVector4(fx.Skip(4 * 1).Take(4).ToArray());
            var c2 = AsVector4(fx.Skip(4 * 2).Take(4).ToArray());
            var c3 = AsVector4(fx.Skip(4 * 3).Take(4).ToArray());
            return new Matrix4x4(c0, c1, c2, c3);
        }
    }
}
