using System;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.ProductUgc;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class ProductDisplayItem : ContactableItem, IProductDisplayItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, Tooltip("商品Id")] ProductId productId;
        [SerializeField, Tooltip("商品を表示する位置（任意）")] Transform productDisplayRoot;

        public override IItem Item
        {
            get
            {
                if (item != null)
                {
                    return item;
                }
                if (this == null)
                {
                    return null;
                }
                return item = GetComponent<Item>();
            }
        }

        bool isInteractable;
        public override bool IsContactable => isInteractable;
        public override bool RequireOwnership => false;
        ProductId IProductDisplayItem.ProductId => productId;
        bool IProductDisplayItem.NeedsProductSample => productDisplayRoot != null;
        public event Action OnInvoked;
        public Transform ProductDisplayRoot => productDisplayRoot;

        void IProductDisplayItem.SetInteractable(bool isInteractable)
        {
            this.isInteractable = isInteractable;
        }

        void IProductDisplayItem.SetSample(GameObject productSample)
        {
            productSample.transform.SetParent(productDisplayRoot, false);
        }

        void IInteractableItem.Invoke()
        {
            if (isInteractable)
            {
                OnInvoked?.Invoke();
            }
        }

        void Reset()
        {
            item = GetComponent<Item>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject)
            {
                item = GetComponent<Item>();
            }

            if (productDisplayRoot != null && !IsSiblingOrSelf(productDisplayRoot, transform))
            {
                productDisplayRoot = null;
            }
        }

        static bool IsSiblingOrSelf(Transform target, Transform mayParent)
        {
            if (target == mayParent) return true;
            var parent = target.parent;
            if (parent == null) return false;
            return IsSiblingOrSelf(parent, mayParent);
        }
    }
}
