using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [Serializable]
    public sealed class PlayerGimmickKey
    {
        [SerializeField] GimmickTarget target;
        [SerializeField, StateKeyString] string key;
        [SerializeField, GimmickKeyItem] Item.Implements.Item item;

        public GimmickKey Key => new GimmickKey(target, key);
        public ItemId ItemId => item != null ? item.Id : default;

        public PlayerGimmickKey()
            : this(default)
        {
        }

        public PlayerGimmickKey(string key)
        {
            target = GimmickTarget.Player;
            this.key = key;
        }
    }
}
