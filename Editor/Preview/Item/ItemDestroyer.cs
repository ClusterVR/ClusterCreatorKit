using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using ClusterVR.CreatorKit.Preview.PlayerController;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class ItemDestroyer
    {
        readonly IItemController itemController;
        public event Action<IItem> OnDestroy;

        public ItemDestroyer(IItemController itemController)
        {
            this.itemController = itemController;
        }

        public void Destroy(IItem item)
        {
            OnDestroy?.Invoke(item);
            itemController?.OnDestroyItem(item);
            GameObject.Destroy(item.gameObject);
        }
    }
}
