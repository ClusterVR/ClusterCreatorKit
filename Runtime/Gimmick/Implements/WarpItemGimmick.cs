using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(MovableItemBase))]
    public sealed class WarpItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] MovableItemBase movableItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, RequiredTransform] Transform targetTransform;
        [SerializeField] bool positionOnly;

        ItemId IGimmick.ItemId => (movableItem != null ? movableItem : movableItem = GetComponent<MovableItemBase>()).Item.Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

#if UNITY_EDITOR
        public Transform TargetTransform => targetTransform;
#endif

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (targetTransform == null)
            {
                return;
            }
            if (movableItem == null)
            {
                movableItem = GetComponent<MovableItemBase>();
            }
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            movableItem.WarpTo(targetTransform.position, positionOnly ? transform.rotation : targetTransform.rotation);
        }

        void Reset()
        {
            movableItem = GetComponent<MovableItemBase>();
        }

        void OnValidate()
        {
            if (movableItem == null || movableItem.gameObject != gameObject)
            {
                movableItem = GetComponent<MovableItemBase>();
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (targetTransform != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(targetTransform.position, targetTransform.rotation, Vector3.one);
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(Vector3.zero, new Vector3(1, 0, 0));
                Gizmos.color = new Color(0, 1, 0, 1);
                Gizmos.DrawLine(Vector3.zero, new Vector3(0, 1, 0));
                Gizmos.color = new Color(0, 0, 1, 1);
                Gizmos.DrawLine(Vector3.zero, new Vector3(0, 0, 1));
            }
        }
#endif
    }
}
