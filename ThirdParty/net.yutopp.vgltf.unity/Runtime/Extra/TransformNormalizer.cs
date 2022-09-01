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

namespace VGltf.Unity.Ext
{
    public sealed class TransformNormalizer : IDisposable
    {
        public GameObject Go { get; private set; }
        readonly HashSet<Mesh> bakedMeshes = new HashSet<Mesh>();

        void IDisposable.Dispose()
        {
            if (Go != null)
            {
                Utils.Destroy(Go);
                Go = null;
            }

            // Destroy baked meshes
            foreach (var kv in bakedMeshes)
            {
                Utils.Destroy(kv);
            }
            bakedMeshes.Clear();
        }

        public void Normalize(GameObject go)
        {
            var nGo = UnityEngine.Object.Instantiate(go) as GameObject;
            try
            {
                Normalized(go, nGo);
            }
            catch (Exception)
            {
                Utils.Destroy(nGo);
                throw;
            }

            Go = nGo;
        }

        void Normalized(GameObject go, GameObject nGo)
        {
            nGo.name = go.name;

            var anim = go.GetComponent<Animator>();
            if (anim == null)
            {
                return;
            }

            BakeMeshes(nGo);
            NormalizeTransforms(nGo.transform, Matrix4x4.identity);
            UpdateBonePoses(nGo);
        }

        public void BakeMeshes(GameObject go)
        {
            // Fix TRS to origin ans bake meshes because skined meshes will be transformed by bindposes.
            var smr = go.GetComponent<SkinnedMeshRenderer>();
            if (smr != null)
            {
                var sharedMesh = smr.sharedMesh;

                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;

                if (go.GetComponent<Animator>() != null)
                {
                    GameObject.Destroy(go.GetComponent<Animator>());
                }

                for (var i = 0; i < sharedMesh.blendShapeCount; ++i)
                {
                    smr.SetBlendShapeWeight(i, 0.0f);
                }

                // Bake
                var mesh = new Mesh();
                bakedMeshes.Add(mesh);

                smr.BakeMesh(mesh);

                mesh.name = sharedMesh.name;
                mesh.boneWeights = sharedMesh.boneWeights;

                var vertices = mesh.vertices;
                var normals = mesh.normals;
                var tangents = mesh.tangents;
                for (var i = 0; i < sharedMesh.blendShapeCount; ++i)
                {
                    var blendShapeName = sharedMesh.GetBlendShapeName(i);
                    var blendShapeFrame = sharedMesh.GetBlendShapeFrameCount(i);
                    var blendShapeWeight = sharedMesh.GetBlendShapeFrameWeight(i, blendShapeFrame - 1);

                    smr.SetBlendShapeWeight(i, blendShapeWeight);
                    var blendShapeMesh = new Mesh();
                    try
                    {
                        smr.BakeMesh(blendShapeMesh);

                        var blendShapeVertices = blendShapeMesh.vertices;
                        var blendShapeNormals = blendShapeMesh.normals;
                        var blendShapeTangents = blendShapeMesh.tangents;

                        var deltaVertices =
                            blendShapeVertices.Select((v, j) => v - vertices[j]).ToArray();
                        var deltaNormals =
                            blendShapeNormals.Length > 0
                            ? blendShapeNormals.Select((n, j) => n - normals[j]).ToArray()
                            : null;
                        var deltaTangents =
                            blendShapeTangents.Length > 0
                            ? blendShapeTangents.Select((t, j) => (Vector3)(t - tangents[j])).ToArray()
                            : null;

                        mesh.AddBlendShapeFrame(
                            blendShapeName,
                            blendShapeWeight,
                            deltaVertices,
                            deltaNormals,
                            deltaTangents
                            );
                    }
                    finally
                    {
                        GameObject.DestroyImmediate(blendShapeMesh);
                        smr.SetBlendShapeWeight(i, 0.0f);
                    }
                }

                smr.sharedMesh = mesh;
            }

            var mf = go.GetComponent<MeshFilter>();
            if (mf != null)
            {
                mf.sharedMesh = BakeMeshAndMemoize(mf.sharedMesh, go.transform);
            }

            for (var i = 0; i < go.transform.childCount; ++i)
            {
                var ct = go.transform.GetChild(i);
                BakeMeshes(ct.gameObject);
            }
        }

        public static void NormalizeTransforms(Transform t, Matrix4x4 m)
        {
            // Apply parents' rotations and scales, then make local values of them to identity.
            var cm = m * Matrix4x4.TRS(Vector3.zero, t.localRotation, t.localScale);

            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            t.localPosition = m * t.localPosition;

            for (var i = 0; i < t.childCount; ++i)
            {
                var ct = t.GetChild(i);
                NormalizeTransforms(ct, cm);
            }
        }

        public void UpdateBonePoses(GameObject go)
        {
            var smr = go.GetComponent<SkinnedMeshRenderer>();
            if (smr != null)
            {
                var replaceMap = new Dictionary<int, int>();
                var bonesList = new List<Transform>();
                foreach (var (bone, boneIndexOrig) in smr.bones.Select((b, i) => (b, i)))
                {
                    if (bone == null)
                    {
                        continue;
                    }

                    var boneIndex = bonesList.Count;
                    replaceMap.Add(boneIndexOrig, boneIndex);

                    bonesList.Add(bone);
                }

                var bones = bonesList.ToArray();
                var newBindPoses = bones.Select(t =>
                {
                    // Ref: https://forum.unity.com/threads/runtime-model-import-wrong-bind-pose.276411/
                    var newTrans = t.worldToLocalMatrix * go.transform.localToWorldMatrix;
                    return newTrans;
                }).ToArray();
                Debug.Assert(newBindPoses.Count() == bones.Count());

                var mesh = smr.sharedMesh;
                mesh.boneWeights = mesh.boneWeights.Select(bwOrig =>
                {
                    var bw = new BoneWeight
                    {
                        weight0 = bwOrig.weight0,
                        weight1 = bwOrig.weight1,
                        weight2 = bwOrig.weight2,
                        weight3 = bwOrig.weight3,
                        // NOTE: Raise exceptions if nodes which expected to be removed have any bone weights.
                        boneIndex0 = replaceMap[bwOrig.boneIndex0],
                        boneIndex1 = replaceMap[bwOrig.boneIndex1],
                        boneIndex2 = replaceMap[bwOrig.boneIndex2],
                        boneIndex3 = replaceMap[bwOrig.boneIndex3]
                    };
                    return bw;
                }).ToArray();
                mesh.bindposes = newBindPoses;
                mesh.RecalculateBounds();

                smr.bones = bones;
            }

            for (var i = 0; i < go.transform.childCount; ++i)
            {
                var ct = go.transform.GetChild(i);
                UpdateBonePoses(ct.gameObject);
            }
        }

        Mesh BakeMeshAndMemoize(Mesh m, Transform t)
        {
            var mesh = UnityEngine.Object.Instantiate(m);
            bakedMeshes.Add(mesh);

            mesh.name = m.name;

            mesh.vertices = mesh.vertices.Select(t.TransformVector).ToArray();
            mesh.normals = mesh.normals.Select(t.TransformDirection).ToArray();
            // mesh.tangents = mesh.tangents.Select(go.transform.TransformVector).ToArray();

            return mesh;
        }
    }
}
