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
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    public abstract class NodeExporterHook
    {
        public virtual void PostHook(NodeExporter exporter, GameObject go, Types.Node gltfNode)
        {
        }
    }

    public class NodeExporter : ExporterRefHookable<NodeExporterHook>
    {
        public override IExporterContext Context { get; }

        public NodeExporter(IExporterContext context)
        {
            Context = context;
        }

        public IndexedResource<GameObject> Export(GameObject go)
        {
            return Context.Resources.Nodes.GetOrCall(go, () =>
            {
                return ForceExport(go);
            });
        }

        public IndexedResource<GameObject> ForceExport(GameObject go)
        {
            IndexedResource<Mesh> meshResource = null;
            int? skinIndex = null;
            var mr = go.GetComponent<MeshRenderer>();
            var smr = go.GetComponent<SkinnedMeshRenderer>();
            if (mr != null)
            {
                var meshFilter = mr.gameObject.GetComponent<MeshFilter>();
                var sharedMesh = meshFilter.sharedMesh;

                meshResource = Context.Exporters.Meshes.Export(mr, sharedMesh);
            }
            else if (smr != null)
            {
                var sharedMesh = smr.sharedMesh;
                meshResource = Context.Exporters.Meshes.Export(smr, sharedMesh);

                if (smr.bones.Length > 0)
                {
                    skinIndex = ExportSkin(smr, sharedMesh).Index;
                }
            }

            var t = Context.CoordUtils.ConvertSpace(go.transform.localPosition);
            var r = Context.CoordUtils.ConvertSpace(go.transform.localRotation);
            var s = go.transform.localScale;

            var gltfNode = new Types.Node
            {
                Name = go.name,

                Mesh = meshResource != null ? (int?)meshResource.Index : null,
                Skin = skinIndex,

                Matrix = null,
                Translation = new float[] { t.x, t.y, t.z },
                Rotation = new float[] { r.x, r.y, r.z, r.w },
                Scale = new float[] { s.x, s.y, s.z },
            };

            var nodeIndex = Context.Gltf.AddNode(gltfNode);
            var resource = Context.Resources.Nodes.Add(go, nodeIndex, go.name, go);

            var nodesIndices = new List<int>();
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                var c = go.transform.GetChild(i);
                var nodeResource = Export(c.gameObject);
                nodesIndices.Add(nodeResource.Index);
            }
            if (nodesIndices.Count > 0)
            {
                gltfNode.Children = nodesIndices.ToArray();
            }

            foreach (var h in Hooks)
            {
                h.PostHook(this, go, gltfNode);
            }

            return resource;
        }

        public IndexedResource<Skin> ExportSkin(SkinnedMeshRenderer r, Mesh mesh)
        {
            return Context.Resources.Skins.GetOrCall(mesh, () =>
            {
                return ForceExportSkin(r, mesh);
            });
        }

        IndexedResource<Skin> ForceExportSkin(SkinnedMeshRenderer smr, Mesh mesh)
        {
            var rootBone = smr.rootBone != null ? (int?)Export(smr.rootBone.gameObject).Index : null;

            var boneTransValues = smr.bones.Select(bt => Export(bt.gameObject)).ToArray();
            var boneIndices = boneTransValues.Select(t => t.Index).ToArray();

            int? matricesAccIndex = null;
            if (mesh.bindposes.Length > 0)
            {
                matricesAccIndex = ExportInverseBindMatrices(mesh.bindposes);
            }

            var gltfSkin = new Types.Skin
            {
                InverseBindMatrices = matricesAccIndex,
                Skeleton = rootBone,
                Joints = boneIndices,
            };
            var skinIndex = Context.Gltf.AddSkin(gltfSkin);
            var resource = Context.Resources.Skins.Add(mesh, skinIndex, mesh.name, new Skin());

            return resource;
        }

        // https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#skin

        int ExportInverseBindMatrices(Matrix4x4[] matrices)
        {
            matrices = matrices.Select(Context.CoordUtils.ConvertSpace).ToArray();

            // MAT4! | FLOAT!
            byte[] buffer = PrimitiveExporter.Marshal(matrices);
            var viewIndex = Context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = matrices.Length,
                Type = Types.Accessor.TypeEnum.Mat4,
            };
            return Context.Gltf.AddAccessor(accessor);
        }
    }
}
