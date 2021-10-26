using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(RidableItem))]
    public sealed class GetOffItemGimmick : MonoBehaviour, IGetOffItemGimmick
    {
        [SerializeField, HideInInspector] RidableItem ridableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);

        ItemId IGimmick.ItemId =>
            (ridableItem != null ? ridableItem.Item : (ridableItem = GetComponent<RidableItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event Action<IRidableItem> OnGetOff;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (ridableItem == null)
            {
                ridableItem = GetComponent<RidableItem>();
            }
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            OnGetOff?.Invoke(ridableItem);
        }

        void OnValidate()
        {
            if (ridableItem == null || ridableItem.gameObject != gameObject)
            {
                ridableItem = GetComponent<RidableItem>();
            }
        }

        void Reset()
        {
            ridableItem = GetComponent<RidableItem>();
        }
    }
}
