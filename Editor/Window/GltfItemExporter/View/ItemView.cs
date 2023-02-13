using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Custom;
using ClusterVR.CreatorKit.Editor.ItemExporter;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ItemExporter;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VGltf;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemView : IDisposable
    {
        const string ValidationMessageTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ValidationMessage.uxml";
        const string ValidationMessageStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessage.uss";
        const string ValidationMessageDarkStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessageDarkStyle.uss";
        const string ValidationMessageLightStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessageLightStyle.uss";

        const int ThumbnailSize = 64;

        Texture2D thumbnail;
        GltfContainer gltfContainer;
        readonly IItemExporter itemExporter;
        readonly IComponentValidator componentValidator;
        readonly IGltfValidator gltfValidator;
        readonly IItemTemplateBuilder builder;

        public event Action<ItemView> OnRemoveButtonClicked;

        public GameObject Item { get; private set; }

        public bool IsValid { get; private set; }

        string Name { get; set; }
        Vector3Int Size { get; set; }

        readonly List<ValidationMessage> validationMessages = new List<ValidationMessage>();

        public ItemView(IItemExporter itemExporter, IComponentValidator componentValidator, IGltfValidator gltfValidator, IItemTemplateBuilder itemTemplateBuilder)
        {
            this.itemExporter = itemExporter;
            this.componentValidator = componentValidator;
            this.gltfValidator = gltfValidator;
            this.builder = itemTemplateBuilder;
        }

        public void SetItem(GameObject item)
        {
            try
            {
                Item = item;
                var itemComponent = item.GetComponent<IItem>();

                if (itemComponent != null)
                {
                    Name = itemComponent.ItemName;
                    Size = itemComponent.Size;
                }
                else
                {
                    Name = "";
                    Size = Vector3Int.zero;
                }

                gltfContainer = ValidateAndBuildGltfContainer();

                CreateThumbnail(item);
            }
            catch (Exception e)
            {
                Clear();
                validationMessages.Add(new ValidationMessage("prefab読み込み時に例外が発生しました。詳細はConsoleを確認してください。",
                    ValidationMessage.MessageType.Error));
                Debug.LogException(e);
            }
        }

        public void BindItemView(VisualElement itemElement)
        {
            var themeStyleSheetPath = EditorGUIUtility.isProSkin
                ? ValidationMessageDarkStyleSheetPath
                : ValidationMessageLightStyleSheetPath;

            var validationMessageTemplate =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ValidationMessageTemplatePath);
            var validationMessageStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(ValidationMessageStylePath);
            var themeValidationMessageStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(themeStyleSheetPath);

            var itemNameLabel = itemElement.Q<Label>("item-name-label");
            itemNameLabel.text = Name;
            var itemSizeLabel = itemElement.Q<Label>("item-size-label");
            itemSizeLabel.text = $"{Size.x} x {Size.y} x {Size.z}";

            var thumbnailImage = itemElement.Q<Image>("thumbnail");
            thumbnailImage.image = thumbnail;

            var validIcon = itemElement.Q<Image>("valid-icon");
            var invalidIcon = itemElement.Q<Image>("invalid-icon");
            validIcon.style.display = IsValid ? DisplayStyle.Flex : DisplayStyle.None;
            invalidIcon.style.display = !IsValid ? DisplayStyle.Flex : DisplayStyle.None;

            var removeButton = itemElement.Q<Button>("remove-button");
            removeButton.clicked += () =>
            {
                OnRemoveButtonClicked?.Invoke(this);
            };

            var validationMessageList = itemElement.Q<ScrollView>("validation-message-list");

            foreach (var validationMessage in validationMessages)
            {
                var validationMessageView = validationMessageTemplate.CloneTree();
                validationMessageView.styleSheets.Add(validationMessageStyle);
                validationMessageView.styleSheets.Add(themeValidationMessageStyle);
                var icon = validationMessageView.Q<Image>("warning-icon");
                icon.SetVisibility(validationMessage.Type == ValidationMessage.MessageType.Warning);
                var messageText = validationMessageView.Q<Label>("validation-message");
                messageText.text = validationMessage.Message;

                validationMessageList.Add(validationMessageView);
            }
        }

        void CreateThumbnail(GameObject gameObject)
        {
            Object.DestroyImmediate(thumbnail);

            thumbnail = new Texture2D(ThumbnailSize, ThumbnailSize)
            {
                hideFlags = HideFlags.DontSave
            };
            try
            {
                using (var itemPreviewRenderer = new ItemPreviewImage())
                {
                    var pngBinary = itemPreviewRenderer.CreatePNG(gameObject);
                    thumbnail.LoadImage(pngBinary);
                }
            }
            catch
            {
                Object.DestroyImmediate(thumbnail);
                throw;
            }
        }

        public void Dispose()
        {
            Object.DestroyImmediate(thumbnail);
        }

        public async Task<byte[]> BuildZippedItemBinary()
        {
            var glbBinary = await gltfContainer.ExportAsync();
            var thumbnailBinary = thumbnail.EncodeToPNG();

            return builder.Build(glbBinary, thumbnailBinary);
        }

        void Clear()
        {
            Item = null;
            gltfContainer = null;
            validationMessages.Clear();
            IsValid = false;
            Object.DestroyImmediate(thumbnail);
        }

        GltfContainer ValidateAndBuildGltfContainer()
        {
            GltfContainer container = null;
            validationMessages.Clear();

            validationMessages.AddRange(GameObjectValidator.Validate(Item.gameObject));
            validationMessages.AddRange(componentValidator.Validate(Item));

            var buildGlbContainerValidationMessages = gltfValidator.Validate(Item).ToList();
            validationMessages.AddRange(buildGlbContainerValidationMessages);
            if (buildGlbContainerValidationMessages.All(message => message.Type != ValidationMessage.MessageType.Error))
            {
                try
                {
                    container = itemExporter.ExportAsGltfContainer(Item);
                    validationMessages.AddRange(gltfValidator.Validate(container));
                }
                catch (Exception e)
                {
                    if (TryGetReadableMessageOfGltfContainerException(e, out var message))
                    {
                        validationMessages.Add(new ValidationMessage(message, ValidationMessage.MessageType.Error));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            if (validationMessages.Count == 0)
            {
                validationMessages.Add(new ValidationMessage("正常なアイテムです。", ValidationMessage.MessageType.Info));
            }
            validationMessages.Sort((a, b) => a.Type.CompareTo(b.Type));
            IsValid = validationMessages.All(message => message.Type != ValidationMessage.MessageType.Error);

            return container;
        }

        bool TryGetReadableMessageOfGltfContainerException(Exception exception, out string message)
        {
            switch (exception)
            {
                case MissingAudioClipException e:
                    message = $"AudioClipがみつかりませんでした。ItemAudioSetにAudioClipを設定してください。(Id: {e.Id})";
                    return true;
                case ExtractAudioDataFailedException e:
                    message = $"AudioClipの情報の取得に失敗しました。(Id: {e.Id})";
                    return true;
                default:
                    message = default;
                    return false;
            }
        }
    }
}
