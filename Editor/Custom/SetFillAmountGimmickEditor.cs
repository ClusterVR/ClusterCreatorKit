using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetFillAmountGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetFillAmountGimmickEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var image = ((Component) target).GetComponent<Image>();
            var helpBox = new IMGUIContainer(() =>
            {
                if (image.sprite == null)
                {
                    EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_image_source_image_required, nameof(Image)), MessageType.Warning);
                }
                else if (image.type != Image.Type.Filled)
                {
                    EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_image_type_filled_required, nameof(Image), nameof(Image.Type.Filled)),
                        MessageType.Warning);
                }
            });
            container.Add(helpBox);
            return container;
        }
    }
}
