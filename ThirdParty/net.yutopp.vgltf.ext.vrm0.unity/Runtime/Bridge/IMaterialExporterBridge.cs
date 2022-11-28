//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using UnityEngine;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity.Bridge
{
    public interface IMaterialExporterBridge
    {
        Types.Material CreateMaterialProp(IExporterContext context, Material matRes);
    }
}
