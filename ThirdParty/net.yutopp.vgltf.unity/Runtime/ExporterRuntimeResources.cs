//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VGltf.Unity
{
    // Cache resources by reference
    public sealed class ExporterRuntimeResources : IDisposable
    {
        public IndexedResourceDict<GameObject, GameObject> Nodes = new IndexedResourceDict<GameObject, GameObject>();
        public IndexedResourceDict<Texture, Texture> Textures = new IndexedResourceDict<Texture, Texture>();
        public IndexedResourceDict<Material, Material> Materials = new IndexedResourceDict<Material, Material>();
        public IndexedResourceDict<(Mesh, Material[]), Mesh> Meshes = new IndexedResourceDict<(Mesh, Material[]), Mesh>(new MeshEqualityComparer());
        public IndexedResourceDict<Mesh, Skin> Skins = new IndexedResourceDict<Mesh, Skin>();

        public void Dispose()
        {
            // DO NOT Dispose any resources because these containers have no ownerships.
        }

        sealed class MeshEqualityComparer : IEqualityComparer<(Mesh, Material[])>
        {
            public bool Equals((Mesh, Material[]) x, (Mesh, Material[]) y)
            {
                return x.Item1.Equals(y.Item1) && x.Item2.SequenceEqual(y.Item2);
            }

            public int GetHashCode((Mesh, Material[]) obj)
            {
                var hashCode = obj.Item1.GetHashCode();
                foreach (var m in obj.Item2)
                {
                    hashCode = unchecked(hashCode * 31 + m.GetHashCode());
                }
                return hashCode;
            }
        }
    }
}
