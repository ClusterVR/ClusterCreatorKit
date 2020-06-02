using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public sealed class Item : MonoBehaviour, IItem
    {
        [SerializeField, HideInInspector] ItemId id;
        [SerializeField, Tooltip("アイテムの名前")] string itemName;

        GameObject IItem.gameObject => this == null ? null : gameObject;

        public ItemId Id
        {
            get => id;
            set => id = value;
        }

        public string ItemName => itemName;

        void Reset()
        {
            itemName = "アイテム";
        }
    }
}