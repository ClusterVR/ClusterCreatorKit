using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IItem
    {
        ItemId Id { get; set; }
        string ItemName { get; }
        GameObject gameObject { get; }
    }
}