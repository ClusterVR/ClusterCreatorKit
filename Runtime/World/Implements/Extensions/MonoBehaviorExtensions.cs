using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements
{
    public static class MonoBehaviorExtensions
    {
        public static void DisableRichText(this MonoBehaviour mono)
        {
            foreach (var text in mono.GetComponentsInChildren<Text>())
            {
                text.supportRichText = false;
            }
        }

        public static void DisableImageRayCastTarget(this MonoBehaviour mono)
        {
            foreach (var graphic in mono.GetComponentsInChildren<Graphic>())
            {
                graphic.raycastTarget = false;
            }
        }
    }
}
