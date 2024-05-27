using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.MainScreenViews;
using ClusterVR.CreatorKit.World.Implements.Mirror;
using ClusterVR.CreatorKit.World.Implements.TextView;
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
            typeof(ItemAudioSetList),
            typeof(HumanoidAnimationList),
            typeof(ItemMaterialSetList),
        };

        static readonly Type[] ItemComponentWhiteList =
        {
            typeof(Transform),
            typeof(MeshFilter),
            typeof(MeshRenderer),
            typeof(Collider),
            typeof(Rigidbody), // MovableItemで利用可能
            typeof(StandardMainScreenView),
            typeof(Mirror),
            typeof(PhysicalShape),
            typeof(OverlapSourceShape),
            typeof(OverlapDetectorShape),
            typeof(InteractableShape),
            typeof(ItemSelectShape),
            typeof(TextView),
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
            { typeof(ItemAudioSetList), new[] { typeof(ScriptableItem) } },
            { typeof(HumanoidAnimationList), new[] { typeof(ScriptableItem) } },
            { typeof(ItemMaterialSetList), new[] { typeof(ScriptableItem) } }
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
                    TranslationUtility.GetMessage(TranslationTable.cck_gameobject_scale_positive, gameObject.name, transformScale),
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
                validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_item_component_not_set, gameObject.name),
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_itemname_input_required, gameObject.name),
                    ValidationMessage.MessageType.Error));
            }
            else if (item.ItemName.Length > ItemNameLengthLimit)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_itemname_too_long, gameObject.name, item.ItemName.Length, ItemNameLengthLimit),
                    ValidationMessage.MessageType.Error));
            }


            var size = item.Size;
            if (size.x > itemSizeLimit.x || size.y > itemSizeLimit.y || size.z > itemSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_itemsize_exceeds_limit, gameObject.name, size, itemSizeLimit),
                    ValidationMessage.MessageType.Error));
            }

            if (size.x < 0 || size.y < 0 || size.z < 0)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_itemsize_positive, gameObject.name, size),
                    ValidationMessage.MessageType.Error));
            }

            if (!allowZeroSize && size == Vector3Int.zero)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_itemsize_minimum_value, gameObject.name, size),
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
                            TranslationUtility.GetMessage(TranslationTable.cck_itemsize_visual_mismatch, gameObject.name, size, defaultSize),
                            ValidationMessage.MessageType.Warning));
                    }
                }
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateMovableItem(GameObject gameObject, bool isBeta)
        {
            if (isBeta)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            var movableItem = gameObject.GetComponent<MovableItem>();
            if (movableItem == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            if (movableItem.IsDynamic)
            {
                var message = TranslationTable.cck_beta_feature_physics_warning;
                return new[] { new ValidationMessage(message, ValidationMessage.MessageType.Warning) };
            }
            return Enumerable.Empty<ValidationMessage>();
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
                : new[] { new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_scriptableitem_source_code_length, gameObject.name, scriptableItem.GetByteCount(true), Constants.Constants.ScriptableItemMaxSourceCodeByteCount), ValidationMessage.MessageType.Error) };
        }

        internal static IEnumerable<ValidationMessage> ValidateAttachableItem(GameObject gameObject, Vector3 offsetPositionLimit)
        {
            var validationMessages = new List<ValidationMessage>();

            if (!gameObject.TryGetComponent<IAccessoryItem>(out var accessoryItem))
            {
                validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_accessoryitem_component_not_set, gameObject.name, nameof(AccessoryItem)),
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            var offsetPos = accessoryItem.DefaultAttachOffsetPosition;
            if (offsetPos.x < -(offsetPositionLimit.x) || offsetPos.x > (offsetPositionLimit.x) ||
                offsetPos.y < -(offsetPositionLimit.y) || offsetPos.y > (offsetPositionLimit.y) ||
                offsetPos.z < -(offsetPositionLimit.z) || offsetPos.z > (offsetPositionLimit.z))
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_offset_position_out_of_range, offsetPos.ToString("0.0")) +
                    TranslationUtility.GetMessage(TranslationTable.cck_offset_range_min_max, (offsetPositionLimit.ToString("0.0")), (-offsetPositionLimit).ToString("0.0")),
                    ValidationMessage.MessageType.Error));
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateRenderers(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();

            if (gameObject.GetComponentInChildren<MeshRenderer>(false) == null)
            {
                validationMessages.Add(new ValidationMessage(TranslationTable.cck_mesh_required, ValidationMessage.MessageType.Error));
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
                    TranslationUtility.GetMessage(TranslationTable.cck_component_type_not_supported, component.gameObject.name, componentType),
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            var isChildItemComponent =
                !isRoot && RootComponentWhiteList.Contains(componentType);

            if (isChildItemComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_component_type_root_required, component.gameObject.name, componentType),
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            if (AdditionalRequireComponents.TryGetValue(componentType, out var requireComponents))
            {
                if (!requireComponents.Any(t => component.GetComponent(t) != null))
                {
                    var isSingular = requireComponents.Length == 1;
                    var message = isSingular ?
                        TranslationUtility.GetMessage(TranslationTable.cck_component_type_disabled_require_component, component.gameObject.name, componentType, requireComponents[0]) :
                        TranslationUtility.GetMessage(TranslationTable.cck_component_type_disabled_require_component_list, component.gameObject.name, componentType, string.Join(", ", requireComponents.Select(c => c.ToString())));
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
                    TranslationUtility.GetMessage(TranslationTable.cck_component_gettype_not_supported, component.gameObject.name, component.GetType()),
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            var isChildAccessoryComponent =
                !isRoot && AccessoryRootComponentWhiteList.Contains(component.GetType());

            if (isChildAccessoryComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_component_gettype_root_required, component.gameObject.name, component.GetType()),
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
                            TranslationUtility.GetMessage(TranslationTable.cck_shader_unsupported, material.name, shader.name),
                            ValidationMessage.MessageType.Warning));
                    }
                    else
                    {
                        var supportShaderListStr = string.Join(", ", shaderNameWhiteList);
                        validationMessages.Add(new ValidationMessage(
                            TranslationUtility.GetMessage(TranslationTable.cck_shader_unsupported_list, material.name, shader.name, supportShaderListStr),
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
                    TranslationUtility.GetMessage(TranslationTable.cck_bounds_out_of_recommended_range, name, bounds.max, bounds.min, maxBounds.max, maxBounds.min),
                    ValidationMessage.MessageType.Warning));
            }
        }

        static void ValidateBoundsSize(string name, Vector3 boundsSizeLimit, Bounds bounds, List<ValidationMessage> validationMessages)
        {
            var size = bounds.max - bounds.min;
            if (size.x > boundsSizeLimit.x || size.y > boundsSizeLimit.y || size.z > boundsSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    TranslationUtility.GetMessage(TranslationTable.cck_boundssize_exceeds_limit, name, size, boundsSizeLimit),
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

        public static IEnumerable<ValidationMessage> ValidateMirror(GameObject gameObject, int maxMirrorCount)
        {
            var mirrors = gameObject.GetComponentsInChildren<IMirror>(true);
            var count = mirrors.Length;
            if (count <= maxMirrorCount)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return new List<ValidationMessage>()
            {
                new(TranslationUtility.GetMessage(TranslationTable.cck_too_many_mirrors, count, maxMirrorCount), ValidationMessage.MessageType.Error)
            };
        }

        public static IEnumerable<ValidationMessage> ValidateCollider(GameObject gameObject)
        {
            var colliders = gameObject.GetComponentsInChildren<Collider>(true);
            var validationMessages = new List<ValidationMessage>();
            foreach (var collider in colliders)
            {
                if (collider.GetComponent<IShape>() == null && collider.isTrigger)
                {
                    validationMessages.Add(new ValidationMessage(
                        TranslationUtility.GetMessage(TranslationTable.cck_collider_istrigger_shape_required, collider.name, nameof(OverlapSourceShape), nameof(OverlapDetectorShape), nameof(InteractableShape), nameof(ItemSelectShape)),
                        ValidationMessage.MessageType.Warning));
                }
            }

            var baseShapes = gameObject.GetComponentsInChildren<BaseShape>(true);
            foreach (var shape in baseShapes)
            {
                if (!shape.IsTrigger)
                    continue;

                foreach (var collider in shape.GetComponents<MeshCollider>())
                {
                    if (!collider.isTrigger && !collider.convex)
                    {
                        var message = $"{shape.GetType().Name}でMesh Colliderを利用する際はConvexとis TriggerをTrueにしないと動作しません。";
                        validationMessages.Add(new ValidationMessage(
                            $"Collider\"{collider.name}\" {message}",
                            ValidationMessage.MessageType.Error));
                        break;
                    }
                }
            }

            return validationMessages;
        }

        public static IEnumerable<ValidationMessage> ValidateHumanoidAnimationList(GameObject gameObject)
        {
            var humanoidAnimationList = gameObject.GetComponent<IHumanoidAnimationList>();
            if (humanoidAnimationList == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return HumanoidAnimationListValidator.Validate(humanoidAnimationList);
        }

        public static IEnumerable<ValidationMessage> ValidateTextViews(GameObject gameObject, bool isBeta)
        {
            var textViews = gameObject.GetComponentsInChildren<ITextView>(true);
            if (textViews == null || textViews.Length == 0)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            var validationMessages = new List<ValidationMessage>();
            foreach (var textView in textViews)
            {
                const int maxTextByteCount = 1000;
                var textByteCount = textView.Text != null ? Encoding.UTF8.GetByteCount(textView.Text) : 0;
                if (textByteCount > maxTextByteCount)
                {
                    validationMessages.Add(new(
                        TranslationUtility.GetMessage(TranslationTable.cck_textview_text_size_limit, nameof(World.Implements.TextView), nameof(TextView.Text), textByteCount, maxTextByteCount),
                        ValidationMessage.MessageType.Error));
                }
            }

            return validationMessages;
        }

        public static IEnumerable<ValidationMessage> ValidateItemMaterialSetList(GameObject gameObject, bool isBeta)
        {
            var itemMaterialSetList = gameObject.GetComponent<IItemMaterialSetList>();
            if (itemMaterialSetList == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return ItemMaterialSetListValidator.Validate(isBeta, gameObject, itemMaterialSetList);
        }
    }
}
