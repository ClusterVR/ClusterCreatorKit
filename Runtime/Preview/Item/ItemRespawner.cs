using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class ItemRespawner
    {
        readonly float despawnHeight;
        readonly List<IMovableItem> movableItems;

        public ItemRespawner (float despawnHeight, IEnumerable<IMovableItem> movableItems)
        {
            this.despawnHeight = despawnHeight;
            this.movableItems = movableItems.ToList();
            CheckHeight();
        }

        async void CheckHeight()
        {
            while (Application.isPlaying)
            {
                foreach (var movableItem in movableItems)
                {
                    if (movableItem.Position.y < despawnHeight) movableItem.Respawn();
                }
                await Task.Delay(300);
            }
        }
    }
}
