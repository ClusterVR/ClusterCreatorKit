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
                    EditorGUILayout.HelpBox($"{nameof(PlayerLocalUI)} は {nameof(Canvas)} の子ではないところに配置してください。",
                        MessageType.Warning);
                }
            });
            container.Add(warningBox);
            return container;
        }
    }
}
