using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public abstract class ItemGroupBase : MonoBehaviour
    {
        Item item;

        public IItem Item
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
    }
}
