//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity.Bridge
{
    public interface IImporterBridge
    {
        void ImportMeta(IImporterContext context, VGltf.Ext.Vrm0.Types.Meta vrmMeta, GameObject go);
        void ImportFirstPerson(IImporterContext context, VGltf.Ext.Vrm0.Types.FirstPerson vrmFirstPerson, GameObject go);
        void ImportBlendShapeMaster(IImporterContext context, VGltf.Ext.Vrm0.Types.BlendShape vrmBlendShape, GameObject go);
        void ImportSecondaryAnimation(IImporterContext context, VGltf.Ext.Vrm0.Types.SecondaryAnimation vrmSecondaryAnimation, GameObject go);
        Task ReplaceMaterialByMtoon(IImporterContext context, VGltf.Ext.Vrm0.Types.Material matProp, Material mat, CancellationToken ct);
    }
}
