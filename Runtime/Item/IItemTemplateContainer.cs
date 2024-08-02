using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public readonly struct ItemTemplateIdAndItem
    {
        public readonly ItemTemplateId Id;
        public readonly IItem Item;

        public ItemTemplateIdAndItem(ItemTemplateId id, IItem item)
        {
            Id = id;
            Item = item;
        }
    }

    public interface IItemTemplateContainer
    {
        IEnumerable<ItemTemplateIdAndItem> ItemTemplates();

#if UNITY_EDITOR
        void SetItemTemplateId(IItem item, ItemTemplateId id);

        void MarkObjectDirty();
#endif
    }
}
