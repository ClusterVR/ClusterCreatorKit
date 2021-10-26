using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Animator)),
        LocalizableGlobalGimmick(LocalizableGlobalGimmickAttribute.Condition.InPlayerLocal)]
    public sealed class SetAnimatorValueGimmick : MonoBehaviour, IGlobalGimmick
    {
        static readonly ParameterType[] SelectableTypes =
            { ParameterType.Signal, ParameterType.Bool, ParameterType.Float, ParameterType.Integer };

        [SerializeField, HideInInspector] Animator animator;
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField, ParameterTypeField(ParameterType.Signal, ParameterType.Bool, ParameterType.Float, ParameterType.Integer)]
        ParameterType parameterType = SelectableTypes[0];
        [SerializeField, Tooltip("AnimatorのParameter名")] string animatorParameterName;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }

        public void Run(GimmickValue value, DateTime current)
        {
            switch (parameterType)
            {
                case ParameterType.Signal:
                    if (value.TimeStamp <= lastTriggeredAt)
                    {
                        return;
                    }
                    lastTriggeredAt = value.TimeStamp;
                    if ((current - value.TimeStamp).TotalSeconds >
                        Constants.TriggerGimmick.TriggerExpireSeconds)
                    {
                        return;
                    }
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void OnValidate()
        {
            if (animator == null || animator.gameObject != gameObject)
            {
                animator = GetComponent<Animator>();
            }

            if (!SelectableTypes.Contains(parameterType))
            {
                parameterType = SelectableTypes[0];
            }
        }

        void Reset()
        {
            animator = GetComponent<Animator>();
        }
    }
}
