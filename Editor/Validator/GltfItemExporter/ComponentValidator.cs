using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.World.Implements.MainScreenViews;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class ComponentValidator
    {
        const int ItemNameLengthLimit = 64;

        static readonly Vector3 BoundsSizeLimit = new Vector3(5, 5, 5);
        static readonly Vector3 BoundsCenterLimit = new Vector3(0, 2, 0);
        static readonly Bounds MaxBounds = new Bounds(BoundsCenterLimit, BoundsSizeLimit);

        static readonly Vector3Int ItemSizeLimit = new Vector3Int(4, 4, 4);

        static readonly string[] ShaderNameWhiteList =
        {
            "Standard",
            "Unlit/Texture",
            "ClusterVR/InternalSDK/MainScreen",
            "ClusterVR/UnlitNonTiledWithBackgroundColor"
        };

        static readonly Type[] ItemComponentWhiteList =
        {
            typeof(Item.Implements.Item),
            typeof(GrabbableItem),
            typeof(MovableItem),
            typeof(RidableItem),
            typeof(ScriptableItem)
        };

        static readonly Type[] BehaviourWhiteList =
        {
            typeof(StandardMainScreenView),
        };

        public static IEnumerable<ValidationMessage> Validate(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();
            validationMessages.AddRange(ValidateItem(gameObject));
            validationMessages.AddRange(ValidateScriptableItem(gameObject));

            foreach (var behaviour in gameObject.GetComponentsInChildren<Behaviour>(true))
            {
                var isRoot = behaviour.gameObject == gameObject;
                validationMessages.AddRange(ValidateBehaviour(behaviour, isRoot));
            }

            validationMessages.AddRange(ValidateBounds(gameObject));
            validationMessages.AddRange(ValidateShader(gameObject));

            return validationMessages;
        }

        static IEnumerable<ValidationMessage> ValidateItem(GameObject gameObject)
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
            if (size.x > ItemSizeLimit.x || size.y > ItemSizeLimit.y || size.z > ItemSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeが規定値以上です。現在：{size}, 規定値：{ItemSizeLimit}",
                    ValidationMessage.MessageType.Error));
            }
            if (size.x < 0 || size.y < 0 || size.z < 0)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeは0以上の値を入力してください。現在：{size}",
                    ValidationMessage.MessageType.Error));
            }
            if (size == Vector3Int.zero)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeの少なくとも1つの値は1以上にしてください。現在：{size}",
                    ValidationMessage.MessageType.Error));
            }

            var bounds = CalcLocalBounds(item.gameObject);
            var boundSize = bounds.size;
            var sizeDiff = size - boundSize;
            const float sizeTolerance = 1f;

            if (Mathf.Abs(sizeDiff.x) >= sizeTolerance || Mathf.Abs(sizeDiff.y) >= sizeTolerance || Mathf.Abs(sizeDiff.z) >= sizeTolerance)
            {
                var defaultSize = new Vector3Int(Mathf.RoundToInt(boundSize.x), Mathf.RoundToInt(boundSize.y), Mathf.RoundToInt(boundSize.z));
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}のItemSizeが見た目の大きさと大きく異なります。現在：{size}, 自動計算値：{defaultSize}",
                    ValidationMessage.MessageType.Warning));
            }

            return validationMessages;
        }

        static IEnumerable<ValidationMessage> ValidateScriptableItem(GameObject gameObject)
        {
            var scriptableItem = gameObject.GetComponent<ScriptableItem>();
            if (scriptableItem == null)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return scriptableItem.IsValid()
                ? Enumerable.Empty<ValidationMessage>()
                : new[] { new ValidationMessage($"{gameObject.name}のScriptableItemのsource codeが長すぎます｡現在：{scriptableItem.GetByteCount()}bytes, 最大値：{Constants.Constants.ScriptableItemMaxSourceCodeByteCount}bytes", ValidationMessage.MessageType.Error) };
        }

        static IEnumerable<ValidationMessage> ValidateBehaviour(Behaviour behaviour, bool isRoot)
        {
            var validationMessages = new List<ValidationMessage>();

            var isInvalidComponent =
                !BehaviourWhiteList.Contains(behaviour.GetType()) &&
                !ItemComponentWhiteList.Contains(behaviour.GetType());

            if (isInvalidComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{behaviour.gameObject.name}の{behaviour.GetType()}は対応していないため正しく動作しません。",
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            var isChildItemComponent =
                !isRoot && ItemComponentWhiteList.Contains(behaviour.GetType());

            if (isChildItemComponent)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{behaviour.gameObject.name}の{behaviour.GetType()}は無効化されます。RootのGameObjectに設定してください。",
                    ValidationMessage.MessageType.Warning));
                return validationMessages;
            }

            var attributes = Attribute.GetCustomAttributes(behaviour.GetType(), typeof(RequireComponent))
                .Cast<RequireComponent>();
            foreach (var requireComponent in attributes)
            {
                validationMessages.AddRange(ValidateRequireComponent(behaviour, requireComponent.m_Type0));
                validationMessages.AddRange(ValidateRequireComponent(behaviour, requireComponent.m_Type1));
                validationMessages.AddRange(ValidateRequireComponent(behaviour, requireComponent.m_Type2));
            }
            return validationMessages;
        }

        static IEnumerable<ValidationMessage> ValidateRequireComponent(Component component, Type requireType)
        {
            var validationMessages = new List<ValidationMessage>();
            if (requireType == null)
            {
                return validationMessages;
            }

            var gameObject = component.gameObject;
            if (!gameObject.TryGetComponent(requireType, out _))
            {
                validationMessages.Add(new ValidationMessage(
                    $"{gameObject.name}の{component.GetType()}に必要なコンポーネント{requireType}が設定されていません。",
                    ValidationMessage.MessageType.Error));
            }

            return validationMessages;
        }

        static IEnumerable<ValidationMessage> ValidateShader(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();

            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                foreach (var material in renderer.sharedMaterials)
                {
                    var shader = material.shader;
                    if (ShaderNameWhiteList.Contains(shader.name))
                    {
                        continue;
                    }
                    validationMessages.Add(new ValidationMessage(
                        $"マテリアル\"{material.name}\"のShader \"{shader.name}\" は未対応です。Standard Shaderに変換されます。",
                        ValidationMessage.MessageType.Warning));
                }
            }
            return validationMessages;
        }

        static IEnumerable<ValidationMessage> ValidateBounds(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();

            var bounds = CalcLocalBounds(gameObject);
            if (!MaxBounds.Contains(bounds.max) || !MaxBounds.Contains(bounds.min))
            {
                validationMessages.Add(new ValidationMessage(
                    $"RendererのBoundsが推奨範囲外です。現在: max: {bounds.max},min: {bounds.min}, 推奨: max: {MaxBounds.max},min {MaxBounds.min}",
                    ValidationMessage.MessageType.Warning));
            }
            var size = bounds.max - bounds.min;
            if (size.x > BoundsSizeLimit.x || size.y > BoundsSizeLimit.y || size.z > BoundsSizeLimit.z)
            {
                validationMessages.Add(new ValidationMessage(
                    $"RendererのBoundsSizeが規定値以上です。現在：{size}, 規定値：{BoundsSizeLimit}",
                    ValidationMessage.MessageType.Error));
            }
            return validationMessages;
        }

        static Bounds CalcLocalBounds(GameObject gameObject)
        {
            var previewScene = EditorSceneManager.NewPreviewScene();
            try
            {
                var go = Object.Instantiate(gameObject);
                SceneManager.MoveGameObjectToScene(go, previewScene);
                go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                var bounds = go.GetComponentsInChildren<Renderer>(true)
                    .Select(r => r.bounds)
                    .Aggregate((result, b) =>
                    {
                        result.Encapsulate(b);
                        return result;
                    });
                return bounds;
            }
            finally
            {
                EditorSceneManager.ClosePreviewScene(previewScene);
            }
        }
    }
}
