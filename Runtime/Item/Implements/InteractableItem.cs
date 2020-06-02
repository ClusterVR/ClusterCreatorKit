using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public abstract class InteractableItem : MonoBehaviour, IInteractableItem
    {
        public abstract IItem Item { get; }
    }
}