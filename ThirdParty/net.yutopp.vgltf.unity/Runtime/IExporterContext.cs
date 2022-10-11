//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace VGltf.Unity
{
    public interface IExporterContext : IDisposable
    {
        Types.Gltf Gltf { get; }
        BufferBuilder BufferBuilder { get; }

        ExporterRuntimeResources Resources { get; }
        CoordUtils CoordUtils { get; }

        ResourceExporters Exporters { get; }
        SamplerExporter SamplerExporter { get; }
    }

    public sealed class ResourceExporters
    {
        public NodeExporter Nodes;
        public MeshExporter Meshes;
        public MaterialExporter Materials;
        public TextureExporter Textures;
        public ImageExporter Images;
    }
}
