//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;

namespace VGltf.Ext.Vrm0.Unity
{
    public sealed class VRM0SecondaryAnimation : MonoBehaviour
    {
        [Serializable]
        public sealed class Spring
        {
            [SerializeField] public string Comment;
            [SerializeField] public float Stiffiness;
            [SerializeField] public float GravityPower;
            [SerializeField] public Vector3 GravityDir;
            [SerializeField] public float DragForce;
            [SerializeField] public Transform Center;
            [SerializeField] public float HitRadius;
            [SerializeField] public Transform[] Bones;
            [SerializeField] public int[] ColliderGroups;
        }

        [Serializable]
        public sealed class ColliderGroup
        {
            [SerializeField] public Transform Node;
            [SerializeField] public Collider[] Colliders;
        }

        [Serializable]
        public sealed class Collider
        {
            [SerializeField] public Vector3 Offset;
            [SerializeField] public float Radius;
        }

        [SerializeField] public Spring[] Springs;
        [SerializeField] public ColliderGroup[] ColliderGroups;
    }
}
