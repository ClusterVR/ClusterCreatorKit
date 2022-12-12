using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    [RequireComponent(typeof(CharacterItem))]
    public sealed class IsGroundedCharacterItemTrigger : MonoBehaviour, IOnReceiveOwnershipItemTrigger, IInvoluntaryItemTrigger
    {
        [SerializeField, HideInInspector] CharacterItem characterItem;
        [SerializeField, ItemVariableTriggerParam(ParameterType.Bool)]
        VariableTriggerParam[] triggers;

        IItem IItemTrigger.Item => (characterItem != null ? characterItem.Item : (characterItem = GetComponent<CharacterItem>()).Item);

        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => TriggerParams(default);

        bool previousValue;

        void Start()
        {
            if (characterItem == null)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }

        void Update()
        {
            if (characterItem == null)
            {
                return;
            }

            var isGrounded = characterItem.IsGrounded;
            if (isGrounded == previousValue)
            {
                return;
            }
            previousValue = isGrounded;
            OnValueChanged(isGrounded);
        }

        void IOnReceiveOwnershipItemTrigger.Invoke(bool _)
        {
            if (characterItem == null)
            {
                return;
            }

            var isGrounded = characterItem.IsGrounded;
            previousValue = isGrounded;
            OnValueChanged(isGrounded);
        }

        void OnValueChanged(bool input)
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(TriggerParams(input).ToArray()));
        }

        IEnumerable<TriggerParam> TriggerParams(bool input)
        {
            var triggerValue = new TriggerValue(input);
            return triggers.Select(t => t.ConvertWithOverrideValue(ParameterType.Bool, triggerValue)).ToArray();
        }

        void Reset()
        {
            characterItem = GetComponent<CharacterItem>();
        }

        void OnValidate()
        {
            if (characterItem == null || characterItem.gameObject != gameObject)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }
    }
}
