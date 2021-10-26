using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public sealed class SetVelocityItemGimmick : MonoBehaviour, IItemGimmick
    {
        public static readonly List<ParameterType> SelectableTypes = new List<ParameterType>(2) { ParameterType.Signal, ParameterType.Vector3 };

        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, ParameterTypeField(ParameterType.Signal, ParameterType.Vector3)]
        ParameterType parameterType = SelectableTypes[0];
        [SerializeField] Transform space;
        [SerializeField] Vector3 velocity;
        [SerializeField] float scaleFactor = 1f;

        ItemId IGimmick.ItemId =>
            (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        DateTime lastTriggeredAt;
        Vector3 gimmickValue;
        bool shouldSetVelocity;

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
            if (parameterType == ParameterType.Signal)
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
            }
            else
            {
                gimmickValue = value.Vector3Value;
            }
            shouldSetVelocity = true;
        }

        void FixedUpdate()
        {
            if (!shouldSetVelocity || space == null)
            {
                return;
            }

            if (parameterType == ParameterType.Signal)
            {
                movableItem.SetVelocity(space.TransformDirection(velocity));
                shouldSetVelocity = false;
            }
            else
            {
                movableItem.SetVelocity(space.TransformDirection(gimmickValue * scaleFactor));
            }
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject)
            {
                movableItem = GetComponent<MovableItem>();
            }

            if (!SelectableTypes.Contains(parameterType))
            {
                parameterType = SelectableTypes[0];
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
