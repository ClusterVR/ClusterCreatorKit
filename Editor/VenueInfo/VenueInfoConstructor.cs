using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.VenueInfo
{
    public static class VenueInfoConstructor
    {
        public static string[] FindDisplayItemIds()
        {
            var result = new List<string>();
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();
            foreach (var rootObject in rootObjects)
            {
                var displayItems = rootObject.GetComponentsInChildren<IProductDisplayItem>();
                foreach (var displayItem in displayItems)
                {
                    var id = displayItem.ProductId;
                    if (!string.IsNullOrEmpty(id.Value))
                    {
                        result.Add(id.Value);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
