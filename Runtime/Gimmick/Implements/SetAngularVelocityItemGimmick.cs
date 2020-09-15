using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public class SetAngularVelocityItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField] Transform space;
        [SerializeField] Vector3 angularVelocity;

        ItemId IGimmick.ItemId => (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;
        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        DateTime lastTriggeredAt;
        bool shouldSetAngularVelocity;

        void Start()
        {
            if (movableItem == null) movableItem = GetComponent<MovableItem>();
            if (space == null) space = transform;
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;

            shouldSetAngularVelocity = true;
        }

        void FixedUpdate()
        {
            if (!shouldSetAngularVelocity || space == null) return;
            movableItem.SetAngularVelocity(space.TransformDirection(angularVelocity));
            shouldSetAngularVelocity = false;
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject) movableItem = GetComponent<MovableItem>();
            if (space == null) space = transform;
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
        }
    }
}