//
// Copyright (c) 2022 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;
using UnityEditor;

namespace VGltf.Ext.Vrm0.Unity.Editor
{
    [CustomEditor(typeof(VRM0FirstPerson))]
    public sealed class VRM0FirstPersonEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            var fp = target as VRM0FirstPerson;
            if (fp == null || fp.FirstPersonBone == null)
            {
                return;
            }

            var pos = fp.FirstPersonBone.transform.position + fp.FirstPersonOffset;

            var newPos = Handles.PositionHandle(pos, Quaternion.identity);
            if (pos != newPos)
            {
                var newOffset = newPos - fp.FirstPersonBone.transform.position;
                fp.FirstPersonOffset = newOffset;
                EditorUtility.SetDirty(fp);
            }
        }
    }
}
