using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public sealed class Item : MonoBehaviour, IItem
    {
        [SerializeField, HideInInspector] ItemId id;
        [SerializeField, Tooltip("アイテムの名前")] string itemName;
        [SerializeField, Tooltip("アイテムの大きさ")] Vector3Int size;

        const float DisbodiedAlpha = 0.5f;
        static readonly Color PlaceableColorMask = Color.green;
        static readonly Color UnplaceableColorMask = Color.red;

        static readonly int ModeId = Shader.PropertyToID("_Mode");
        const float OpaqueModeValue = 0f;
        const float TransparentModeValue = 3f;
        static readonly int SrcBlendId = Shader.PropertyToID("_SrcBlend");
        static readonly int DstBlendId = Shader.PropertyToID("_DstBlend");
        static readonly int ZWriteId = Shader.PropertyToID("_ZWrite");

        enum ManipulationState
        {
            NotSet, Placeable, Unplaceable,
        }

        ManipulationState manipulationState;

        Transform cachedTransform;
        Transform CachedTransform => cachedTransform ??= transform;
        Vector3? defaultScale;
        Vector3 DefaultScale => defaultScale ??= CachedTransform.localScale;

        GameObject IItem.gameObject => this == null ? null : gameObject;

        IMovableItem movableItem;
        bool isInitialized;

        bool disbodied;

        readonly List<(Material, Color)> instancedMaterialAndBaseColors = new List<(Material, Color)>();
        readonly Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

        public void Construct(string itemName, Vector3Int size)
        {
            this.itemName = itemName;
            this.size = size;
        }

        public ItemId Id
        {
            get => id;
            set => id = value;
        }

        string IItem.ItemName => itemName;
        Vector3Int IItem.Size => size;

        ItemTemplateId IItem.TemplateId { get; set; }

        Vector3 IItem.Position
        {
            get
            {
                CacheMovableItem();
                if (movableItem != null)
                {
                    return movableItem.Position;
                }
                else
                {
                    return CachedTransform.position;
                }
            }
        }

        Quaternion IItem.Rotation
        {
            get
            {
                CacheMovableItem();
                if (movableItem != null)
                {
                    return movableItem.Rotation;
                }
                else
                {
                    return CachedTransform.rotation;
                }
            }
        }

        bool IItem.IsDestroyed => this == null;

        void IItem.SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp)
        {
            CacheMovableItem();
            if (movableItem != null)
            {
                movableItem.SetPositionAndRotation(position, rotation, isWarp);
            }
            else
            {
                CachedTransform.SetPositionAndRotation(position, rotation);
            }
        }

        void IItem.SetRawScale(Vector3 scale)
        {
            CachedTransform.localScale = scale;
        }

        void IItem.SetNormalizedScale(Vector3 scale)
        {
            CachedTransform.localScale = Vector3.Scale(scale, DefaultScale);
        }

        void IItem.EnablePhysics()
        {
            CacheMovableItem();
            if (movableItem != null)
            {
                movableItem.EnablePhysics();
            }
        }

        void IItem.Embody()
        {
            if (!disbodied) return;
            foreach (var collider in gameObject.GetComponentsInChildren<Collider>(true))
            {
                collider.enabled = true;
            }

            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                if (originalMaterials.TryGetValue(renderer, out var original))
                {
                    renderer.materials = original;
                }
            }

            ReleaseMaterials();
            disbodied = false;
            manipulationState = ManipulationState.NotSet;
        }

        void IItem.Disbody()
        {
            if (disbodied) return;
            foreach (var collider in gameObject.GetComponentsInChildren<Collider>(true))
            {
                collider.enabled = false;
            }

            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                originalMaterials[renderer] = renderer.sharedMaterials;
                instancedMaterialAndBaseColors.AddRange(renderer.materials.Select(m => (m, GetBaseColor(m))));
            }

            foreach (var (material, baseColor) in instancedMaterialAndBaseColors)
            {
                if (!IsMode(material, TransparentModeValue))
                {
                    SetTransparent(material);
                }
                var color = baseColor;
                color.a = color.a * DisbodiedAlpha;
                material.color = color;
            }
            disbodied = true;
        }

        static Color GetBaseColor(Material material)
        {
            var isOpaque = IsMode(material, OpaqueModeValue);
            var color = material.color;
            if (isOpaque)
            {
                color.a = 1f;
            }
            return color;
        }

        static bool IsMode(Material material, float value)
        {
            return Mathf.Approximately(material.GetFloat(ModeId), value);
        }

        void IItem.UpdateIsPlaceable(bool isPlaceable)
        {
            if (!disbodied) return;
            var state = isPlaceable ? ManipulationState.Placeable : ManipulationState.Unplaceable;
            if (state == manipulationState) return;
            var maskColor = isPlaceable ? PlaceableColorMask : UnplaceableColorMask;
            foreach (var (material, baseColor) in instancedMaterialAndBaseColors)
            {
                SetRGBMask(material, baseColor, maskColor); // アルファはDisbody/EmbodyでやっているのでMaskでは影響させない
            }
            manipulationState = state;
        }

        void SetTransparent(Material material)
        {
            material.SetFloat(ModeId, TransparentModeValue);
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt(SrcBlendId, (int) BlendMode.One);
            material.SetInt(DstBlendId, (int) BlendMode.OneMinusSrcAlpha);
            material.SetInt(ZWriteId, 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int) RenderQueue.Transparent;
        }

        static void SetRGBMask(Material material, Color baseColor, Color maskColor)
        {
            var color = baseColor * maskColor;
            color.a = material.color.a;
            material.color = color;
        }

        void CacheMovableItem()
        {
            if (isInitialized) return;
            movableItem = GetComponent<IMovableItem>();
            isInitialized = true;
        }

        void OnDestroy()
        {
            ReleaseMaterials();
        }

        void ReleaseMaterials()
        {
            foreach (var (material, _) in instancedMaterialAndBaseColors)
            {
                Destroy(material);
            }
            instancedMaterialAndBaseColors.Clear();
            originalMaterials.Clear();
        }

        void OnDrawGizmosSelected()
        {
            var localPosition = Vector3.up * size.y * 0.5f;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.color = new Color(1, 1, 0, 0.8f);
            Gizmos.DrawCube(localPosition, size);
        }
    }
}
