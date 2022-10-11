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
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    using TMPA = Types.Mesh.PrimitiveType.AttributeName;

    public class MeshExporter : ExporterRefHookable<uint>
    {
        public override IExporterContext Context { get; }

        public MeshExporter(IExporterContext context)
        {
            Context = context;
        }

        public IndexedResource<Mesh> Export(Renderer r, Mesh mesh)
        {
            var materials = r.sharedMaterials;
            return Context.Resources.Meshes.GetOrCall((mesh, materials), () =>
            {
                return ForceExport(mesh, materials);
            });
        }

        class Target
        {
            public string Name;

            public int Position;
            public int? Normal;
            public int? Tangent;

            public float Weight;
        }

        public IndexedResource<Mesh> ForceExport(Mesh mesh, Material[] materials)
        {
            var materialIndices = new List<int>();
            foreach (var m in materials)
            {
                var materialResource = Context.Exporters.Materials.Export(m);
                materialIndices.Add(materialResource.Index);
            }

            // TODO: share things other than materials if only materials differ.
            // (cache mesh and AccIndices, then use the AccIndices when export same mesh)

            // Convert to right-handed coordinate system
            var positionAccIndex = ExportPositions(mesh.vertices);

            int? normalAccIndex = null;
            if (mesh.normals.Length > 0)
            {
                normalAccIndex = ExportNormals(Normalize(mesh.normals));
            }

            int? tangentAccIndex = null;
            //if (mesh.tangents.Length > 0)
            //{
            //    tangentAccIndex = ExportTangents(mesh.tangents);
            //}

            int? texcoord0AccIndex = null;
            if (mesh.uv.Length > 0)
            {
                texcoord0AccIndex = ExportUV(mesh.uv);
            }

            int? texcoord1AccIndex = null;
            if (mesh.uv2.Length > 0)
            {
                texcoord1AccIndex = ExportUV(mesh.uv2);
            }

            int? color0AccIndex = null;
            if (mesh.colors.Length > 0)
            {
                color0AccIndex = ExportColors(mesh.colors);
            }

            int? joints0AccIndex = null;
            int? weights0AccIndex = null;
            if (mesh.boneWeights.Length > 0)
            {
                var joints = mesh.boneWeights
                    .Select(w => new Vec4<int>(
                        w.boneIndex0,
                        w.boneIndex1,
                        w.boneIndex2,
                        w.boneIndex3)
                    ).ToArray();
                var weights = mesh.boneWeights
                    .Select(w => new Vector4(
                        w.weight0,
                        w.weight1,
                        w.weight2,
                        w.weight3)
                    ).ToArray();

                joints0AccIndex = ExportJoints(joints);
                weights0AccIndex = ExportWeights(weights);
            }

            #region blendSpace
            List<Target> targets = null;
            if (mesh.blendShapeCount > 0)
            {
                targets = new List<Target>();

                for (int i = 0; i < mesh.blendShapeCount; ++i)
                {
                    var name = mesh.GetBlendShapeName(i);

                    var deltaVertices = new Vector3[mesh.vertexCount];
                    var deltaNormals = new Vector3[mesh.vertexCount];
                    var deltaTangents = new Vector3[mesh.vertexCount];

                    var frameCount = mesh.GetBlendShapeFrameCount(i);
                    mesh.GetBlendShapeFrameVertices(
                        i,
                        frameCount - 1 /* get last frame */,
                        deltaVertices,
                        deltaNormals,
                        deltaTangents);

                    // TODO: Export as sparse accessors
                    var mPositionAccIndex = ExportPositions(deltaVertices);
                    var mNormalsAccIndex = ExportNormals(deltaNormals);
                    //var mTangentsAccIndex = ExportPositions(deltaTangents);
                    var mTangentsAccIndex = (int?)null;

                    var weight = mesh.GetBlendShapeFrameWeight(
                        i,
                        frameCount - 1 /* get last frame */);

                    targets.Add(new Target
                    {
                        Name = name,

                        Position = mPositionAccIndex,
                        Normal = mNormalsAccIndex,
                        Tangent = mTangentsAccIndex,

                        Weight = weight,
                    });
                }
            }

            List<Dictionary<string, int>> primTargets = null;
            if (targets != null)
            {
                primTargets = new List<Dictionary<string, int>>();

                foreach (var t in targets)
                {
                    var primTarget = new Dictionary<string, int>();
                    primTarget[TMPA.POSITION] = t.Position;
                    if (t.Normal != null)
                    {
                        primTarget[TMPA.NORMAL] = t.Normal.Value;
                    }
                    if (t.Tangent != null)
                    {
                        primTarget[TMPA.TANGENT] = t.Tangent.Value;
                    }

                    primTargets.Add(primTarget);
                }
            }
            #endregion

            var primitives = new List<Types.Mesh.PrimitiveType>();
            for (var i = 0; i < mesh.subMeshCount; ++i)
            {
                var indices = mesh.GetIndices(i); // Owner ship will be taken
                var positionindicesAccIndex = ExportIndices(indices);

                var attrs = new Dictionary<string, int>();
                attrs[TMPA.POSITION] = positionAccIndex;
                if (normalAccIndex != null)
                {
                    attrs[TMPA.NORMAL] = normalAccIndex.Value;
                }
                if (tangentAccIndex != null)
                {
                    attrs[TMPA.TANGENT] = tangentAccIndex.Value;
                }
                if (texcoord0AccIndex != null)
                {
                    attrs[TMPA.TEXCOORD_0] = texcoord0AccIndex.Value;
                }
                if (texcoord1AccIndex != null)
                {
                    attrs[TMPA.TEXCOORD_1] = texcoord1AccIndex.Value;
                }
                if (color0AccIndex != null)
                {
                    attrs[TMPA.COLOR_0] = color0AccIndex.Value;
                }
                if (joints0AccIndex != null)
                {
                    attrs[TMPA.JOINTS_0] = joints0AccIndex.Value;
                }
                if (weights0AccIndex != null)
                {
                    attrs[TMPA.WEIGHTS_0] = weights0AccIndex.Value;
                }

                var primitive = new Types.Mesh.PrimitiveType
                {
                    Attributes = attrs,
                    Indices = positionindicesAccIndex,
                    Material = materialIndices[i < materialIndices.Count ? i : materialIndices.Count - 1],
                    Targets = primTargets,
                };
                primitives.Add(primitive);
            }

            var gltfMesh = new Types.Mesh
            {
                Name = mesh.name,

                Primitives = primitives,
                // Weights = Should support default morph target weights?
            };
            if (targets != null)
            {
                var targetNames = targets.Select(t => t.Name).ToArray();
                // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#morph-targets
                gltfMesh.AddExtra("targetNames", targetNames);
            }

            var meshIndex = Context.Gltf.AddMesh(gltfMesh);
            var resource = Context.Resources.Meshes.Add((mesh, materials), meshIndex, mesh.name, mesh);

            return resource;
        }

        // https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#meshes

        int ExportIndices(int[] indices)
        {
            // https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#primitiveindices

            Context.CoordUtils.FlipIndices(indices);

            // Scalar | UNSIGNED_BYTE
            //        | UNSIGNED_SHORT
            //        | UNSIGNED_INT! (TODO: optimize kind...)

            byte[] buffer = PrimitiveExporter.Marshal(indices);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ELEMENT_ARRAY_BUFFER);

            var viewComponentType = Types.Accessor.ComponentTypeEnum.UNSIGNED_INT;

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = viewComponentType,
                Count = indices.Length,
                Type = Types.Accessor.TypeEnum.Scalar,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportSparseIndicesBuffer(ref int[] indices, out Types.Accessor.SparseType.IndicesType.ComponentTypeEnum componentType)
        {
            // Scalar | UNSIGNED_BYTE
            //        | UNSIGNED_SHORT
            //        | UNSIGNED_INT! (TODO: optimize kind...)
            byte[] buffer = PrimitiveExporter.Marshal(indices);
            var viewIndex = Context.BufferBuilder.AddView(new ArraySegment<byte>(buffer));

            componentType = Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_INT;

            return viewIndex;
        }

        int ExportPositions(Vector3[] vec3, int[] indices = null)
        {
            Types.Accessor.ComponentTypeEnum viewComponentType;
            var viewIndex = ExportPositionsBuffer(ref vec3, out viewComponentType);

            var sparseIndexType = default(Types.Accessor.SparseType.IndicesType.ComponentTypeEnum);
            int? sparseViewIndex = null;
            if (indices != null)
            {
                Debug.Assert(indices.Length == vec3.Length);
                sparseViewIndex = ExportSparseIndicesBuffer(ref indices, out sparseIndexType);
            }

            // position MUST have min/max
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var v in vec3)
            {
                min = new Vector3(Mathf.Min(v.x, min.x), Mathf.Min(v.y, min.y), Mathf.Min(v.z, min.z));
                max = new Vector3(Mathf.Max(v.x, max.x), Mathf.Max(v.y, max.y), Mathf.Max(v.z, max.z));
            }

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = viewComponentType,
                Count = vec3.Length,
                Type = Types.Accessor.TypeEnum.Vec3,
                Min = new float[] { min.x, min.y, min.z },
                Max = new float[] { max.x, max.y, max.z },
            };
            if (sparseViewIndex != null)
            {
                accessor.Sparse = new Types.Accessor.SparseType
                {
                    Count = vec3.Length,
                    Indices = new Types.Accessor.SparseType.IndicesType
                    {
                        BufferView = sparseViewIndex.Value,
                        ByteOffset = 0,
                        ComponentType = sparseIndexType,
                    },
                    Values = new Types.Accessor.SparseType.ValuesType
                    {
                        BufferView = accessor.BufferView.Value,
                        ByteOffset = accessor.ByteOffset,
                    },
                };
                accessor.BufferView = null;
            }
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportPositionsBuffer(ref Vector3[] vec3, out Types.Accessor.ComponentTypeEnum componentType)
        {
            Context.CoordUtils.ConvertSpaces(vec3);

            // VEC3! | FLOAT!
            byte[] buffer = PrimitiveExporter.Marshal(vec3);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            componentType = Types.Accessor.ComponentTypeEnum.FLOAT;

            return viewIndex;
        }

        int ExportNormals(Vector3[] vec3)
        {
            Context.CoordUtils.ConvertSpaces(vec3);

            // VEC3! | FLOAT!
            byte[] buffer = PrimitiveExporter.Marshal(vec3);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = vec3.Length,
                Type = Types.Accessor.TypeEnum.Vec3,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportTangents(Vector4[] vec4)
        {
            Context.CoordUtils.ConvertSpaces(vec4);

            // VEC4! | FLOAT!
            byte[] buffer = PrimitiveExporter.Marshal(vec4);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = vec4.Length,
                Type = Types.Accessor.TypeEnum.Vec4,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportUV(Vector2[] uv)
        {
            CoordUtils.ConvertUVs(uv);

            // VEC2! | FLOAT!
            //       | UNSIGNED_BYTE  (normalized) 
            //       | UNSIGNED_SHORT (normalized)
            byte[] buffer = PrimitiveExporter.Marshal(uv);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = uv.Length,
                Type = Types.Accessor.TypeEnum.Vec2,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportColors(Color[] colors)
        {
            // VEC3  | FLOAT!
            // VEC4! | UNSIGNED_BYTE  (normalized)
            //       | UNSIGNED_SHORT (normalized)
            byte[] buffer = PrimitiveExporter.Marshal(colors);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = colors.Length,
                Type = Types.Accessor.TypeEnum.Vec4,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportJoints(Vec4<int>[] joints)
        {
            // VEC4! | UNSIGNED_BYTE
            //       | UNSIGNED_SHORT!
            byte[] buffer = PrimitiveExporter.Marshal(
                joints
                .Select(v => new Vec4<ushort>((ushort)v.x, (ushort)v.y, (ushort)v.z, (ushort)v.w))
                .ToArray()
                );
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.UNSIGNED_SHORT,
                Count = joints.Length,
                Type = Types.Accessor.TypeEnum.Vec4,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        int ExportWeights(Vector4[] weights)
        {
            // VEC4! | FLOAT!
            //       | UNSIGNED_BYTE  (normalized)
            //       | UNSIGNED_SHORT (normalized)
            byte[] buffer = PrimitiveExporter.Marshal(weights);
            var viewIndex = Context.BufferBuilder.AddView(
                new ArraySegment<byte>(buffer),
                null,
                Types.BufferView.TargetEnum.ARRAY_BUFFER);

            var accessor = new Types.Accessor
            {
                BufferView = viewIndex,
                ByteOffset = 0,
                ComponentType = Types.Accessor.ComponentTypeEnum.FLOAT,
                Count = weights.Length,
                Type = Types.Accessor.TypeEnum.Vec4,
            };
            return Context.Gltf.AddAccessor(accessor);
        }

        static Vector3[] Normalize(Vector3[] vec3)
        {
            return vec3.Select(v => v.normalized).ToArray();
        }
    }
}
