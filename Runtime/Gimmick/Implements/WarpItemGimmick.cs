using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItem))]
    public class WarpItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] MovableItem movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, RequiredTransform] Transform targetTransform;
        [SerializeField] bool positionOnly;

        ItemId IGimmick.ItemId => (movableItem != null ? movableItem.Item : (movableItem = GetComponent<MovableItem>()).Item).Id;
        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

#if UNITY_EDITOR
        public Transform TargetTransform => targetTransform;
#endif

        DateTime lastTriggeredAt;

        void Start()
        {
            if (movableItem == null) movableItem = GetComponent<MovableItem>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (targetTransform == null) return;
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
            movableItem.WarpTo(targetTransform.position, positionOnly ? transform.rotation : targetTransform.rotation);
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject) movableItem = GetComponent<MovableItem>();
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItem>();
        }
    }
}