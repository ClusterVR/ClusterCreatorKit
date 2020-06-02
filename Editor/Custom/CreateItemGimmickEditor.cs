using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(CreateItemGimmick)), CanEditMultipleObjects]
    public sealed class CreateItemGimmickEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var keyField = new PropertyField(serializedObject.FindProperty("key"));
            container.Add(keyField);

            var itemTemplateProperty = serializedObject.FindProperty("itemTemplate");
            var itemTemplateIsNoneHelpBox = new IMGUIContainer(() => EditorGUILayout.HelpBox($"{itemTemplateProperty.displayName} を指定する必要があります。", MessageType.Warning));
            var itemTemplateIsNotPrefabHelpBox = new IMGUIContainer(() => EditorGUILayout.HelpBox($"{itemTemplateProperty.displayName} には Prefab の指定を推奨しています。", MessageType.Warning));
            void SwitchDisplayHelp(UnityEngine.Object obj)
            {
                itemTemplateIsNoneHelpBox.SetVisibility(obj == null);
                itemTemplateIsNotPrefabHelpBox.SetVisibility(obj != null && ((Item.Implements.Item)obj).gameObject.scene.name != null);
            }
            container.Add(itemTemplateIsNoneHelpBox);
            container.Add(itemTemplateIsNotPrefabHelpBox);

            var itemTemplateField = new ObjectField(itemTemplateProperty.displayName)
            {
                tooltip = "生成するアイテムのprefab",
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
                tooltip = "生成位置(任意)"
            };
            container.Add(spawnPointField);

            container.Bind(serializedObject);
            return container;
        }

        void OnSceneGUI()
        {
            if (!(target is CreateItemGimmick targetGimmick)) return;
            MoveAndRotateHandle.Draw(targetGimmick.SpawnPoint, "SpawnPoint");
        }
    }
}