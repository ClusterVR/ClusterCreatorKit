using ClusterVR.CreatorKit.Translation;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public sealed class Item : MonoBehaviour, IItem
    {
        [SerializeField, HideInInspector] ItemId id;
        [SerializeField, Tooltip(TranslationTable.cck_item_name)] string itemName;
        [SerializeField, Tooltip(TranslationTable.cck_item_size)] Vector3Int size;

        Transform cachedTransform;
        Transform CachedTransform => cachedTransform ??= transform;
        Vector3? defaultScale;
        Vector3 DefaultScale => defaultScale ??= CachedTransform.localScale;

        GameObject IItem.gameObject => this == null ? null : gameObject;

        IMovableItem movableItem;
        bool isInitialized;

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

        void CacheMovableItem()
        {
            if (isInitialized) return;
            movableItem = GetComponent<IMovableItem>();
            isInitialized = true;
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
