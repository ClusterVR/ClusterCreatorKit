using System;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class RidableItem : ContactableItem, IRidableItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, Tooltip("座る位置")] Transform seat;
        [SerializeField, Tooltip("降りる位置（任意）")] Transform exitTransform;

        [SerializeField, Tooltip("座っているアバターの左手の位置（任意）")] Transform leftGrip;
        [SerializeField, Tooltip("座っているアバターの右手の位置（任意）")] Transform rightGrip;
        [SerializeField, Tooltip("座っているアバターのアニメーション（任意）")] AnimationClip avatarOverrideAnimation;

        public override IItem Item => item;
        public Transform Seat => seat;
        Transform IRidableItem.ExitTransform => exitTransform;

        Transform IRidableItem.LeftGrip => leftGrip;
        Transform IRidableItem.RightGrip => rightGrip;
        AnimationClip IRidableItem.AvatarOverrideAnimation => avatarOverrideAnimation;


        public event Action OnInvoked;
        public event Action OnGetOn;
        public event Action OnGetOff;

        void Start()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
        }

        void IInteractableItem.Invoke()
        {
            OnInvoked?.Invoke();
        }

        public void GetOn()
        {
            gameObject.SetLayerRecursively(LayerName.RidingItem);
            OnGetOn?.Invoke();
        }

        public void GetOff()
        {
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
            OnGetOff?.Invoke();
        }

        void Reset()
        {
            item = GetComponent<Item>();
            gameObject.SetLayerRecursively(LayerName.InteractableItem);
            seat = transform;
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject)
            {
                item = GetComponent<Item>();
            }

            if (seat == null)
            {
                seat = transform;
            }
        }
    }
}
