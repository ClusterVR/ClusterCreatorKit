using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class ProductDisplayItemValidator
    {
        public static bool IsValidAll(GameObject[] sceneRootObjects, out IEnumerable<GameObject> invalidDisplays)
        {
            var invalidDisplaysList = new List<GameObject>();
            foreach (var rootObject in sceneRootObjects)
            {
                var productDisplayItems = rootObject.GetComponentsInChildren<IProductDisplayItem>(true);
                invalidDisplaysList.AddRange(productDisplayItems.Where(item => !IsValid(item)).Select(item => item.Item.gameObject));
            }

            invalidDisplays = invalidDisplaysList;
            return invalidDisplaysList.Count == 0;
        }

        static bool IsValid(IProductDisplayItem productDisplayItem)
        {
            if (string.IsNullOrEmpty(productDisplayItem.ProductId.Value))
            {
                return true;
            }
            return productDisplayItem.IsValid;
        }
    }
}
