//
// Copyright (c) 2023 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VGltf.Unity;

namespace VGltf.Ext.Vrm0.Unity.Filter
{
    public sealed class BlendshapeUnifier : IDisposable
    {
        public GameObject Go { get; private set; }
        readonly Dictionary<SkinnedMeshRenderer, (Mesh, Mesh)> bakedMeshes = new Dictionary<SkinnedMeshRenderer, (Mesh, Mesh)>();

        void IDisposable.Dispose()
        {
            if (Go != null)
            {
                Utils.Destroy(Go);
                Go = null;
            }

            // Destroy baked meshes
            foreach (var (_, mesh) in bakedMeshes.Values)
            {
                Utils.Destroy(mesh);
            }
            bakedMeshes.Clear();
        }

        public void Unify(GameObject go)
        {
            var nGo = UnityEngine.Object.Instantiate(go) as GameObject;
            nGo.name = go.name;
            try
            {
                Normalized(nGo);
            }
            catch (Exception)
            {
                Utils.Destroy(nGo);
                throw;
            }

            Go = nGo;
        }

        void Normalized(GameObject nGo)
        {
            var bsp = nGo.GetComponent<VRM0BlendShapeProxy>();
            if (bsp == null)
            {
                return;
            }

            UnifyWeightsOfBlendShapes(nGo, bsp);
        }

        void UnifyWeightsOfBlendShapes(GameObject go, VRM0BlendShapeProxy bsp)
        {
            foreach (var group in bsp.Groups)
            {
                var shapes = group.MeshShapes;
                if (shapes.Count != 1)
                {
                    continue;
                }

                var shape = shapes[0];
                if (shape.Weights.Count > 1)
                {
                    var (originalMesh, nMesh) = AssumeReplaced(shape.SkinnedMeshRenderer);

                    // Apply all BlendShapes contained in shape
                    foreach (var weight in shape.Weights)
                    {
                        var shapeIndex = nMesh.GetBlendShapeIndex(weight.ShapeKeyName);
                        shape.SkinnedMeshRenderer.SetBlendShapeWeight(shapeIndex, weight.WeightValue); // [0, 100]
                    }

                    var generatedShapeName = CreateShapeName(group.Name);

                    var bakedMesh = new Mesh();
                    try {
                        shape.SkinnedMeshRenderer.BakeMesh(bakedMesh);
                        AddNewBlendShape(nMesh, generatedShapeName, originalMesh, bakedMesh);
                    } finally {
                        Utils.Destroy(bakedMesh);
                    }

                    // Finally, set all blendshapes of nMesh to 0
                    for (int i = 0; i < nMesh.blendShapeCount; ++i)
                    {
                        shape.SkinnedMeshRenderer.SetBlendShapeWeight(i, 0);
                    }

                    // Set the Weight of shape to a single blendshape baked in
                    shape.Weights = new List<VRM0BlendShapeProxy.Weight>
                    {
                        new VRM0BlendShapeProxy.Weight
                        {
                            ShapeKeyName = generatedShapeName,
                            WeightValue = 100,
                        }
                    };
                }
            }
        }

        void AddNewBlendShape(Mesh targetMesh, string blendShapeName, Mesh originalMesh, Mesh blendShapeMesh)
        {
            var vertices = originalMesh.vertices;
            var normals = originalMesh.normals;
            var tangents = originalMesh.tangents;

            var blendShapeVertices = blendShapeMesh.vertices;
            var blendShapeNormals = blendShapeMesh.normals;
            var blendShapeTangents = blendShapeMesh.tangents;

            var deltaVertices = blendShapeVertices.Select((v, j) => v - vertices[j]).ToArray();
            var deltaNormals = blendShapeNormals.Length > 0
                ? blendShapeNormals.Select((n, j) => n - normals[j]).ToArray()
                : null;
            var deltaTangents = blendShapeTangents.Length > 0
                ? blendShapeTangents.Select((t, j) => (Vector3)(t - tangents[j])).ToArray()
                : null;

            targetMesh.AddBlendShapeFrame(blendShapeName, 100, deltaVertices, deltaNormals, deltaTangents);
        }

        (Mesh, Mesh) AssumeReplaced(SkinnedMeshRenderer smr)
        {
            if (bakedMeshes.TryGetValue(smr, out var result)) {
                return result;
            }

            var originalMesh = smr.sharedMesh;

            var nMesh = UnityEngine.Object.Instantiate(originalMesh);
            bakedMeshes.Add(smr, (originalMesh, nMesh));

            smr.sharedMesh = nMesh;

            return (originalMesh, nMesh);
        }

        static string CreateShapeName(string groupName)
        {
            return $"_VGltf.Baked_{groupName}";
        }
    }
}
