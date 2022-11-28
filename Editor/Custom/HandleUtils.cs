using System;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class HandleUtils
    {
        static readonly Color HandleColor = new(0, 1, 0, 0.4f);

        public static void Draw(Vector3 position, Quaternion rotation, string name)
        {
            using (new Handles.DrawingScope(HandleColor))
            {
                Handles.Label(position, name, EditorStyles.boldLabel);
                Handles.CubeHandleCap(0, position, rotation, 0.2f, EventType.Repaint);
            }
        }

        public static void AddMoveHandle(Vector3 position, Quaternion rotation, Action<Vector3> onMoved)
        {
            if (Tools.current != Tool.Move)
            {
                return;
            }
            var changed = false;
            Vector3 newPosition;
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                newPosition = Handles.PositionHandle(position, Tools.pivotRotation == PivotRotation.Local ? rotation : Quaternion.identity);
                changed = changeCheck.changed;
            }
            if (changed)
            {
                onMoved(newPosition);
            }
        }

        public static void AddRotationHandle(Vector3 position, Quaternion rotation, Action<Quaternion> onRotated)
        {
            if (Tools.current != Tool.Rotate)
            {
                return;
            }
            var changed = false;
            Quaternion newRotation;
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                newRotation = Handles.RotationHandle(rotation, position);
                changed = changeCheck.changed;
            }
            if (changed)
            {
                onRotated(newRotation);
            }
        }
    }
}
