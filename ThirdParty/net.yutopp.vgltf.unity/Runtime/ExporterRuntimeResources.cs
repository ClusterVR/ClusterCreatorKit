//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;

namespace VGltf.Unity
{
    // Cache resources by reference
    public sealed class ExporterRuntimeResources : IDisposable
    {
        public IndexedResourceDict<GameObject, GameObject> Nodes = new IndexedResourceDict<GameObject, GameObject>();
        public IndexedResourceDict<Texture, Texture> Textures = new IndexedResourceDict<Texture, Texture>();
        public IndexedResourceDict<Material, Material> Materials = new IndexedResourceDict<Material, Material>();
        public IndexedResourceDict<Mesh, Mesh> Meshes = new IndexedResourceDict<Mesh, Mesh>();
        public IndexedResourceDict<Mesh, Skin> Skins = new IndexedResourceDict<Mesh, Skin>();

        public void Dispose()
        {
            // DO NOT Dispose any resources because these containers have no ownerships.
        }
    }
}
