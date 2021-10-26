using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(CharacterItem)), DisallowMultipleComponent]
    public sealed class SetAngularVelocityCharacterItemGimmick : MonoBehaviour, IItemGimmick
    {
        static readonly ParameterType[] SelectableTypes =
            { ParameterType.Float, ParameterType.Integer };

        [SerializeField, HideInInspector] CharacterItem characterItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, ParameterTypeField(ParameterType.Float, ParameterType.Integer)]
        ParameterType parameterType = SelectableTypes[0];
        [SerializeField] float scaleFactor = 1f;

        ItemId IGimmick.ItemId =>
            (characterItem != null ? characterItem.Item : (characterItem = GetComponent<CharacterItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        void Start()
        {
            if (characterItem == null)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }

        public void Run(GimmickValue value, DateTime _)
        {
            characterItem.SetAngularVelocityY(GetValue(value) * scaleFactor);
        }

        float GetValue(GimmickValue value)
        {
            switch (parameterType)
            {
                case ParameterType.Float:
                    return value.FloatValue;
                case ParameterType.Integer:
                    return value.IntegerValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void OnValidate()
        {
            if (characterItem == null || characterItem.gameObject != gameObject)
            {
                characterItem = GetComponent<CharacterItem>();
            }
            if (!SelectableTypes.Contains(parameterType))
            {
                parameterType = SelectableTypes[0];
            }
        }

        void Reset()
        {
            characterItem = GetComponent<CharacterItem>();
        }
    }
}
