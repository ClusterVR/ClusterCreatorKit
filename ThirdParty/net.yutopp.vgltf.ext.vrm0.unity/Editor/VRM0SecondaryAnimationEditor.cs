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
    [CustomEditor(typeof(VRM0SecondaryAnimation))]
    public sealed class VRM0SecondaryAnimationEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Selected, typeof(VRM0SecondaryAnimation))]
        private static void DrawGizmo(VRM0SecondaryAnimation sa, GizmoType gizmoType)
        {
            if (sa == null)
            {
                return;
            }

            if (sa.Springs != null)
            {
                Gizmos.color = Color.red;

                foreach (var spring in sa.Springs)
                {
                    if (spring.Bones == null)
                    {
                        continue;
                    }
                    foreach (var bone in spring.Bones)
                    {
                        if (bone == null)
                        {
                            continue;
                        }

                        // TODO: Show all spheres for children bones
                        Gizmos.DrawWireSphere(bone.position, spring.HitRadius);
                    }

                }

                if (sa.ColliderGroups != null)
                {
                    Gizmos.color = Color.yellow;

                    foreach (var cg in sa.ColliderGroups)
                    {
                        if (cg.Node == null)
                        {
                            continue;
                        }
                        if (cg.Colliders == null)
                        {
                            continue;
                        }
                        foreach (var col in cg.Colliders)
                        {
                            var pos = cg.Node.position + col.Offset;
                            Gizmos.DrawWireSphere(pos, col.Radius);
                        }
                    }
                }
            }
        }
    }
}
