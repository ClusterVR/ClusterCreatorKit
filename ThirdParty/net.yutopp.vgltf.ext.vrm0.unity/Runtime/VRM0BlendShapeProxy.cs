//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using UnityEngine;

namespace VGltf.Ext.Vrm0.Unity
{
    public sealed class VRM0BlendShapeProxy : MonoBehaviour
    {
        [Serializable]
        public sealed class BlendShapeGroup
        {
            [SerializeField] public string Name;
            [SerializeField] public BlendShapePreset Preset;

            [SerializeField] public List<MeshShape> MeshShapes = new List<MeshShape>();
        }

        public enum BlendShapePreset
        {
            Unknown,

            Neutral,

            A,
            I,
            U,
            E,
            O,

            Blink,

            // 喜怒哀楽
            Joy,
            Angry,
            Sorrow,
            Fun,

            // LookAt
            LookUp,
            LookDown,
            LookLeft,
            LookRight,

            Blink_L,
            Blink_R,
        }

        [Serializable]
        public sealed class MeshShape
        {
            [SerializeField] public SkinnedMeshRenderer SkinnedMeshRenderer;
            [SerializeField] public List<Weight> Weights = new List<Weight>();
        }

        [Serializable]
        public sealed class Weight
        {
            [SerializeField] public string ShapeKeyName;
            [SerializeField] public float WeightValue;
        }

        [SerializeField] public List<BlendShapeGroup> Groups = new List<BlendShapeGroup>();
    }
}
