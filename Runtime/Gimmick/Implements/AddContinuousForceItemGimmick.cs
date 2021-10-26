using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public sealed class AddContinuousForceItemGimmick : MonoBehaviour, IItemGimmick
    {
        public static readonly List<ParameterType> SelectableTypes = new List<ParameterType>(4)
            { ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector3 };

        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);

        [SerializeField, ParameterTypeField(ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector3)]
        ParameterType parameterType = SelectableTypes[0];

        [SerializeField] Transform space;
        [SerializeField] Vector3 force;
        [SerializeField] float scaleFactor = 1f;
        [SerializeField] bool ignoreMass;

        ItemId IGimmick.ItemId =>
            (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        ForceMode ForceMode => ignoreMass ? ForceMode.Acceleration : ForceMode.Force;

        float currentPower;
        Vector3 currentVector;

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

        public void Run(GimmickValue value, DateTime _)
        {
            if (parameterType == ParameterType.Vector3)
            {
                currentVector = value.Vector3Value;
            }
            else
            {
                currentPower = GetPower(value);
            }
        }

        void FixedUpdate()
        {
            if (space == null)
            {
                return;
            }

            movableItem.AddForce(CalculateForce(), ForceMode);
        }

        float GetPower(GimmickValue value)
        {
            switch (parameterType)
            {
                case ParameterType.Bool:
                    return value.BoolValue ? 1 : 0;
                case ParameterType.Float:
                    return value.FloatValue;
                case ParameterType.Integer:
                    return value.IntegerValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        Vector3 CalculateForce()
        {
            if (parameterType == ParameterType.Vector3)
            {
                return space.TransformDirection(currentVector) * scaleFactor;
            }
            else
            {
                return space.TransformDirection(force) * currentPower;
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
