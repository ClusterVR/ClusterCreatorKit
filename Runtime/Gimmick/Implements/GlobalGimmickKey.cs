using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [Serializable]
    public class GlobalGimmickKey
    {
        [SerializeField] GimmickKey key = new GimmickKey(GimmickTarget.Global);
        [SerializeField] Item.Implements.Item item;

        public GimmickKey Key => key;
        public ItemId ItemId => item != null ? item.Id : default;
    }
}