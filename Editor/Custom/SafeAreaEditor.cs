using System.Linq;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SafeArea)), CanEditMultipleObjects]
    public sealed class SafeAreaEditor : VisualElementEditor
    {
        const string AutoAnchorActionName = "Anchor自動設定";

        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var warningBox = new IMGUIContainer(() =>
            {
                if (!IsChildOfPlayerLocalUI((SafeArea) target))
                {
                    EditorGUILayout.HelpBox($"{nameof(SafeArea)} が正しく動作するためには {nameof(PlayerLocalUI)} の子である必要があります。",
                        MessageType.Warning);
                }
            });
            var autoLayoutButton = new Button(AutoAnchor) { text = AutoAnchorActionName };
            container.Add(warningBox);
            container.Add(autoLayoutButton);
            return container;
        }

        void AutoAnchor()
        {
            if (!EditorUtility.DisplayDialog(AutoAnchorActionName, "Anchorを自動設定します。子オブジェクトが端や中央のうち最も近い場所に追従するようになります",
                "OK", "Cancel"))
            {
                return;
            }

            var thisRectTransform = ((SafeArea) target).GetComponent<RectTransform>();
            var thisRect = thisRectTransform.rect;
            var xBorder = thisRect.width / 6;
            var yBorder = thisRect.height / 6;
            foreach (Transform c in thisRectTransform)
            {
                if (!(c is RectTransform child))
                {
                    continue;
                }
                Undo.RecordObject(child, AutoAnchorActionName);
                var pos = child.localPosition;
                var anchorX = pos.x < -xBorder ? 0.0f : pos.x < xBorder ? 0.5f : 1f;
                var anchorY = pos.y < -yBorder ? 0.0f : pos.y < yBorder ? 0.5f : 1f;
                child.anchorMin = new Vector2(anchorX, anchorY);
                child.anchorMax = new Vector2(anchorX, anchorY);
                child.localPosition = pos;
            }

            EditorUtility.DisplayDialog(AutoAnchorActionName, "Anchorの設定が完了しました", "OK");
        }

        static bool IsChildOfPlayerLocalUI(SafeArea safeArea)
        {
            var parent = safeArea.transform.parent;
            return parent != null && parent.GetComponent<PlayerLocalUI>() != null;
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
        static void OnDrawGizmo(SafeArea safeArea, GizmoType gizmoType)
        {
            var canvas = safeArea.GetComponentInParent<Canvas>();
            if (canvas == null || canvas.rootCanvas.renderMode == RenderMode.WorldSpace)
            {
                return;
            }
            var safeAreaRectTransform = safeArea.GetComponent<RectTransform>();
            if ((gizmoType & GizmoType.InSelectionHierarchy) > 0 ||
                Selection.activeTransform != null && Selection.activeTransform.IsChildOf(safeAreaRectTransform))
            {
                DrawGizmo(safeAreaRectTransform);
            }
        }

        static void DrawGizmo(RectTransform safeAreaRectTransform)
        {
            var corners = new Vector3[4];
            safeAreaRectTransform.GetWorldCorners(corners);
            Handles.Label(corners[0], nameof(SafeArea));
            var xMin = corners.Min(c => c.x);
            var xMax = corners.Max(c => c.x);
            var yMin = corners.Min(c => c.y);
            var yMax = corners.Max(c => c.y);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(safeAreaRectTransform.position, new Vector3(xMax - xMin, yMax - yMin, 0f));
        }
    }
}
