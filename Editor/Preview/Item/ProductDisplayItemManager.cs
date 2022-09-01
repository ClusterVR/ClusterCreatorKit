using System.Collections.Generic;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ProductUgc;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class ProductDisplayItemManager
    {
        public ProductDisplayItemManager(ItemCreator itemCreator,
            IEnumerable<IProductDisplayItem> productDisplayItems)
        {
            itemCreator.OnCreate += OnCreate;
            Register(productDisplayItems);
        }

        void Register(IEnumerable<IProductDisplayItem> productDisplayItems)
        {
            foreach (var productDisplayItem in productDisplayItems)
            {
                Register(productDisplayItem);
            }
        }

        void Register(IProductDisplayItem productDisplayItem)
        {
            if (!productDisplayItem.ProductId.IsValid())
            {
                return;
            }
            if (productDisplayItem.NeedsProductSample)
            {
                productDisplayItem.SetSample(CreateProductSample(productDisplayItem.ProductId));
            }
            productDisplayItem.OnInvoked += () => OnInvoked(productDisplayItem);
            productDisplayItem.SetInteractable(true);
        }

        void OnInvoked(IProductDisplayItem productDisplayItem)
        {
            Debug.Log($"商品Idが{productDisplayItem.ProductId.Value}である商品の詳細ページが開かれます");
        }

        void OnCreate(IItem item)
        {
            var productDisplayItem = item.gameObject.GetComponent<IProductDisplayItem>();
            if (productDisplayItem != null)
            {
                Register(productDisplayItem);
            }
        }

        static GameObject CreateProductSample(ProductId productId)
        {
            var rootObject = new GameObject($"ProductSample : {productId.Value}");
            var model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.transform.SetParent(rootObject.transform);
            model.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            rootObject.SetLayerRecursively(LayerName.InteractableItem);
            return rootObject;
        }
    }
}
