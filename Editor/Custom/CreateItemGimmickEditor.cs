﻿using ClusterVR.CreatorKit.Editor.Extensions;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(CreateItemGimmick), isFallback = true), CanEditMultipleObjects]
    public class CreateItemGimmickEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            container.Add(keyField);

            var itemTemplateProperty = serializedObject.FindProperty("itemTemplate");
            var itemTemplateIsNoneHelpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_need_item_template_property_specification, itemTemplateProperty.displayName), MessageType.Warning));
            var itemTemplateIsNotPrefabHelpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_recommend_prefab_for_item_template_property, itemTemplateProperty.displayName),
                    MessageType.Warning));

            void SwitchDisplayHelp(UnityEngine.Object obj)
            {
                itemTemplateIsNoneHelpBox.SetVisibility(obj == null);
                itemTemplateIsNotPrefabHelpBox.SetVisibility(obj != null &&
                    ((Item.Implements.Item) obj).gameObject.scene.name != null);
            }

            container.Add(itemTemplateIsNoneHelpBox);
            container.Add(itemTemplateIsNotPrefabHelpBox);

            var itemTemplateField = new ObjectField(itemTemplateProperty.displayName)
            {
                tooltip = TranslationTable.cck_prefab_for_generated_item,
                objectType = typeof(Item.Implements.Item),
                value = itemTemplateProperty.objectReferenceValue
            };
            itemTemplateField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
            {
                itemTemplateProperty.objectReferenceValue = e.newValue;
                SwitchDisplayHelp(e.newValue);
                itemTemplateProperty.serializedObject.ApplyModifiedProperties();
            });
            SwitchDisplayHelp(itemTemplateProperty.objectReferenceValue);
            itemTemplateField.Bind(itemTemplateProperty.serializedObject);
            container.Add(itemTemplateField);

            var spawnPointField = new PropertyField(serializedObject.FindProperty("spawnPoint"))
            {
                tooltip = TranslationTable.cck_optional_generation_position
            };
            container.Add(spawnPointField);

            container.Bind(serializedObject);
            return container;
        }

        void OnSceneGUI()
        {
            if (!(target is CreateItemGimmick targetGimmick))
            {
                return;
            }
            MoveAndRotateHandle.Draw(targetGimmick.SpawnPoint, "SpawnPoint");
        }
    }
}
