using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.ItemExporter;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.ItemExporter;
using ClusterVR.CreatorKit.ItemExporter.ExporterHooks;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;
using VGltf;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{

    public sealed class ItemBuilder
    {
        const int ThumbnailSize = 64;

        readonly bool isBetaAllowedItemType;
        readonly IComponentValidator componentValidator;
        readonly IGltfValidator gltfValidator;
        readonly IItemExporter itemExporter;
        readonly IItemTemplateBuilder itemTemplateBuilder;

        public ItemBuilder(IItemBuilderDependencies dependencies)
        {
            isBetaAllowedItemType = dependencies.IsBetaAllowedItemType;
            componentValidator = dependencies.ComponentValidator;
            gltfValidator = dependencies.GltfValidator;
            itemExporter = dependencies.ItemExporter;
            itemTemplateBuilder = dependencies.ItemTemplateBuilder;
        }

        public UploadingItem BuildItem(GameObject item, bool isBetaRequested)
        {
            try
            {

                string name;
                Vector3Int size;
                var itemComponent = item.GetComponent<IItem>();

                if (itemComponent != null)
                {
                    name = itemComponent.ItemName;
                    size = itemComponent.Size;

                    BuildHumanoidAnimation(itemComponent);
                }
                else
                {
                    name = "";
                    size = Vector3Int.zero;
                }

                var validationMessages = new List<ValidationMessage>();
                var gltfContainer = ValidateAndBuildGltfContainer(item, isBetaRequested, validationMessages, out var isValid);

                CreateThumbnail(item, out var thumbnail);

                return new UploadingItem(item, isValid, name, size, gltfContainer, thumbnail, validationMessages);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return new UploadingItem(null, false, null, default, null, null,
                    new List<ValidationMessage>
                {
                    new(TranslationTable.cck_prefab_load_exception, ValidationMessage.MessageType.Error)
                });
            }
        }

        static void CreateThumbnail(GameObject gameObject, out Texture2D thumbnail)
        {
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

        GltfContainer ValidateAndBuildGltfContainer(GameObject item, bool isBetaRequested,
            List<ValidationMessage> validationMessages, out bool isValid)
        {
            var applyBeta = isBetaAllowedItemType && isBetaRequested;
            GltfContainer container = null;
            validationMessages.Clear();

            validationMessages.AddRange(GameObjectValidator.Validate(item.gameObject));
            validationMessages.AddRange(componentValidator.Validate(item, applyBeta));

            var buildGlbContainerValidationMessages = gltfValidator.Validate(item).ToList();
            validationMessages.AddRange(buildGlbContainerValidationMessages);
            if (buildGlbContainerValidationMessages.All(message => message.Type != ValidationMessage.MessageType.Error))
            {
                try
                {
                    container = itemExporter.ExportAsGltfContainer(item, applyBeta);
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
                validationMessages.Add(new ValidationMessage(TranslationTable.cck_item_valid, ValidationMessage.MessageType.Info));
            }
            validationMessages.Sort((a, b) => a.Type.CompareTo(b.Type));
            isValid = validationMessages.All(message => message.Type != ValidationMessage.MessageType.Error);

            return container;
        }

        static bool TryGetReadableMessageOfGltfContainerException(Exception exception, out string message)
        {
            switch (exception)
            {
                case MissingAudioClipException e:
                    message = TranslationUtility.GetMessage(TranslationTable.cck_audioclip_not_found_exception, e.Id);
                    return true;
                case MissingHumanoidAnimationException e:
                    message = TranslationUtility.GetMessage(TranslationTable.cck_animation_not_found_exception, e.Id);
                    return true;
                case ExtractAudioDataFailedException e:
                    message = TranslationUtility.GetMessage(TranslationTable.cck_audioclip_info_fetch_failed, e.Id);
                    return true;
                default:
                    message = default;
                    return false;
            }
        }

        static void BuildHumanoidAnimation(IItem item)
        {
            var humanoidAnimationList = item.GetComponent<HumanoidAnimationList>();
            if (humanoidAnimationList == null)
            {
                return;
            }

            var animations = humanoidAnimationList.RawHumanoidAnimations;
            if (animations == null)
            {
                return;
            }

            var humanoidAnimations = new Dictionary<AnimationClip, HumanoidAnimation>();

            foreach (var animation in animations)
            {
                var animationClip = animation.Animation;
                if (!humanoidAnimations.TryGetValue(animationClip, out var humanoidAnimation))
                {
                    humanoidAnimation = HumanoidAnimationBuilder.Build(animation.Animation);
                    humanoidAnimations.Add(animationClip, humanoidAnimation);
                }
                animation.SetHumanoidAnimation(humanoidAnimation);
            }
        }

        public async Task<byte[]> BuildZippedItemBinaryAsync(UploadingItem uploadingItem)
        {
            var glbBinary = await uploadingItem.Gltf.ExportAsync();
            var thumbnailBinary = uploadingItem.Thumbnail.EncodeToPNG();

            return itemTemplateBuilder.Build(glbBinary, thumbnailBinary);
        }
    }
}
