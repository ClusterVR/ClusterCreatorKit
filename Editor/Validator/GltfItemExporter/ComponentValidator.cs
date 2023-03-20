using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.World.Implements.MainScreenViews;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class ComponentValidator
    {
        const int ItemNameLengthLimit = 64;

        static readonly Type[] RootComponentWhiteList =
        {
            typeof(Item.Implements.Item),
            typeof(GrabbableItem),
            typeof(MovableItem),
            typeof(RidableItem),
            typeof(ScriptableItem),
            typeof(ItemAudioSetList)
        };

        static readonly Type[] ItemComponentWhiteList =
        {
            typeof(Transform),
            typeof(MeshFilter),
            typeof(MeshRenderer),
            typeof(Collider),
            typeof(Rigidbody), // MovableItemで利用可能
            typeof(StandardMainScreenView)
        };

        static readonly Type[] AccessoryRootComponentWhiteList =
        {
            typeof(Item.Implements.Item),
            typeof(AccessoryItem)
        };

        static readonly Type[] AccessoryComponentWhiteList =
        {
            typeof(Transform),
            typeof(MeshFilter),
            typeof(MeshRenderer),
        };

        static readonly Dictionary<Type, Type[]> AdditionalRequireComponents = new()
        {
            { typeof(ItemAudioSetList), new[] { typeof(ScriptableItem) } }
        };

        static bool Contains(IEnumerable<Type> list, Type target)
        {
            return list.Any(typeInList => (target.IsSubclassOf(typeInList) || target == typeInList));
        }

        internal static IEnumerable<ValidationMessage> ValidateTransform(GameObject gameObject)
        {
            var transformScale = gameObject.transform.localScale;
            if (transformScale.x <= 0 || transformScale.y <= 0 || transformScale.z <= 0)
            {
                return new[] { new ValidationMessage(
                    $"{gameObject.name}のScaleは0より大きい値を入力してください。現在：{transformScale}",
                    ValidationMessage.MessageType.Error) };
            }

            return Enumerable.Empty<ValidationMessage>();
        }

        internal static IEnumerable<ValidationMessage> ValidateItem(GameObject gameObject, Vector3 itemSizeLimit, bool allowZeroSize, bool checkBoundSizeGap)
        {
            var validationMessages = new List<ValidationMessage>();

            var item = gameObject.GetComponent<IItem>();
            if (item == null)
            {
                validationMessages.Add(new ValidationMessage($"{gameObject.name}にItemコンポーネントが設定されていません。",
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                validationMessages.Add(new ValidationMessage($"{gameObject.name}のItemNameを入力してください。",
                    ValidationMessage.MessageType.Error));
            }
            else if (item.ItemName.Length > ItemNameLengthLimit)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemNameが長すぎます。現在値：{item.ItemName.Length}, 最大値：{ItemNameLengthLimit}",
                    ValidationMessage.MessageType.Error));
            }


            var size = item.Size;
            if (size.x > itemSizeLimit.x || size.y > itemSizeLimit.y || size.z > itemSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeが規定値以上です。現在：{size}, 規定値：{itemSizeLimit}",
                    ValidationMessage.MessageType.Error));
            }

            if (size.x < 0 || size.y < 0 || size.z < 0)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeは0以上の値を入力してください。現在：{size}",
                    ValidationMessage.MessageType.Error));
            }

            if (!allowZeroSize && size == Vector3Int.zero)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeの少なくとも1つの値は1以上にしてください。現在：{size}",
                    ValidationMessage.MessageType.Error));
            }

            if (checkBoundSizeGap)
            {
                BoundsCalculator.CalcLocalBounds(item.gameObject, out var bounds, out _);
                if (bounds.HasValue)
                {
                    var boundSize = bounds.Value.size;
                    var sizeDiff = size - boundSize;
                    const float sizeTolerance = 1f;

                    if (Mathf.Abs(sizeDiff.x) >= sizeTolerance || Mathf.Abs(sizeDiff.y) >= sizeTolerance || Mathf.Abs(sizeDiff.z) >= sizeTolerance)
                    {
                        var defaultSize = new Vector3Int(Mathf.RoundToInt(boundSize.x), Mathf.RoundToInt(boundSize.y), Mathf.RoundToInt(boundSize.z));
                        validationMessages.Add(new ValidationMessage(
                            $"{gameObject.name}のItemSizeが見た目の大きさと大きく異なります。現在：{size}, 自動計算値：{defaultSize}",
                            ValidationMessage.MessageType.Warning));
                    }
                }
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateScriptableItem(GameObject gameObject)
        {
            var scriptableItem = gameObject.GetComponent<ScriptableItem>();
            if (scriptableItem == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return scriptableItem.IsValid(true)
                ? Enumerable.Empty<ValidationMessage>()
                : new[] { new ValidationMessage($"{gameObject.name}のScriptableItemのsource codeが長すぎます｡現在：{scriptableItem.GetByteCount(true)}bytes, 最大値：{Constants.Constants.ScriptableItemMaxSourceCodeByteCount}bytes", ValidationMessage.MessageType.Error) };
        }

        internal static IEnumerable<ValidationMessage> ValidateAttachableItem(GameObject gameObject, Vector3 offsetPositionLimit)
        {
            var validationMessages = new List<ValidationMessage>();

            if (!gameObject.TryGetComponent<IAccessoryItem>(out var accessoryItem))
            {
                validationMessages.Add(new ValidationMessage($"{gameObject.name}に{nameof(AccessoryItem)}コンポーネントが設定されていません。",
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            var offsetPos = accessoryItem.DefaultAttachOffsetPosition;
            if (offsetPos.x < -(offsetPositionLimit.x) || offsetPos.x > (offsetPositionLimit.x) ||
                offsetPos.y < -(offsetPositionLimit.y) || offsetPos.y > (offsetPositionLimit.y) ||
                offsetPos.z < -(offsetPositionLimit.z) || offsetPos.z > (offsetPositionLimit.z))
            {
                validationMessages.Add(new ValidationMessage(
                    $"Offset Positionが規定範囲外です。{offsetPos.ToString("0.0")}、" +
                    $"規定範囲: max: {(offsetPositionLimit.ToString("0.0"))},min: {(-offsetPositionLimit).ToString("0.0")}",
                    ValidationMessage.MessageType.Error));
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateRenderers(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();

            if (gameObject.GetComponentInChildren<MeshRenderer>(false) == null)
            {
                validationMessages.Add(new ValidationMessage("少なくとも1つのMeshが有効である必要があります", ValidationMessage.MessageType.Error));
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateComponent(Component component, bool isRoot)
        {
            var validationMessages = new List<ValidationMessage>();
            var componentType = component.GetType();

            var isInvalidComponent =
                !Contains(ItemComponentWhiteList, componentType) &&
                !RootComponentWhiteList.Contains(componentType);

            if (isInvalidComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{component.gameObject.name}の{componentType}は対応していないため正しく動作しません。",
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            var isChildItemComponent =
                !isRoot && RootComponentWhiteList.Contains(componentType);

            if (isChildItemComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{component.gameObject.name}の{componentType}は無効化されます。RootのGameObjectに設定してください。",
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            if (AdditionalRequireComponents.TryGetValue(componentType, out var requireComponents))
            {
                if (!requireComponents.Any(t => component.GetComponent(t) != null))
                {
                    var isSingular = requireComponents.Length == 1;
                    var message = isSingular ?
                        $"{component.gameObject.name}の{componentType}は無効化されます。{requireComponents[0]}をGameObjectに設定してください。" :
                        $"{component.gameObject.name}の{componentType}は無効化されます。{string.Join(", ", requireComponents.Select(c => c.ToString()))}のいずれかをGameObjectに設定してください。";
                    validationMessages.Add(new ValidationMessage(message, ValidationMessage.MessageType.Warning));
                    return validationMessages;
                }
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateAccessoryComponent(Component component, bool isRoot)
        {
            var validationMessages = new List<ValidationMessage>();

            var validComponentList = AccessoryRootComponentWhiteList.Concat(AccessoryComponentWhiteList);
            var isInvalidComponent = !validComponentList.Contains(component.GetType());

            if (isInvalidComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{component.gameObject.name}の{component.GetType()}は対応していません。",
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            var isChildAccessoryComponent =
                !isRoot && AccessoryRootComponentWhiteList.Contains(component.GetType());

            if (isChildAccessoryComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{component.gameObject.name}の{component.GetType()}は無効化されます。RootのGameObjectに設定してください。",
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateShader(GameObject gameObject, string[] shaderNameWhiteList, bool fallbackToStandard)
        {
            var validationMessages = new List<ValidationMessage>();

            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                foreach (var material in renderer.sharedMaterials)
                {
                    var shader = material.shader;
                    if (shaderNameWhiteList.Contains(shader.name))
                    {
                        continue;
                    }
                    if (fallbackToStandard)
                    {
                        validationMessages.Add(new ValidationMessage(
                            $"マテリアル\"{material.name}\"のShader \"{shader.name}\" は未対応です。Standard Shaderに変換されます。",
                            ValidationMessage.MessageType.Warning));
                    }
                    else
                    {
                        var supportShaderListStr = string.Join(", ", shaderNameWhiteList);
                        validationMessages.Add(new ValidationMessage(
                            $"マテリアル\"{material.name}\"のShader \"{shader.name}\" は未対応です。対応Shader: {supportShaderListStr}",
                            ValidationMessage.MessageType.Error));
                    }
                }
            }
            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateBounds(GameObject gameObject, Vector3 boundsCenterLimit, Vector3 boundsSizeLimit)
        {
            var maxBounds = new Bounds(boundsCenterLimit, boundsSizeLimit);

            var validationMessages = new List<ValidationMessage>();
            BoundsCalculator.CalcLocalBounds(gameObject, out var rendererBounds, out var colliderBounds);
            if (rendererBounds.HasValue)
            {
                CheckBoundsRecommendation("Renderer", maxBounds, rendererBounds.Value, validationMessages);
                ValidateBoundsSize("Renderer", boundsSizeLimit, rendererBounds.Value, validationMessages);
            }
            if (colliderBounds.HasValue)
            {
                CheckBoundsRecommendation("Collider", maxBounds, colliderBounds.Value, validationMessages);
                ValidateBoundsSize("Collider", boundsSizeLimit, colliderBounds.Value, validationMessages);
            }
            return validationMessages;
        }

        static void CheckBoundsRecommendation(string name, Bounds maxBounds, Bounds bounds, List<ValidationMessage> validationMessages)
        {
            if (!maxBounds.Contains(bounds.max) || !maxBounds.Contains(bounds.min))
            {
                validationMessages.Add(new ValidationMessage(
                    $"{name}のBoundsが推奨範囲外です。現在: max: {bounds.max},min: {bounds.min}, 推奨: max: {maxBounds.max},min {maxBounds.min}",
                    ValidationMessage.MessageType.Warning));
            }
        }

        static void ValidateBoundsSize(string name, Vector3 boundsSizeLimit, Bounds bounds, List<ValidationMessage> validationMessages)
        {
            var size = bounds.max - bounds.min;
            if (size.x > boundsSizeLimit.x || size.y > boundsSizeLimit.y || size.z > boundsSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{name}のBoundsSizeが規定値以上です。現在：{size}, 規定値：{boundsSizeLimit}",
                    ValidationMessage.MessageType.Error));
            }
        }

        public static IEnumerable<ValidationMessage> ValidateItemAudioSetList(GameObject gameObject)
        {
            var itemAudioSetList = gameObject.GetComponent<IItemAudioSetList>();
            if (itemAudioSetList == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return ItemAudioSetListValidator.Validate(itemAudioSetList);
        }
    }
}
