using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IItem
    {
        ItemId Id { get; set; }
        string ItemName { get; }

        Vector3Int Size { get; }

        GameObject gameObject { get; }
        ItemTemplateId TemplateId { get; set; }

        Vector3 Position { get; }
        Quaternion Rotation { get; }
        bool IsDestroyed { get; }

        void SetPositionAndRotation(Vector3 position, Quaternion rotation, bool isWarp = false);
        void SetNormalizedScale(Vector3 scale);
        void SetRawScale(Vector3 scale);
        void EnablePhysics();

        void Embody();
        void Disbody();
        void UpdateIsPlaceable(bool isPlaceable);
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
