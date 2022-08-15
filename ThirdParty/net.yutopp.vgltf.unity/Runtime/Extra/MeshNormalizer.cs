//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
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
    // Normalize transform/scale of mesh nodes (rotation will not be affacted).
    public sealed class MeshNormalizer : IDisposable
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

            BakeMeshes(nGo, true);
            NormalizeTransforms(nGo.transform, Matrix4x4.identity);
        }

        public void BakeMeshes(GameObject go, bool isAncestorUniform)
        {
            var isUniform = IsUniform(go.transform.localScale);

            var mf = go.GetComponent<MeshFilter>();
            if (mf != null)
            {
                if (!isAncestorUniform && !Mathf.Approximately(Quaternion.Angle(Quaternion.identity, go.transform.localRotation), 0.0f))
                {
                    // NOTE: Could not apply rotations for children of the node which has non-uniformed scale.
                    throw new Exception($"{go.name} has ancestors which have a non-uniformed scale");
                }

                var ot = go.transform.localPosition;
                var or = go.transform.localRotation;

                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.identity;

                mf.sharedMesh = BakeMeshAndMemoize(mf.sharedMesh, go.transform);

                // o Box
                // o Capsule
                // o Mesh
                // o Sphere
                // x Terrain
                // x Wheel
                foreach (var c in go.GetComponents<Collider>())
                {
                    switch (c)
                    {
                        // BoxCollider can be scaled by non-uniformed vec3.
                        case BoxCollider bc:
                        {
                            bc.size = Vector3.Scale(bc.size, go.transform.lossyScale);
                            bc.center = go.transform.TransformPoint(bc.center);
                            break;
                        }

                        // MeshCollider can be scaled by non-uniformed vec3.
                        case MeshCollider mc:
                        {
                            mc.sharedMesh = BakeMeshAndMemoize(mc.sharedMesh, go.transform);
                            break;
                        }

                        case SphereCollider sc:
                        {
                            if (!isUniform)
                            {
                                throw new Exception($"{go.name} has both of SphereCollider and non-uniformed scale");
                            }

                            var scale = go.transform.lossyScale.x; // NOTE: all elements are same values
                            sc.radius = scale * sc.radius;
                            sc.center = go.transform.TransformPoint(sc.center);
                            break;
                        }

                        case CapsuleCollider cc:
                        {
                            if (!isUniform)
                            {
                                throw new Exception($"{go.name} has both of CapsuleCollider and non-uniformed scale");
                            }

                            var scale = go.transform.lossyScale.x; // NOTE: all elements are same values
                            cc.radius = scale * cc.radius;
                            cc.height = scale * cc.height;
                            cc.center = go.transform.TransformPoint(cc.center);
                            break;
                        }
                    }
                }

                go.transform.localPosition = ot;
                go.transform.localRotation = or;
            }

            for (var i = 0; i < go.transform.childCount; ++i)
            {
                var ct = go.transform.GetChild(i);
                BakeMeshes(ct.gameObject, !(!isUniform || !isAncestorUniform));
            }
        }

        public static void NormalizeTransforms(Transform t, Matrix4x4 m)
        {
            // Apply parents' scales, then make local values of them to identity.            
            var cm = m * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, t.localScale);

            t.localRotation = m.rotation * t.localRotation;
            t.localScale = Vector3.one;
            t.localPosition = m * t.localPosition;

            for (var i = 0; i < t.childCount; ++i)
            {
                var ct = t.GetChild(i);
                NormalizeTransforms(ct, cm);
            }
        }

        Mesh BakeMeshAndMemoize(Mesh m, Transform t)
        {
            var mesh = UnityEngine.Object.Instantiate(m);
            bakedMeshes.Add(mesh);

            mesh.name = m.name;

            mesh.vertices = mesh.vertices.Select(t.TransformPoint).ToArray();
            mesh.normals = mesh.normals.Select(t.TransformDirection).ToArray();
            // mesh.tangents = mesh.tangents.Select(go.transform.TransformVector).ToArray();

            return mesh;
        }

        static bool IsUniform(Vector3 v)
        {
            return Mathf.Approximately(v.x, v.y) && Mathf.Approximately(v.y, v.z);
        }
    }
}
