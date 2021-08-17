using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Preview.PlayerController;
using Object = UnityEngine.Object;

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
            Object.Destroy(item.gameObject);
        }
    }
}
