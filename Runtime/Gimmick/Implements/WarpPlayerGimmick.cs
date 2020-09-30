using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public class WarpPlayerGimmick : MonoBehaviour, IPlayerEffectGimmick, IWarpPlayerEffect
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
            if (targetTransform == null) return;
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
            OnRun?.Invoke(this);
        }
    }
}