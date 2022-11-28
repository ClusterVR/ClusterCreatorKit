//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using UnityEngine;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity
{
    public sealed class DefaultMaterialExporterBridge : VGltf.Ext.Vrm0.Unity.Bridge.IMaterialExporterBridge
    {
        public Types.Material CreateMaterialProp(IExporterContext context, Material mat)
        {
            var vrmMat = new Types.Material();

            // TODO: if mat.shader is MToon, support that

            vrmMat.Name = mat.name;
            vrmMat.Shader = Types.Material.VRM_USE_GLTFSHADER;

            return vrmMat;
        }
    }
}
