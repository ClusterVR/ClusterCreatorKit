//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VGltf.Unity
{
    public abstract class NodeImporterHook
    {
        public virtual void PostHook(NodeImporter importer, int nodeIndex, GameObject go)
        {
        }
    }

    public sealed class NodeImporter : ImporterRefHookable<NodeImporterHook>
    {
        public override IImporterContext Context { get; }

        public NodeImporter(IImporterContext context)
        {
            Context = context;
        }

        public async Task<IndexedResource<GameObject>> ImportGameObjects(int nodeIndex, GameObject parentGo, CancellationToken ct)
        {
            return await Context.Resources.Nodes.GetOrCallAsync(nodeIndex, async () =>
            {
                return await ForceImportGameObjects(nodeIndex, parentGo, ct);
            });
        }

        public async Task<IndexedResource<GameObject>> ForceImportGameObjects(int nodeIndex, GameObject parentGo, CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;
            var gltfNode = gltf.Nodes[nodeIndex];

            var go = new GameObject();
            go.name = gltfNode.Name;

            var resource = Context.Resources.Nodes.Add(nodeIndex, nodeIndex, go.name, go);

            if (parentGo != null)
            {
                // TODO: go.transform have parent already, it will error
                go.transform.SetParent(parentGo.transform, false);
            }

            var matrix = PrimitiveImporter.AsMatrix4x4(gltfNode.Matrix);
            if (!matrix.isIdentity)
            {
                var trs = Context.CoordUtils.ConvertSpace(matrix);
                go.transform.localPosition = CoordUtils.GetTranslate(trs);
                go.transform.localRotation = CoordUtils.GetRotation(trs);
                go.transform.localScale = CoordUtils.GetScale(trs);
            }
            else
            {
                var t = PrimitiveImporter.AsVector3(gltfNode.Translation);
                var r = PrimitiveImporter.AsQuaternion(gltfNode.Rotation);
                var s = PrimitiveImporter.AsVector3(gltfNode.Scale);
                go.transform.localPosition = Context.CoordUtils.ConvertSpace(t);
                go.transform.localRotation = Context.CoordUtils.ConvertSpace(r);
                go.transform.localScale = s;
            }

            if (gltfNode.Children != null)
            {
                foreach (var childIndex in gltfNode.Children)
                {
                    if (Context.Resources.Nodes.Contains(childIndex))
                    {
                        throw new NotImplementedException("Node duplication"); // TODO:
                    }
                    await ImportGameObjects(childIndex, go, ct);
                    await Context.TimeSlicer.Slice(ct);
                }
            }

            return resource;
        }

        public async Task ImportMeshesAndSkins(int nodeIndex, CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;
            var gltfNode = gltf.Nodes[nodeIndex];

            var go = Context.Resources.Nodes[nodeIndex].Value.gameObject;

            if (gltfNode.Mesh != null)
            {
                await Context.Importers.Meshes.Import(gltfNode.Mesh.Value, go, ct);

                if (gltfNode.Skin != null)
                {
                    ImportSkin(gltfNode.Skin.Value, go);
                }
            }

            if (gltfNode.Children != null)
            {
                foreach (var childIndex in gltfNode.Children)
                {
                    await ImportMeshesAndSkins(childIndex, ct);
                    await Context.TimeSlicer.Slice(ct);
                }
            }

            // TODO: move to elsewhere...
            foreach (var h in Hooks)
            {
                h.PostHook(this, nodeIndex, go);
            }
        }

        void ImportSkin(int skinIndex, GameObject go)
        {
            var gltf = Context.Container.Gltf;
            var gltfSkin = gltf.Skins[skinIndex];

            var smr = go.GetComponent<SkinnedMeshRenderer>();
            if (smr == null)
            {
                throw new NotImplementedException(); // TODO
            }

            if (gltfSkin.Skeleton != null)
            {
                smr.rootBone = Context.Resources.Nodes[gltfSkin.Skeleton.Value].Value.transform;
            }

            smr.bones = gltfSkin.Joints.Select(i => Context.Resources.Nodes[i].Value.transform).ToArray();

            if (gltfSkin.InverseBindMatrices != null)
            {
                var buf = Context.GltfResources.GetOrLoadTypedBufferByAccessorIndex(gltfSkin.InverseBindMatrices.Value);
                var matrices = buf.GetEntity<float, Matrix4x4>((xs, i) => Context.CoordUtils.ConvertSpace(PrimitiveImporter.AsMatrix4x4(xs, i))).AsArray();

                var mesh = smr.sharedMesh;
                mesh.bindposes = matrices;
            }
        }
    }
}
