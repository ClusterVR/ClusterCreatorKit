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
    public sealed class VRM0FirstPerson : MonoBehaviour
    {
        [SerializeField] public Transform FirstPersonBone;

        [SerializeField] public Vector3 FirstPersonOffset;
    }
}
