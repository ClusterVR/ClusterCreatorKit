using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerLocalUI)), CanEditMultipleObjects]
    public sealed class PlayerLocalUIEditor : VisualElementEditor
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
            return container;
        }
    }
}
