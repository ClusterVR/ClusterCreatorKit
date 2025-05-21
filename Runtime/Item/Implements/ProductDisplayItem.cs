using System;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.ProductUgc;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public class ProductDisplayItem : ContactableItem, IProductDisplayItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, Tooltip(TranslationTable.cck_product_id)] ProductId productId;
        [SerializeField, Tooltip(TranslationTable.cck_product_display_position_optional)] Transform productDisplayRoot;
        [SerializeField, Tooltip(TranslationTable.cck_product_display_avatar_facial_expression_tooltip)] ProductDisplayAvatarFacialExpressionType productDisplayAvatarFacialExpressionType;
        [SerializeField, Tooltip(TranslationTable.cck_product_display_avatar_pose_tooltip)] AnimationClip productDisplayAvatarPose;

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
        ProductDisplayAvatarFacialExpressionType IProductDisplayItem.ProductDisplayAvatarFacialExpressionType => productDisplayAvatarFacialExpressionType;
        AnimationClip IProductDisplayItem.ProductDisplayAvatarPose => productDisplayAvatarPose;
        bool IProductDisplayItem.IsValid => productId.IsValid();

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
            if (IsContactable)
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

            if (productDisplayRoot != null && !productDisplayRoot.IsDecendantOrSelf(transform))
            {
                productDisplayRoot = null;
            }
        }
    }
}
