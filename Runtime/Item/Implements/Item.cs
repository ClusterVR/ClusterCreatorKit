using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public sealed class Item : MonoBehaviour, IItem
    {
        [HideInInspector] public ItemId id;
        [SerializeField, Tooltip("アイテムの名前")] string itemName;
        public ItemId Id => id;
        public string ItemName => itemName;

        void Reset()
        {
            itemName = "アイテム";
        }
    }
}