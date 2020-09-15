using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public class AddContinuousTorqueItemGimmick : MonoBehaviour, IItemGimmick
    {
        static readonly ParameterType[] selectableTypes = { ParameterType.Bool, ParameterType.Float, ParameterType.Integer };

        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, ParameterTypeField(ParameterType.Bool, ParameterType.Float, ParameterType.Integer)]
        ParameterType parameterType = selectableTypes[0];
        [SerializeField] Transform space;
        [SerializeField] Vector3 torque;
        [SerializeField] bool ignoreMass;

        ItemId IGimmick.ItemId => (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;
        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        ForceMode ForceMode => ignoreMass ? ForceMode.Acceleration : ForceMode.Force;

        float currentPower;

        void Start()
        {
            if (movableItem == null) movableItem = GetComponent<MovableItem>();
            if (space == null) space = transform;
        }

        public void Run(GimmickValue value, DateTime _)
        {
            currentPower = GetPower(value);
        }

        void FixedUpdate()
        {
            if (space == null) return;
            movableItem.AddTorque(space.TransformDirection(torque) * currentPower, ForceMode);
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

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject) movableItem = GetComponent<MovableItem>();
            if (!selectableTypes.Contains(parameterType))
            {
                parameterType = selectableTypes[0];
            }
            if (space == null) space = transform;
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
        }
    }
}