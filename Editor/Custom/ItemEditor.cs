using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(CreatorKit.Item.Implements.Item)), CanEditMultipleObjects]
    public sealed class ItemEditor : UnityEditor.Editor
    {
        const string SetDefaultSizeText = "Set Default Size";

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var item = (Item.Implements.Item) target;
            var idField = new TextField("Id") { value = item.Id.Value.ToString(), isReadOnly = true };
            container.Add(idField);
            var itemNameField = new PropertyField(serializedObject.FindProperty("itemName"));
            container.Add(itemNameField);
            var sizeField = new PropertyField(serializedObject.FindProperty("size"));
            container.Add(sizeField);

            var setDefaultSizeButton = new Button()
            {
                text = SetDefaultSizeText
            };
            setDefaultSizeButton.clicked += () =>
            {
                if (!EditorUtility.DisplayDialog(SetDefaultSizeText, "GameObjectのBoundsからItemのSizeを自動設定します",
                        "OK", "Cancel"))
                {
                    return;
                }

                serializedObject.Update();
                if (CalcDefaultSize(item, out var defaultSize))
                {
                    var size = serializedObject.FindProperty("size");
                    size.vector3IntValue = defaultSize;
                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    EditorUtility.DisplayDialog(SetDefaultSizeText, "ItemのSizeを自動設定するにはRendererが必要です", "OK");
                }
            };

            container.Add(setDefaultSizeButton);
            container.Bind(serializedObject);
            return container;
        }

        bool CalcDefaultSize(Item.Implements.Item item, out Vector3Int defaultSize)
        {
            BoundsCalculator.CalcLocalBounds(item.gameObject, out var bounds, out _);
            if (bounds.HasValue)
            {
                var size = bounds.Value.size;
                defaultSize = new Vector3Int(Mathf.RoundToInt(size.x), Mathf.RoundToInt(size.y), Mathf.RoundToInt(size.z));
                return true;
            }
            else
            {
                defaultSize = default;
                return false;
            }
        }
    }
}
