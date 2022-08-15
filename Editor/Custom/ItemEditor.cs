using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                var size = serializedObject.FindProperty("size");
                size.vector3IntValue = CalcDefaultSize(item);
                serializedObject.ApplyModifiedProperties();
            };

            container.Add(setDefaultSizeButton);
            container.Bind(serializedObject);
            return container;
        }

        Vector3Int CalcDefaultSize(Item.Implements.Item item)
        {
            var gameObject = item.gameObject;
            var previewScene = EditorSceneManager.NewPreviewScene();
            try
            {
                var go = Instantiate(gameObject);
                SceneManager.MoveGameObjectToScene(go, previewScene);
                go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                var bounds = go.GetComponentsInChildren<Renderer>(true)
                    .Select(r => r.bounds)
                    .Aggregate((result, b) =>
                    {
                        result.Encapsulate(b);
                        return result;
                    });
                var size = bounds.max - bounds.min;
                return new Vector3Int(Mathf.RoundToInt(size.x), Mathf.RoundToInt(size.y), Mathf.RoundToInt(size.z));
            }
            finally
            {
                EditorSceneManager.ClosePreviewScene(previewScene);
            }
        }
    }
}
