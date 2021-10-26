using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public abstract class ContactableItem : MonoBehaviour, IContactableItem
    {
        public abstract IItem Item { get; }
    }
}
