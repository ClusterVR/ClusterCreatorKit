using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ProductDisplayItem), editorForChildClasses: true, isFallback = true), CanEditMultipleObjects]
    public class ProductDisplayItemEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var avatarFacialExpressionTypeField = FindPropertyField(container, "productDisplayAvatarFacialExpressionType");
            var avatarFacialExpressionTypeFieldIndex = container.IndexOf(avatarFacialExpressionTypeField);
            var avatarPoseField = FindPropertyField(container, "productDisplayAvatarPose");

            avatarFacialExpressionTypeField.label = TranslationTable.cck_product_display_avatar_facial_expression_label;
            avatarPoseField.label = TranslationTable.cck_product_display_avatar_pose_label;

            var avatarLabelText = new TextElement
            {
                text = TranslationTable.cck_product_display_avatar_section_text,
                style =
                {
                    marginTop = 10,
                }
            };
            container.Insert(avatarFacialExpressionTypeFieldIndex, avatarLabelText);
            avatarFacialExpressionTypeField.style.marginLeft = 10;
            avatarPoseField.style.marginLeft = 10;

            var avatarPoseInfoHelpBox = new IMGUIContainer(() =>
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox(TranslationTable.cck_product_display_avatar_pose_anim_length_helpbox_text, MessageType.Info);
                EditorGUI.indentLevel--;
            });
            container.Add(avatarPoseInfoHelpBox);

            avatarPoseField.RegisterValueChangeCallback(ev =>
            {
                var objectReferenceValue = ev.changedProperty.objectReferenceValue;
                if (objectReferenceValue == null)
                {
                    avatarPoseInfoHelpBox.SetVisibility(false);
                    return;
                }
                var animationClip = objectReferenceValue as AnimationClip;
                if (animationClip == null)
                {
                    avatarPoseInfoHelpBox.SetVisibility(false);
                    return;
                }
                avatarPoseInfoHelpBox.SetVisibility(animationClip.length > 0f);
            });

            return container;
        }

        void OnSceneGUI()
        {
            if (target is not ProductDisplayItem productDisplayItem)
            {
                return;
            }
            MoveAndRotateHandle.Draw(productDisplayItem.ProductDisplayRoot, "ProductDisplayRoot");
        }
    }
}
