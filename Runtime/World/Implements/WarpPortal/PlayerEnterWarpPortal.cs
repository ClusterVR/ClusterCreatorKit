﻿using ClusterVR.CreatorKit.Validator;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WarpPortal
{
    [RequireComponent(typeof(Collider))]
    [RequireIsTriggerSettings]
    public sealed class PlayerEnterWarpPortal : MonoBehaviour, IWarpPortal
    {
        [SerializeField] Transform target;
        [SerializeField] bool keepPosition;
        [SerializeField] bool keepRotation;

        public event OnEnterWarpPortalEventHandler OnEnterWarpPortalEvent;

        void OnTriggerEnter(Collider other)
        {
            if (target == null) return;

            OnEnterWarpPortalEvent?.Invoke(
                new OnEnterWarpPortalEventArgs(other.gameObject, target.position, target.rotation, keepPosition,
                    keepRotation));
        }

        void OnValidate()
        {
            foreach (var col in GetComponentsInChildren<Collider>(true))
            {
                col.isTrigger = true;
            }
        }
    }
}
