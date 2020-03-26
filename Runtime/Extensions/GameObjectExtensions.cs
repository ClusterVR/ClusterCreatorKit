using UnityEngine;

namespace ClusterVR.CreatorKit.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform t in gameObject.transform)
            {
                SetLayerRecursively(t.gameObject, layer);
            }
        }
    }
}
