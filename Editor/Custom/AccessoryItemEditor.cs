using System.IO;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(AccessoryItem)), CanEditMultipleObjects]
    public sealed class AccessoryItemEditor : VisualElementEditor
    {
        const string SetUsableShaderText = "アクセサリーに使えるシェーダーに変更する";
        const string MToonShaderName = "VRM/MToon";

        Button setUsableShaderButton;

        void OnSceneGUI()
        {
            ShowDefaultAttachOffsetHandle();
        }

        void ShowDefaultAttachOffsetHandle()
        {
            if (target is not AccessoryItem accessoryItem)
            {
                return;
            }
            var transform = accessoryItem.transform;
            var rootPosition = transform.position;
            var rootRotation = transform.rotation;

            var handlePosition = rootPosition + (rootRotation * Quaternion.Inverse(accessoryItem.DefaultAttachOffsetRotation)) * -accessoryItem.DefaultAttachOffsetPosition;
            var handleRotation = rootRotation * Quaternion.Inverse(accessoryItem.DefaultAttachOffsetRotation);
            var handleName = accessoryItem.DefaultAttachBoneName.ToString();
            HandleUtils.Draw(handlePosition, handleRotation, handleName);
            HandleUtils.AddMoveHandle(handlePosition, handleRotation,
                newPosition =>
                {
                    var localPosition = (accessoryItem.DefaultAttachOffsetRotation * Quaternion.Inverse(rootRotation)) * -(newPosition - rootPosition);
                    Undo.RecordObject(accessoryItem, "Move DefaultAttachOffsetPosition");
                    accessoryItem.UpdateDefaultAttachOffsetPosition(localPosition);
                });
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            container.Add(base.CreateInspectorGUI());
            container.Add(CreateSetUsableShaderButton());
            return container;
        }

        VisualElement CreateSetUsableShaderButton()
        {
            setUsableShaderButton = new Button()
            {
                text = SetUsableShaderText
            };
            setUsableShaderButton.clicked += TrySetUsableShader;
            UpdateSetUsableShaderButtonVisibility();
            return setUsableShaderButton;
        }

        void TrySetUsableShader()
        {
            var targetShader = Shader.Find(MToonShaderName);
            if (targetShader == null)
            {
                EditorUtility.DisplayDialog(SetUsableShaderText, $"{MToonShaderName} Shaderが見つかりませんでした。Creator Kitが正しく導入されているか確認してください", "OK");
                return;
            }

            var targetMaterials = GatherTargetMaterials();

            if (targetMaterials.Count == 0)
            {
                EditorUtility.DisplayDialog(SetUsableShaderText, $"全てのShaderは利用可能です", "OK");
                return;
            }

            var folderAsked = false;
            var folderToSave = "";
            var saveCanceled = false;
            var changed = false;
            foreach (var t in targetMaterials)
            {
                var material = t.Key;
                if (IsEditableAsset(material))
                {
                    material.shader = targetShader;
                    MToon.Utils.ValidateProperties(material);
                    EditorUtility.SetDirty(material);
                }
                else
                {
                    if (!folderAsked)
                    {
                        while (true)
                        {
                            folderToSave = EditorUtility.OpenFolderPanel("マテリアルを保存するフォルダーを選んで下さい", "Assets", "");
                            if (folderToSave == null)
                            {
                                saveCanceled = true;
                                break;
                            }

                            folderToSave = Path.GetRelativePath(Directory.GetCurrentDirectory(), folderToSave);
                            if (!folderToSave.StartsWith("Assets"))
                            {
                                EditorUtility.DisplayDialog(SetUsableShaderText, $"Assets内のフォルダーを選択してください", "OK");
                                continue;
                            }

                            break;
                        }
                        folderAsked = true;
                    }
                    if (saveCanceled)
                    {
                        continue;
                    }

                    var copiedMaterial = new Material(material);
                    copiedMaterial.shader = targetShader;
                    MToon.Utils.ValidateProperties(copiedMaterial);

                    AssetDatabase.CreateAsset(copiedMaterial, GetUniquePath(folderToSave, copiedMaterial));
                    foreach (var (renderer, index) in t)
                    {
                        var materials = renderer.sharedMaterials;
                        materials[index] = copiedMaterial;
                        renderer.sharedMaterials = materials;
                        EditorUtility.SetDirty(renderer);
                    }
                }
                changed = true;
            }
            if (changed)
            {
                EditorUtility.DisplayDialog(SetUsableShaderText, $"RendererのMaterialのShaderを{MToonShaderName}に変更しました", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog(SetUsableShaderText, $"Shaderの変更はキャンセルされました", "OK");
            }
            UpdateSetUsableShaderButtonVisibility();
        }

        static string GetUniquePath(string folderPath, Material material)
        {
            var name = material.name;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "New Material.mat";
            }
            else
            {
                name = name + ".mat";
            }
            var path = Path.Combine(folderPath, name);
            return AssetDatabase.GenerateUniqueAssetPath(path);
        }

        bool HasTargetMaterial()
        {
            return ((Component) target).GetComponentsInChildren<Renderer>(true)
                .SelectMany(r => r.sharedMaterials)
                .Any(IsUnusableShaderMaterial);
        }

        ILookup<Material, (Renderer, int)> GatherTargetMaterials()
        {
            return ((Component) target).GetComponentsInChildren<Renderer>(true)
                .SelectMany(r => r.sharedMaterials
                    .Select((m, i) => (m, i))
                    .Where(t => IsUnusableShaderMaterial(t.m))
                    .Select(t => (t.m, (r, t.i))))
                .ToLookup(t => t.m, t => t.Item2);
        }

        static bool IsUnusableShaderMaterial(Material material) => material != null && !IsUsableShader(material.shader);

        static bool IsUsableShader(Shader shader)
        {
            return shader != null && shader.name == MToonShaderName;
        }

        static bool IsEditableAsset(Material material)
        {
            if (!AssetDatabase.IsMainAsset(material))
            {
                return false;
            }
            var path = AssetDatabase.GetAssetPath(material);
            return !string.IsNullOrEmpty(path) && path.StartsWith("Assets");
        }

        public override void OnInspectorGUI()
        {
            UpdateSetUsableShaderButtonVisibility();
            base.OnInspectorGUI();
        }

        void UpdateSetUsableShaderButtonVisibility()
        {
            if (setUsableShaderButton == null)
            {
                return;
            }
            setUsableShaderButton.SetVisibility(HasTargetMaterial());
        }
    }
}
