using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Editor.Preview.Gimmick
{
    public sealed class CreateItemGimmickManager
    {
        readonly ItemCreator itemCreator;

        public CreateItemGimmickManager(
            ItemCreator itemCreator,
            IEnumerable<ICreateItemGimmick> createItemGimmicks)
        {
            this.itemCreator = itemCreator;
            itemCreator.OnCreate += OnCreateItem;
            Register(createItemGimmicks);
        }

        void Register(IEnumerable<ICreateItemGimmick> gimmicks)
        {
            foreach (var gimmick in gimmicks)
            {
                gimmick.OnCreateItem += OnCreateInvoked;
            }
        }

        void OnCreateItem(IItem item)
        {
            Register(item.gameObject.GetComponents<ICreateItemGimmick>());
        }

        void OnCreateInvoked(CreateItemEventArgs args)
        {
            itemCreator.Create(args.TemplateId, args.Position, args.Rotation);
        }
    }
}
