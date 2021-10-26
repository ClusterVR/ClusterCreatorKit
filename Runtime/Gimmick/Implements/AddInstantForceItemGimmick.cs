using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public sealed class AddInstantForceItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField] Transform space;
        [SerializeField] Vector3 force;
        [SerializeField] bool ignoreMass;

        ItemId IGimmick.ItemId =>
            (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        ForceMode ForceMode => ignoreMass ? ForceMode.VelocityChange : ForceMode.Impulse;

        DateTime lastTriggeredAt;
        bool shouldAddInstantForce;

        void Start()
        {
            if (movableItem == null)
            {
                movableItem = GetComponent<MovableItem>();
            }
            if (space == null)
            {
                space = transform;
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

            shouldAddInstantForce = true;
        }

        void FixedUpdate()
        {
            if (!shouldAddInstantForce || space == null)
            {
                return;
            }
            movableItem.AddForce(space.TransformDirection(force), ForceMode);
            shouldAddInstantForce = false;
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject)
            {
                movableItem = GetComponent<MovableItem>();
            }
            if (space == null)
            {
                space = transform;
            }
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
        }
    }
}
