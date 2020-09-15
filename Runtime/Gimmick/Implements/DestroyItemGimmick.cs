using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class DestroyItemGimmick : MonoBehaviour, IItemGimmick, IDestroyItemGimmick
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);

        ItemId IGimmick.ItemId => (item != null ? item : item = GetComponent<Item.Implements.Item>()).Id;
        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event DestroyItemEventHandler OnDestroyItem;

        void Start()
        {
            if (item == null) item = GetComponent<Item.Implements.Item>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            OnDestroyItem?.Invoke(new DestroyItemEventArgs {Item = item, TimestampDiffSeconds = (current - value.TimeStamp).TotalSeconds});
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }
    }
}