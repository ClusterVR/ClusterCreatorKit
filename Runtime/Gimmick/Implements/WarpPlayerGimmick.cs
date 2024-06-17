using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public sealed class WarpPlayerGimmick : MonoBehaviour, IPlayerEffectGimmick, IWarpPlayerEffect
    {
        [SerializeField] PlayerGimmickKey key;
        [SerializeField] Transform targetTransform;
        [SerializeField] bool keepPosition;
        [SerializeField] bool keepRotation;

        GimmickTarget IGimmick.Target => key.Key.Target;
        string IGimmick.Key => key.Key.Key;
        ItemId IGimmick.ItemId => key.ItemId;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event PlayerEffectEventHandler OnRun;
        public Vector3 TargetPosition => targetTransform.position;
        public Quaternion TargetRotation => targetTransform.rotation;
        public bool KeepPosition => keepPosition;
        public bool KeepRotation => keepRotation;

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (targetTransform == null)
            {
                return;
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
            OnRun?.Invoke(this);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (targetTransform != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(targetTransform.position, Quaternion.Euler(0, targetTransform.rotation.eulerAngles.y, 0), Vector3.one);
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(new Vector3(0, 0.75f, 0), new Vector3(0, 0.75f, 1));
                Gizmos.color = new Color(0, 0, 1, 0.5f);
                Gizmos.DrawSphere(new Vector3(0, 0.75f, 0), 0.75f);
            }
        }
#endif
    }
}
