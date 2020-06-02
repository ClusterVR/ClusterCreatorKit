using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class MoveAndRotateHandle
    {
        public static void Draw(Transform target, string name)
        {
            if (target == null) return;
            var position = target.position;
            var rotation = target.rotation;
            using (new Handles.DrawingScope(new Color(0, 1, 0, 0.4f)))
            {
                Handles.Label(position, name, EditorStyles.boldLabel);
                Handles.CubeHandleCap(0, position, rotation, 0.2f, EventType.Repaint);
            }

            switch (Tools.current)
            {
                case Tool.Move:
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        position = Handles.PositionHandle(position, Tools.pivotRotation == PivotRotation.Local ? rotation : Quaternion.identity);
                        if (changeCheck.changed)
                        {
                            Undo.RecordObject(target, $"Move {name}");
                            target.position = position;
                        }
                    }
                    break;
                case Tool.Rotate:
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        rotation = Handles.RotationHandle(rotation, position);
                        if (changeCheck.changed)
                        {
                            Undo.RecordObject(target, $"Rotate {name}");
                            target.rotation = rotation;
                        }
                    }
                    break;
            }
        }
    }
}