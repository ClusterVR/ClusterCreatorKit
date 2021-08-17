using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class ItemRespawner
    {
        readonly float despawnHeight;
        readonly ItemDestroyer itemDestroyer;
        readonly List<(IMovableItem Item, bool CanRespawn)> movableItems;
        readonly List<IItem> itemsToDestroy = new List<IItem>();

        public ItemRespawner(float despawnHeight,
            ItemCreator itemCreator,
            ItemDestroyer itemDestroyer,
            IEnumerable<IMovableItem> movableItems)
        {
            this.despawnHeight = despawnHeight;
            this.itemDestroyer = itemDestroyer;
            this.movableItems = movableItems.Select(i => (i, true)).ToList();

            itemCreator.OnCreate += OnCreate;
            itemDestroyer.OnDestroy += OnDestroy;

            CheckHeightAsync();
        }

        async void CheckHeightAsync()
        {
            while (Application.isPlaying)
            {
                CheckHeight();
                await Task.Delay(300);
            }
        }

        void CheckHeight()
        {
            itemsToDestroy.Clear();
            foreach (var movableItem in movableItems)
            {
                if (movableItem.Item.Position.y < despawnHeight)
                {
                    if (movableItem.CanRespawn)
                    {
                        movableItem.Item.Respawn();
                    }
                    else
                    {
                        itemsToDestroy.Add(movableItem.Item.Item);
                    }
                }
            }

            foreach (var item in itemsToDestroy)
            {
                itemDestroyer.Destroy(item);
            }
        }

        void OnCreate(IItem item)
        {
            var movableItem = item.gameObject.GetComponent<IMovableItem>();
            if (movableItem != null)
            {
                movableItems.Add((movableItem, false));
            }
        }

        void OnDestroy(IItem item)
        {
            movableItems.RemoveAll(i => i.Item.Item.Id.Equals(item.Id));
        }
    }
}
