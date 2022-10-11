//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;

namespace VGltf.Unity
{
    public interface IImporterContext : IDisposable
    {
        GltfContainer Container { get; }
        ResourcesStore GltfResources { get; }

        ImporterRuntimeResources Resources { get; }
        ITimeSlicer TimeSlicer { get; }
        CoordUtils CoordUtils { get; }
        ImportingSetting ImportingSetting { get; }

        ResourceImporters Importers { get; }
        SamplerApplier SamplerApplier { get; }
    }

    public sealed class ResourceImporters
    {
        public NodeImporter Nodes;
        public MeshImporter Meshes;
        public MaterialImporter Materials;
        public TextureImporter Textures;
        public ImageImporter Images;
    }

    public sealed class ImportingSetting
    {
        public bool TextureUpdateMipmaps = true;
        public bool TextureMakeNoLongerReadable = true;
    }
}
