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
        public static Vector2 AsVector2(float[] fx, int i = 0)
        {
            return new Vector2(fx[i+0], fx[i+1]);
        }

        public static Vector3 AsVector3(float[] fx, int i = 0)
        {
            return new Vector3(fx[i+0], fx[i+1], fx[i+2]);
        }

        public static Vector4 AsVector4(float[] fx, int i = 0)
        {
            return new Vector4(fx[i+0], fx[i+1], fx[i+2], fx[i+3]);
        }

        public static Quaternion AsQuaternion(float[] fx, int i = 0)
        {
            return new Quaternion(fx[i+0], fx[i+1], fx[i+2], fx[i+3]);
        }

        public static Matrix4x4 AsMatrix4x4(float[] fx, int i = 0)
        {
            var c0 = new Vector4(fx[i+0], fx[i+1], fx[i+2], fx[i+3]);
            var c1 = new Vector4(fx[i+4], fx[i+5], fx[i+6], fx[i+7]);
            var c2 = new Vector4(fx[i+8], fx[i+9], fx[i+10], fx[i+11]);
            var c3 = new Vector4(fx[i+12], fx[i+13], fx[i+14], fx[i+15]);
            return new Matrix4x4(c0, c1, c2, c3);
        }
    }
}
