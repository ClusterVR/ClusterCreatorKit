using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public sealed class SetWheelColliderSteerAngleItemGimmick : MonoBehaviour, IItemGimmick
    {
        static readonly ParameterType[] SelectableTypes =
            { ParameterType.Bool, ParameterType.Float, ParameterType.Integer };

        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, ParameterTypeField(ParameterType.Bool, ParameterType.Float, ParameterType.Integer)]
        ParameterType parameterType = SelectableTypes[0];
        [SerializeField] WheelCollider[] wheelColliders = {};
        [SerializeField] float angleRate;

        ItemId IGimmick.ItemId =>
            (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        float currentAngle;

        void Start()
        {
            if (movableItem == null)
            {
                movableItem = GetComponent<MovableItem>();
            }
        }

        void IGimmick.Run(GimmickValue value, DateTime current)
        {
            currentAngle = GetValue(value) * angleRate;
        }

        float GetValue(GimmickValue value)
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

        void FixedUpdate()
        {
            foreach (var wheelCollider in wheelColliders)
            {
                if (wheelCollider == null || wheelCollider.attachedRigidbody != movableItem.Rigidbody)
                {
                    continue;
                }
                wheelCollider.steerAngle = currentAngle;
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
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
        }
    }
}
