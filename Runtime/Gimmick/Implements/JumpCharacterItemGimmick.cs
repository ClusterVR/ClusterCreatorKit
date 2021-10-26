using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(CharacterItem))]
    public sealed class JumpCharacterItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] CharacterItem characterItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField] float speed = 1f;

        ItemId IGimmick.ItemId =>
            (characterItem != null ? characterItem.Item : (characterItem = GetComponent<CharacterItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (characterItem == null)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            characterItem.SetVelocityY(speed);
        }

        void OnValidate()
        {
            if (characterItem == null || characterItem.gameObject != gameObject)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }

        void Reset()
        {
            characterItem = GetComponent<CharacterItem>();
        }
    }
}
