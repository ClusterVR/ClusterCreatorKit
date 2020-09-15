using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Animator))]
    public class SetAnimatorValueGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField, HideInInspector] Animator animator;
        [SerializeField, LocalizableGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField] ParameterType parameterType;
        [SerializeField, Tooltip("AnimatorのParameter名")] string animatorParameterName;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (animator == null) animator = GetComponent<Animator>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            switch (parameterType)
            {
                case ParameterType.Signal:
                    if (value.TimeStamp <= lastTriggeredAt) return;
                    lastTriggeredAt = value.TimeStamp;
                    if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
                    animator.SetTrigger(animatorParameterName);
                    break;
                case ParameterType.Bool:
                    animator.SetBool(animatorParameterName, value.BoolValue);
                    break;
                case ParameterType.Float:
                    animator.SetFloat(animatorParameterName, value.FloatValue);
                    break;
                case ParameterType.Integer:
                    animator.SetInteger(animatorParameterName, value.IntegerValue);
                    break;
            }
        }

        void OnValidate()
        {
            if (animator == null || animator.gameObject != gameObject) animator = GetComponent<Animator>();
        }

        void Reset()
        {
            animator = GetComponent<Animator>();
        }
    }
}