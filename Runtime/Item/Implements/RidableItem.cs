using System;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class RidableItem : ContactableItem, IRidableItem
    {
        [SerializeField, HideInInspector] Item item;
        [SerializeField, Tooltip(TranslationTable.cck_sitting_position)] Transform seat;
        [SerializeField, Tooltip(TranslationTable.cck_dismount_position_optional)] Transform exitTransform;

        [SerializeField, Tooltip(TranslationTable.cck_avatar_left_hand_position_optional)] Transform leftGrip;
        [SerializeField, Tooltip(TranslationTable.cck_avatar_right_hand_position_optional)] Transform rightGrip;
        [SerializeField, Tooltip(TranslationTable.cck_avatar_animation_optional)] AnimationClip avatarOverrideAnimation;

        public override IItem Item
        {
            get
            {
                if (item != null)
                {
                    return item;
                }
                if (this == null)
                {
                    return null;
                }
                return item = GetComponent<Item>();
            }
        }
        bool IRidableItem.IsDestroyed => this == null;

        public Transform Seat
        {
            get
            {
                if (seat != null)
                {
                    return seat;
                }
                if (this == null)
                {
                    return null;
                }
                return seat = transform;
            }
        }
        Transform IRidableItem.ExitTransform => exitTransform;

        Transform IRidableItem.LeftGrip => leftGrip;
        Transform IRidableItem.RightGrip => rightGrip;
        AnimationClip IRidableItem.AvatarOverrideAnimation => avatarOverrideAnimation;
        public override bool IsContactable => true;
        public override bool RequireOwnership => true;

        public event Action OnInvoked;
        public event Action OnGetOn;
        public event Action OnGetOff;

        public void Construct(Transform seat, Transform exitTransform, Transform leftGrip, Transform rightGrip)
        {
            this.seat = seat;
            this.exitTransform = exitTransform;
            this.leftGrip = leftGrip;
            this.rightGrip = rightGrip;
        }

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

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (seat != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(seat.position, seat.rotation, Vector3.one);
                Gizmos.color = new Color(1, 1, 0, 1);
                Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, 0.5f));
            }

            if (leftGrip != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(leftGrip.position, leftGrip.rotation, Vector3.one);
                Gizmos.color = new Color(0, 0, 1, 1);
                Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, 0.2f));
                Gizmos.color = new Color(0, 0, 1, 0.5f);
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(0.05f, 0.05f, 0.05f));
                UnityEditor.Handles.Label(leftGrip.position, "Left Grip");
            }

            if (rightGrip != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(rightGrip.position, rightGrip.rotation, Vector3.one);
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, 0.2f));
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(0.05f, 0.05f, 0.05f));
                UnityEditor.Handles.Label(rightGrip.position, "Right Grip");
            }

            if (exitTransform != null)
            {
                Gizmos.matrix = Matrix4x4.TRS(exitTransform.position, Quaternion.Euler(0, exitTransform.rotation.eulerAngles.y, 0), Vector3.one);
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(new Vector3(0, 0.25f, 0), new Vector3(0, 0.25f, 0.3f));
                Gizmos.color = new Color(0, 0, 1, 0.5f);
                Gizmos.DrawSphere(new Vector3(0, 0.25f, 0), 0.25f);
                UnityEditor.Handles.Label(exitTransform.position, "Exit");
            }
        }
#endif
    }
}
