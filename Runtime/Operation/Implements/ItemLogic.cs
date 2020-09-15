using System;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public class ItemLogic : MonoBehaviour, IItemLogic
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, ItemLogic] Logic logic;

        IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        ItemId IGimmick.ItemId => Item.Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event RunItemLogicEventHandler OnRunItemLogic;

        DateTime lastTriggeredAt;
        bool validated;
        bool isValid;

        public void Run(GimmickValue value, DateTime current)
        {
            if (!validated) Validate();
            if (!isValid) return;
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
            OnRunItemLogic?.Invoke(this, new RunItemLogicEventArgs(logic));
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
        }

        void Validate()
        {
            isValid = logic != null && logic.IsValid();
            validated = true;
        }
    }
}