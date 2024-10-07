using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerLocalUI), isFallback = true), CanEditMultipleObjects]
    public class PlayerLocalUIEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var canvas = ((PlayerLocalUI) target).GetComponent<Canvas>();
            var warningBox = new IMGUIContainer(() =>
            {
                if (!canvas.isRootCanvas)
                {
                    EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_not_child_of_canvas, nameof(PlayerLocalUI), nameof(Canvas)),
                        MessageType.Warning);
                }
            });
            container.Add(warningBox);

            var graphicRaycaster = ((PlayerLocalUI) target).GetComponent<GraphicRaycaster>();
            var graphicRaycasterWarningBox = new IMGUIContainer(() =>
            {
                if (graphicRaycaster == null && ((IPlayerLocalUI) target).SortingOrder == PlayerLocalUISortingOrder.Interactable)
                {
                    EditorGUILayout.HelpBox(TranslationTable.cck_player_local_ui_graphic_raycaster_warning, MessageType.Warning);
                }
            });
            container.Add(graphicRaycasterWarningBox);

            var buttonContainer = new IMGUIContainer(() =>
            {
                EditorGUILayout.HelpBox(
                    TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_interactable_hint),
                    MessageType.Info);
                if (GUILayout.Button(TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_interactable_button)))
                {
                    if (canvas.gameObject.GetComponent<GraphicRaycaster>() == null)
                    {
                        Undo.RecordObject(canvas.gameObject, "Add GraphicRaycaster");
                        canvas.gameObject.AddComponent<GraphicRaycaster>();
                        Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_graphic_raycaster_attach_log, canvas.gameObject.name), canvas.gameObject);
                    }
                    var selectables = canvas.GetComponentsInChildren<Selectable>(true);
                    foreach (var selectable in selectables)
                    {
                        if (!selectable.TryGetComponent(out Collider collider))
                        {
                            Undo.RecordObject(selectable.gameObject, "Add BoxCollider");
                            collider = selectable.gameObject.AddComponent<BoxCollider>();
                            Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_collider_attach_log, selectable.gameObject.name), selectable.gameObject);
                        }
                        if (collider is BoxCollider boxCollider)
                        {
                            Undo.RecordObject(boxCollider, "Update BoxCollider Size");
                            var rectTransform = selectable.GetComponent<RectTransform>();
                            boxCollider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
                            boxCollider.center = new Vector2((rectTransform.pivot.x - 0.5f) * -rectTransform.rect.size.x, (rectTransform.pivot.y - 0.5f) * -rectTransform.rect.size.y);
                            Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_player_local_ui_collider_resized_log, selectable.gameObject.name), selectable.gameObject);
                        }
                    }
                }
            });
            container.Add(buttonContainer);

            return container;
        }
    }
}
