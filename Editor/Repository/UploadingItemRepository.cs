using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class UploadingItemRepository
    {
        public static readonly UploadingItemRepository CraftItems = new();
        public static readonly UploadingItemRepository Accessories = new();

        readonly Reactive<List<UploadingItem>> items = new(new());
        public IReadOnlyReactive<IReadOnlyList<UploadingItem>> Items => items;

        private UploadingItemRepository() { }

        public void Clear()
        {
            foreach (var item in items.Val)
            {
                item.Dispose();
            }
            items.Val.Clear();
            items.Val = items.Val;
        }

        public void Remove(UploadingItem uploadingItem)
        {
            uploadingItem.Dispose();
            items.Val = items.Val.Where(item => item != uploadingItem).ToList();
        }

        public void AddOrUpdateItems(IEnumerable<UploadingItem> uploadingItems)
        {
            foreach (var uploadingItem in uploadingItems)
            {
                var existingItemIndex =
                    items.Val.FindIndex(item =>
                        item.Item != null && item.Item.GetInstanceID() == uploadingItem.Item.GetInstanceID());
                if (existingItemIndex > -1)
                {
                    items.Val[existingItemIndex] = uploadingItem;
                }
                else
                {
                    items.Val.Insert(0, uploadingItem);
                }
            }
            items.Val = items.Val;
        }
    }
}
