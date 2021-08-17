using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class DestroyItemGimmickManager
    {
        readonly ItemDestroyer itemDestroyer;

        public DestroyItemGimmickManager(
            ItemCreator itemCreator,
            ItemDestroyer itemDestroyer,
            IEnumerable<IDestroyItemGimmick> destroyItemGimmicks)
        {
            this.itemDestroyer = itemDestroyer;
            itemCreator.OnCreate += OnCreateItem;
            Register(destroyItemGimmicks);
        }

        void OnCreateItem(IItem item)
        {
            Register(item.gameObject.GetComponents<IDestroyItemGimmick>());
        }

        void Register(IEnumerable<IDestroyItemGimmick> destroyItemGimmicks)
        {
            foreach (var destroyItemGimmick in destroyItemGimmicks)
            {
                destroyItemGimmick.OnDestroyItem += OnInvoked;
            }
        }

        void OnInvoked(DestroyItemEventArgs args)
        {
            if (args.TimestampDiffSeconds > Constants.TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            itemDestroyer.Destroy(args.Item);
        }
    }
}
