using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemView : VisualElement
    {
        const string ItemTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ItemView.uxml";

        const string ValidationMessageTemplatePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uxml/ValidationMessage.uxml";
        const string ValidationMessageStylePath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessage.uss";
        const string ValidationMessageDarkStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessageDarkStyle.uss";
        const string ValidationMessageLightStyleSheetPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Window/GltfItemExporter/Uss/ValidationMessageLightStyle.uss";

        public ItemView()
        {
            var itemTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ItemTemplatePath);
            var view = itemTemplate.CloneTree();
            hierarchy.Add(view);
        }

        public static void Bind(ItemViewModel itemViewModel, ItemView itemView)
        {
            var themeStyleSheetPath = EditorGUIUtility.isProSkin
                ? ValidationMessageDarkStyleSheetPath
                : ValidationMessageLightStyleSheetPath;

            var validationMessageTemplate =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ValidationMessageTemplatePath);
            var validationMessageStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(ValidationMessageStylePath);
            var themeValidationMessageStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(themeStyleSheetPath);

            var itemNameLabel = itemView.Q<Label>("item-name-label");
            itemNameLabel.text = itemViewModel.Name;
            var itemSizeLabel = itemView.Q<Label>("item-size-label");
            itemSizeLabel.text = $"{itemViewModel.Size.x} x {itemViewModel.Size.y} x {itemViewModel.Size.z}";

            var thumbnailImage = itemView.Q<Image>("thumbnail");
            thumbnailImage.image = itemViewModel.UploadingItem.Thumbnail;

            var validIcon = itemView.Q<Image>("valid-icon");
            var invalidIcon = itemView.Q<Image>("invalid-icon");
            validIcon.style.display = itemViewModel.IsValid ? DisplayStyle.Flex : DisplayStyle.None;
            invalidIcon.style.display = !itemViewModel.IsValid ? DisplayStyle.Flex : DisplayStyle.None;

            var removeButton = itemView.Q<Button>("remove-button");
            removeButton.clicked += () =>
            {
                itemViewModel.InvokeOnRemoveButtonClicked(itemViewModel);
            };

            var validationMessageList = itemView.Q<ScrollView>("validation-message-list");

            foreach (var validationMessage in itemViewModel.UploadingItem.ValidationMessages)
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
    }
}
