using System;
using ClusterVR.CreatorKit.ProductUgc;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IProductDisplayItem : IInteractableItem
    {
        ProductId ProductId { get; }
        bool NeedsProductSample { get; }
        event Action OnInvoked;
        void SetSample(GameObject productSample);
        void SetInteractable(bool isInteractable);
    }
}
