using System;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class MoveAndRotateHandle
    {
        public static void Draw(Vector3 position, Quaternion rotation, string name, Action<Vector3> onMoved, Action<Quaternion> onRotated)
        {
            HandleUtils.Draw(position, rotation, name);
            HandleUtils.AddMoveHandle(position, rotation, onMoved);
            HandleUtils.AddRotationHandle(position, rotation, onRotated);
        }

        public static void Draw(Transform target, string name)
        {
            if (target == null)
            {
                return;
            }
            Draw(target.position, target.rotation, name,
                newPosition =>
                {
                    Undo.RecordObject(target, $"Move {name}");
                    target.position = newPosition;
                },
                newRotation =>
                {
                    Undo.RecordObject(target, $"Rotate {name}");
                    target.rotation = newRotation;
                });
        }
    }
}
