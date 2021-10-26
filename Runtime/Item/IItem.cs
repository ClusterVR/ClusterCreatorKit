using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IItem
    {
        ItemId Id { get; set; }
        string ItemName { get; }
        GameObject gameObject { get; }
    }

    public static class IItemExtensions
    {
        public static T GetComponent<T>(this IItem item)
        {
            return item.gameObject != null
                ? item.gameObject.GetComponent<T>()
                : default;
        }
    }
}
